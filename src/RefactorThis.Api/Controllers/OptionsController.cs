using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/products/{id}/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly ICreateOptionRequestProcessor _createOptionRequestProcessor;
        private readonly IGetOptionsRequestProcessor _getOptionsRequestProcessor;
        private readonly IUpdateOptionRequestProcessor _updateOptionRequestProcessor;
        private readonly IDeleteOptionRequestProcessor _deleteOptionRequestProcessor;

        public OptionsController(
            ICreateOptionRequestProcessor createOptionRequestProcessor,
            IGetOptionsRequestProcessor getOptionsRequestProcessor,
            IUpdateOptionRequestProcessor updateOptionRequestProcessor,
            IDeleteOptionRequestProcessor deleteOptionRequestProcessor)
        {
            _createOptionRequestProcessor = createOptionRequestProcessor;
            _getOptionsRequestProcessor = getOptionsRequestProcessor;
            _updateOptionRequestProcessor = updateOptionRequestProcessor;
            _deleteOptionRequestProcessor = deleteOptionRequestProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            var options = await _getOptionsRequestProcessor.ListOptionsAsync(id);
            return Ok(options);
        }

        [HttpGet("{optionId}")]
        public async Task<IActionResult> GetOption(Guid id, Guid optionId)
        {
            var option = await _getOptionsRequestProcessor.GetOptionByIdAsync(id, optionId);
            return Ok(option);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOption(Guid id, [FromBody]ProductOptionRequest optionRequest)
        {
            await _createOptionRequestProcessor.CreateProductOptionAsync(id, optionRequest);
            return Ok();
        }

        [HttpPut("{optionId}")]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, [FromBody] ProductOptionRequest optionRequest)
        {
            await _updateOptionRequestProcessor.UpdateProductOptionAsync(id, optionId, optionRequest);
            return Ok();
        }

        [HttpDelete("{optionId}")]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            await _deleteOptionRequestProcessor.DeleteProductOptionAsync(id, optionId);
            return Ok();
        }
    }
}
