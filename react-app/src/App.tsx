import './App.css';
import "/node_modules/flag-icons/css/flag-icons.min.css";

import {useState} from 'react';

import {
    IconButton,
    styled,
    TextField,
    Alert,
    Stack,
    Typography,
    Snackbar,
    Button,
} from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import DeleteIcon from '@mui/icons-material/Delete';

import {PostCodeResult} from './types/PostCodeResult';
import AddressCard from './components/AddressCard';

function App() {
    const [postCode, setPostCode] = useState("");
    const [postCodeResults, setPostCodeResults] = useState<PostCodeResult[]>([]);
    const [errorMessage, setErrorMessage] = useState('');
    const [open, setOpenErrorSnack] = useState(false);

    const fetchApiData = async () => {
        try {
            const response = await fetch('http://localhost:5140/api/v1/PostCodes?postCode=' + postCode);
            const data = await response.json();

            if (!response.ok) {
                setErrorMessage(response.statusText);
                setOpenErrorSnack(true);
            } else {
                setPostCodeResults((prevResults) => {
                    const updatedResults = [...prevResults, data];
                    return updatedResults.slice(-3);
                });
            }
        } catch (error) {
            setErrorMessage('Error fetching API data: ' + error);
            setOpenErrorSnack(true);
        }
    };
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        await fetchApiData();
    };

    const handleErrorOnClose = () => {
        setOpenErrorSnack(false);
        setErrorMessage('');
    }
    
    const handleClearAddresses = () => {
        setPostCodeResults([]);
    }

    return (
        <div className="App">
            <header className="page-header">
                <Typography variant="h1">
                    <span className="fi fi-gb"></span> Post Code Search
                </Typography>
            </header>
            <Stack spacing={2}>

                <form onSubmit={handleSubmit}>
                    <StyledTextField label="Post Code"
                                     onChange={(e) => setPostCode(e.target.value)}
                                     sx={{'padding-right': '15px'}}
                    />
                    <IconButton type="submit">
                        <SearchIcon fontSize="inherit" sx={{color: "white"}}/>
                    </IconButton>
                </form>

                <Stack spacing={2} direction="column-reverse">
                    {postCodeResults.map((result, i) => (
                        <AddressCard key={i}
                                    postCode={result.postCode}
                                     latitude={result.latitude}
                                     longitude={result.longitude}
                                     distanceToHeathrowInKMs={result.distanceToHeathrowInKMs}
                                     distanceToHeathrowInMiles={result.distanceToHeathrowInMiles}
                                     district={result.district}/>
                    ))}
                </Stack>

                {postCodeResults.length !== 0 &&
                    <Button type="submit" onClick={handleClearAddresses} variant="contained"
                            startIcon={<DeleteIcon />}
                            color="error">
                        Clear Address List
                    </Button>
                }
            </Stack>
            <Snackbar open={open} autoHideDuration={5000} onClose={handleErrorOnClose}>
                <Alert
                    onClose={handleErrorOnClose}
                    severity="error"
                    variant="filled"
                    sx={{width: '100%'}}
                >
                    {errorMessage}
                </Alert>
            </Snackbar>
        </div>
    );
}

const StyledTextField = styled(TextField)({
    "& label": {
        color: "white"
    },
    "& input": {
        color: "white"
    },
    "&:hover label": {
        fontWeight: 700
    },
    "& label.Mui-focused": {
        color: "white"
    },
    "& .MuiInput-underline:after": {
        borderBottomColor: "white"
    },
    "& .MuiOutlinedInput-root": {
        "& fieldset": {
            borderColor: "white"
        },
        "&:hover fieldset": {
            borderColor: "white",
            borderWidth: 2
        },
        "&.Mui-focused fieldset": {
            borderColor: "white"
        }
    }
});

export default App;
