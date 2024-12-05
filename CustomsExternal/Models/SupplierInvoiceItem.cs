using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class SupplierInvoiceItem
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    public int SupplierInvoiceId { get; set; }

    public int LineNumber { get; set; }

    public string? CustomsBookType { get; set; }

    public string? TaxExemptCode { get; set; }

    public string? OptionalTama { get; set; }

    public string? SalesTaxExemptionType { get; set; }

    public bool IsUsed { get; set; }

    public decimal CustomsValueAmount { get; set; }

    public string? AmountType { get; set; }

    public string? ClassificationId { get; set; }

    public string? ClassificationIdentificationTypeCode { get; set; }

    public string? TariffQuantity { get; set; }

    public string? OriginCountryCode { get; set; }

    public string? DutyRegimeCode { get; set; }

    public string? MeasureQualifier { get; set; }

    public virtual SupplierInvoice ? SupplierInvoice { get; set; } 
}
