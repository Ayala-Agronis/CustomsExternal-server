using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class Registration
{
    public decimal RowId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? CustomerType { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public bool? AllowPromotion { get; set; }

    public string? Password { get; set; }
}
