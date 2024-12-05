using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class Document
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string? DocumentType { get; set; }

    public int? CustomsId { get; set; }

    public string? InternalId { get; set; }

    public int? CustomsStatus { get; set; }

    public string? ErrorDesc { get; set; }

    public decimal? RelatedEntity { get; set; }

    public decimal? RelatedId { get; set; }
}
