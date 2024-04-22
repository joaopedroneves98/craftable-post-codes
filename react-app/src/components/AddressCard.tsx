import {Card, CardContent, Typography} from "@mui/material";
import {PostCodeResult} from '../types/PostCodeResult';

function AddressCard(postCodeResult: PostCodeResult){
    return (
        <Card sx={{minWidth: 275}}>
            <CardContent>
                <Typography variant="h5" component="div">
                    PostCode: {postCodeResult.postCode}
                </Typography>
                <Typography variant="h6">
                    District: {postCodeResult.district}
                </Typography>
                <Typography variant="body2">
                    Latitude: {postCodeResult.latitude}
                </Typography>
                <Typography variant="body2">
                    Longitude: {postCodeResult.longitude}
                </Typography>
                <Typography variant="body1">
                    Distance to Heathrow:
                </Typography>
                <Typography variant="body2">
                    {postCodeResult.distanceToHeathrowInKMs} kms
                    <br/>
                    {postCodeResult.distanceToHeathrowInMiles} mi
                </Typography>

            </CardContent>
        </Card>
    );
}

export default AddressCard;