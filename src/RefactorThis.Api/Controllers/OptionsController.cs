using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactorThis.Core.DTOs.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Api.Controllers;

[Route("api/v1/Products/{productId:guid}/[controller]")]
[ApiController]
public class OptionsController(ILogger<OptionsController> logger, IProductOptionService productOptionService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListOptions(Guid productId)
    {
        var options = await productOptionService.ListOptionsAsync(productId);
        logger.LogInformation("List options for Product (Id: {productId})  successfully", productId);
        return Ok(options);
    }

    [HttpGet("{optionId:guid}")]
    public async Task<IActionResult> GetOption(Guid productId, Guid optionId)
    {
        var option = await productOptionService.GetOptionByIdAsync(productId, optionId);
        if (option is null)
            return NotFound();
        
        logger.LogInformation("Retrieved options for Product(Id: {productId}) and Option(Id: {optionId}) successfully", productId, optionId);
        return Ok(option);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOption(Guid productId, [FromBody]CreateProductOptionRequest optionRequest)
    {
        var result = await productOptionService.CreateProductOptionAsync(productId, optionRequest);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        logger.LogInformation("Created option for Product(Id :{productId}) successfully", productId);
        return Created($"api/v1/Products/{productId}", new { ProductId = productId, OptionId = result.Value });
    }

    [HttpPut("{optionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOption(Guid productId, Guid optionId, [FromBody] UpdateProductOptionRequest optionRequest)
    {
        var result = await productOptionService.UpdateProductOptionAsync(productId, optionId, optionRequest);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        logger.LogInformation("Update option for Product(Id :{ProductId}) and Option(Id: {optionId}) successfully", productId, optionId);
        return Ok();
    }

    [HttpDelete("{optionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOption(Guid productId, Guid optionId)
    {
        var result = await productOptionService.DeleteProductOptionAsync(productId, optionId);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        logger.LogInformation("Delete option for Product(Id :{productId}) and Option(Id: {optionId}) successfully", productId, optionId);
        return Ok();
    }
}