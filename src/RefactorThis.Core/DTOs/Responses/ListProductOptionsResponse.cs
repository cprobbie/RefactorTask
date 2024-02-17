using System.Collections.Generic;

namespace RefactorThis.Core.Domain.Responses;

public record ListProductOptionsResponse(IEnumerable<Option?> Items);
