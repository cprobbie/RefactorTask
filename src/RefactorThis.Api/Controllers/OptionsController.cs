using System;

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
        public IActionResult GetOptions(Guid id)
        {
            var options = _getOptionsRequestProcessor.ListOptions(id);
            return Ok(options);
        }

        [HttpGet("{optionId}")]
        public IActionResult GetOption(Guid id, Guid optionId)
        {
            var option = _getOptionsRequestProcessor.GetOptionById(id, optionId);
            return Ok(option);
        }

        [HttpPost]
        public IActionResult CreateOption(Guid id, [FromBody]ProductOptionRequest optionRequest)
        {
            _createOptionRequestProcessor.CreateProductOption(id, optionRequest);
            return Ok();
        }

        [HttpPut("{optionId}")]
        public IActionResult UpdateOption(Guid id, Guid optionId, [FromBody] ProductOptionRequest optionRequest)
        {
            _updateOptionRequestProcessor.UpdateProductOption(id, optionId, optionRequest);
            return Ok();
        }

        [HttpDelete("{optionId}")]
        public IActionResult DeleteOption(Guid id, Guid optionId)
        {
            _deleteOptionRequestProcessor.DeleteProductOption(id, optionId);
            return Ok();
        }
    }
}
