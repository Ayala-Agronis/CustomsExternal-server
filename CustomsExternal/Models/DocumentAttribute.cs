using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class DocumentAttribute
{
    public decimal Id { get; set; }

    public decimal? PointerId { get; set; }

    public decimal? DocId { get; set; }

    public string? Attribute { get; set; }

    public string? AttributeVlaue { get; set; }
}
