using System;

using Microsoft.AspNetCore.Mvc;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
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
            if (options.Count == 0)
            {
                return NotFound($"No option was found for ProductId: {id}");
            }
            var optionsDTO = new ProductOptionsDTO(options);
            return Ok(optionsDTO);
        }

        [HttpGet("{optionId}")]
        public IActionResult GetOption(Guid id, Guid optionId)
        {
            var option = _getOptionsRequestProcessor.GetOptionById(id, optionId);
            if (option is null)
            {
                return NotFound($"No option was found for ProductId {id} and OptionId {optionId}");
            }
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
