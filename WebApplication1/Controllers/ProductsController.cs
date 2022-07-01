using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ProductsVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] SearchQueryParams query, CancellationToken cancellationToken)
        {
            var response = await _productsService.GetProducts(query, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _productsService.GetProduct(id, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/update")]
        [ProducesResponseType(typeof(ProductVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductVM product, CancellationToken cancellationToken)
        {
            var response = await _productsService.UpdateProduct(id, product, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/Add/{courseId}")]
        [ProducesResponseType(typeof(ProductVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddCourse([FromRoute] int id, [FromRoute] int courseId, CancellationToken cancellationToken)
        {
            var response = await _productsService.AddCourseToProduct(id, courseId, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }

        [HttpPost("{id}/Remove/{courseId}")]
        [ProducesResponseType(typeof(ProductVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveCourse([FromRoute] int id, [FromRoute] int courseId, CancellationToken cancellationToken)
        {
            var response = await _productsService.RemoveCourseFromProduct(id, courseId, cancellationToken);
            return response == null ? NotFound(null) : Ok(response);
        }
    }
}