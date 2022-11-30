using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutgoingContext.Product.Commands;
using OutgoingContext.Product.Queries;

namespace WebApi.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct(RegisterProductCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(RemoveProductCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
