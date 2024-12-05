using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class ConsignmentPackagesMeasure
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    public int PackagesSequenceNumeric { get; set; }

    public string? PackageMeasureQualifier { get; set; }

    public decimal? TotalPackageQuantity { get; set; }

    public decimal? GrossMassMeasure { get; set; }

    public string? TypeCode { get; set; }

    public string? MarksNumbers { get; set; }

    public int ConsignmentId { get; set; }

    public virtual Consignment ? Consignment { get; set; } 

    public virtual Declaration ? Declaration { get; set; }
}
