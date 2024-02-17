using System.Collections.Generic;
using RefactorThis.Core.Domain;

namespace RefactorThis.Core.DTOs.Responses;

public record ListProductOptionsResponse(IEnumerable<Option?> Items);
