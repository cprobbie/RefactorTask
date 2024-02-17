using System.Collections.Generic;

namespace RefactorThis.Core.Domain.Responses;

public record ListProductsResponse(IEnumerable<Product?> Items);
