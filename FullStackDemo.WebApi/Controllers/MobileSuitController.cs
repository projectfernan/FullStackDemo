using Azure;
using FullStackDemo.ApplicationService.Commands.CommandModels.MobileSuits;
using FullStackDemo.ApplicationService.Commands.Interfaces.IMobilesSuits;
using FullStackDemo.ApplicationService.DTOs.Common;
using FullStackDemo.ApplicationService.DTOs.MobileSuits;
using FullStackDemo.ApplicationService.Queries.Interfaces.IMobileSuits;
using FullStackDemo.ApplicationService.Queries.QueryModels.MobileSuits;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackDemo.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileSuitController : ControllerBase
    {
        private readonly IMobileSuitQueryHandler _mobileSuitQueryHandler;
        private readonly IMobileSuitCommandHandler _mobileSuitCommandHandler;

        public MobileSuitController(IMobileSuitQueryHandler mobileSuitQueryHandler,
                                    IMobileSuitCommandHandler mobileSuitCommandHandler) { 
            _mobileSuitQueryHandler = mobileSuitQueryHandler;
            _mobileSuitCommandHandler = mobileSuitCommandHandler;
        }

        // GET: api/MobileSuit/GetMobileSuitsPaginated?start=0&length=5
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMobileSuitsPaginated([FromQuery] GetMobileSuitPaginatedQuery data)
        {
            ApiBodyDto response = new ApiBodyDto();

            // Ensure the search parameter is not null or empty; set to empty string if it is
            if (string.IsNullOrEmpty(data.search) || data.search == "null" || data.search == null)
            {
                data.search = "";
            }

            try
            {
                // Call the query handler to get the paginated Mobile Suits data
                var res = await _mobileSuitQueryHandler.HandleGetMobileSuitPaginated(data);

                // Populate the response DTO with the results
                response.TotalCount = res.TotalCount; // Set the total count of items
                response.Data = res.Data; // Set the list of Mobile Suits
            }
            catch {
                // Return an appropriate error response (e.g., 500 Internal Server Error)
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }

            // Return a successful response with the data
            return Ok(response);
        }

        // POST: api/MobileSuit/CreateMobileSuit
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMobileSuit([FromBody] CreateMobileSuitCommand data) 
        {
            ApiBodyDto response = new ApiBodyDto();

            try
            {
                // Call the command handler to create a new MobileSuit and get the result
                int res = await _mobileSuitCommandHandler.HandleCreateMobileSuit(data);

                // Set the total count of created MobileSuits in the response
                response.TotalCount = res;
            }
            catch {
                // Return an appropriate error response (e.g., 500 Internal Server Error)
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }

            // Return a successful response with the created MobileSuit count
            return Ok(response);
        }

        // GET api/MobileSuit/GetMobileSuitById?id=1
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMobileSuitById([FromQuery] GetMobileSuitByIdQuery data)
        {
            ApiBodyDto response = new ApiBodyDto();

            try
            {
                // Call the query handler to retrieve a MobileSuit by its ID
                var res = await _mobileSuitQueryHandler.HandleGetMobileSuitById(data);

                // Check if a valid MobileSuit was returned
                if (res.Id > 0)
                {
                    response.TotalCount = 1; // Set the count to 1 since a MobileSuit was found
                    response.Data = res; // Assign the retrieved MobileSuit data to the response
                }
                else {
                    response.TotalCount = 0; // Set count to 0 if no MobileSuit was found
                }
            }
            catch
            {
                // Return an appropriate error response (e.g., 500 Internal Server Error)
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }

            // Return a successful response with the retrieved MobileSuit data
            return Ok(response);
        }

        // PUT api/MobileSuit/UpdateMobileSuit
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMobileSuit([FromBody] UpdateMobileSuitCommand data)
        {
            ApiBodyDto response = new ApiBodyDto();

            try
            {
                // Call the command handler to update the MobileSuit and get the result
                int res = await _mobileSuitCommandHandler.HandleUpdateMobileSuit(data);

                // Set the total count of updated MobileSuits in the response
                response.TotalCount = res;
            }
            catch
            {
                // Return an appropriate error response (e.g., 500 Internal Server Error)
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }

            // Return a successful response with the updated MobileSuit count
            return Ok(response);
        }

        // DELETE api/MobileSuit/DeleteMobileSuit
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteMobileSuit([FromQuery] DeleteMobileSuitCommand data)
        {
            ApiBodyDto response = new ApiBodyDto();

            try
            {
                // Call the command handler to delete the MobileSuit and get the result
                int res = await _mobileSuitCommandHandler.HandleDeleteMobileSuit(data);

                // Set the total count of deleted MobileSuits in the response
                response.TotalCount = res;
            }
            catch
            {
                // Return an appropriate error response (e.g., 500 Internal Server Error)
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }

            // Return a successful response with the deleted MobileSuit count
            return Ok(response);
        }
    }
}
