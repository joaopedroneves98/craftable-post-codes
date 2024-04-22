namespace Craftable.PostCodes.API.Controllers
{
    using Application.Abstractions.Application;
    using Application.DTO;

    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostCodesController : ControllerBase
    {
        private readonly IPostCodesService _postCodesService;
        
        public PostCodesController(IPostCodesService postCodesService)
        {
            this._postCodesService = postCodesService;

        }
        
        /// <summary>
        /// Obtains information regarding the provided post code, like the coordinates and distance to Heathrow airport.
        /// </summary>
        /// <param name="postCode">The post code to be searched</param>
        /// <returns>Information regardin the post code.</returns>
        [HttpGet(Name = "GetPostCodeAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostCodeAsync([FromQuery] string postCode)
        {
            return this.Ok(await this._postCodesService.GetAddressDetailsAsync(postCode));
        }
    }
}