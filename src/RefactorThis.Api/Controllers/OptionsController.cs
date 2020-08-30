using System;

using Microsoft.AspNetCore.Mvc;

using RefactorThis.Core.Domain;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/product/{id}/[controller]")]
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
        public IActionResult CreateOption(Guid id, [FromBody]ProductOption option)
        {
            _createOptionRequestProcessor.CreateProductOption(id, option);
            return Ok();
        }

        [HttpPut("{optionId}")]
        public IActionResult UpdateOption(Guid id, Guid optionId, [FromBody]ProductOption option)
        {
            _updateOptionRequestProcessor.UpdateProductOption(id, optionId, option);
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
