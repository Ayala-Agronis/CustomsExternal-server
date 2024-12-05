using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class SupplierInvoice
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    public int SequenceNumeric { get; set; }

    public string? InvoiceNumber { get; set; }

    public DateTime IssueDateTime { get; set; }

    public string? InvoiceTypeCode { get; set; }

    public bool IsPrefarenceDocumentInd { get; set; }

    public string? PrefarenceDocumentType { get; set; }

    public string? PaymentType { get; set; }

    public decimal InvoiceAmount { get; set; }

    public decimal RateNumeric { get; set; }

    public string? SupplierId { get; set; }

    public string? TradeTermsConditionCode { get; set; }

    public string? LocationId { get; set; }

    public string? CurrencyCode { get; set; }

    public virtual Declaration ? Declaration { get; set; } 

    public virtual ICollection<SupplierInvoiceItem> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItem>();
}
