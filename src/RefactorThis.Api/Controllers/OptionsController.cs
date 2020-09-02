using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/Products/{id}/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly ILogger<OptionsController> _logger;
        private readonly ICreateOptionRequestProcessor _createOptionRequestProcessor;
        private readonly IGetOptionsRequestProcessor _getOptionsRequestProcessor;
        private readonly IUpdateOptionRequestProcessor _updateOptionRequestProcessor;
        private readonly IDeleteOptionRequestProcessor _deleteOptionRequestProcessor;

        public OptionsController(ILogger<OptionsController> logger, 
            ICreateOptionRequestProcessor createOptionRequestProcessor,
            IGetOptionsRequestProcessor getOptionsRequestProcessor,
            IUpdateOptionRequestProcessor updateOptionRequestProcessor,
            IDeleteOptionRequestProcessor deleteOptionRequestProcessor)
        {
            _logger = logger;
            _createOptionRequestProcessor = createOptionRequestProcessor;
            _getOptionsRequestProcessor = getOptionsRequestProcessor;
            _updateOptionRequestProcessor = updateOptionRequestProcessor;
            _deleteOptionRequestProcessor = deleteOptionRequestProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            var options = await _getOptionsRequestProcessor.ListOptionsAsync(id);
            _logger.LogInformation($"List options for Product(Id: {id})  successfully");
            return Ok(options);
        }

        [HttpGet("{optionId}")]
        public async Task<IActionResult> GetOption(Guid id, Guid optionId)
        {
            var option = await _getOptionsRequestProcessor.GetOptionByIdAsync(id, optionId);
            _logger.LogInformation($"Get options for Product(Id: {id}) and Option(Id: {optionId}) successfully");
            return Ok(option);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOption(Guid id, [FromBody]CreateProductOptionRequest optionRequest)
        {
            await _createOptionRequestProcessor.CreateProductOptionAsync(id, optionRequest);
            _logger.LogInformation($"Create option for Product(Id :{id}) successfully");
            return Ok();
        }

        [HttpPut("{optionId}")]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, [FromBody] UpdateProductOptionRequest optionRequest)
        {
            await _updateOptionRequestProcessor.UpdateProductOptionAsync(id, optionId, optionRequest);
            _logger.LogInformation($"Update option for Product(Id :{id}) and Option(Id: {optionId}) successfully");
            return Ok();
        }

        [HttpDelete("{optionId}")]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            await _deleteOptionRequestProcessor.DeleteProductOptionAsync(id, optionId);
            _logger.LogInformation($"Delete option for Product(Id :{id}) and Option(Id: {optionId}) successfully");
            return Ok();
        }
    }
}
