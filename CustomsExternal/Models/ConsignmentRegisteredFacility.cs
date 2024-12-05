using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class ConsignmentRegisteredFacility
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    public int ConsignmentId { get; set; }

    public short FacilitySequenceNumeric { get; set; }

    public string? FacilityId { get; set; }

    public string? FacilityType { get; set; }

    public virtual Consignment Consignment { get; set; } = null!;

    public virtual Declaration Declaration { get; set; } = null!;
}
