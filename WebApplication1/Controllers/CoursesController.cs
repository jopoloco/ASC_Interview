using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly IProductsService _productsService;

        public CoursesController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(CoursesVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] SearchQueryParams query, CancellationToken cancellationToken)
        {
            var response = await _productsService.GetCourses(query, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CoursesVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCourse([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _productsService.GetCourse(id, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/update")]
        [ProducesResponseType(typeof(CourseVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] CourseVM course, CancellationToken cancellationToken)
        {
            var response = await _productsService.UpdateCourse(id, course, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/Add/{productId}")]
        [ProducesResponseType(typeof(CourseVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddProductToCourse([FromRoute] int id, [FromRoute] int productId, CancellationToken cancellationToken)
        {
            var response = await _productsService.AddProductToCourse(id, productId, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/Remove/{productId}")]
        [ProducesResponseType(typeof(CourseVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveProductFromCourse([FromRoute] int id, [FromRoute] int productId, CancellationToken cancellationToken)
        {
            var response = await _productsService.RemoveProductFromCourse(id, productId, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }
    }
}