using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class DeclarationTaxis
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    /// <summary>
    /// בסיס לחישוב מס
    /// </summary>
    public decimal? AdValoremTaxBaseAmount { get; set; }

    /// <summary>
    /// סעיף מס שחושב
    /// </summary>
    public string? TaxTypeCode { get; set; }

    /// <summary>
    /// סכום מס
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// סכום מס נדחה
    /// </summary>
    public decimal DeferedTaxAmount { get; set; }

    public virtual Declaration Declaration { get; set; } = null!;
}
