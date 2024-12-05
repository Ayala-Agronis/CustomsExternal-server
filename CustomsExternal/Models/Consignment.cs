using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class Consignment
{
    public int Id { get; set; }

    public int DeclarationId { get; set; }

    public short SequenceNumeric { get; set; }

    public string? CargoDescription { get; set; }

    public int? LastReleaseFromWarehousInd { get; set; }

    public string? ExportationCountryCode { get; set; }

    public string? LoadingLocation { get; set; }

    public string? TransportContractDocumentId { get; set; }

    public string? TransportContractDocumentTypeCode { get; set; }

    public DateTime? TransportContractDocumentIssueDateTime { get; set; }

    public string? SecondCargoId { get; set; }

    public string? ThirdCargoId { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public string? UnloadingLocationId { get; set; }

    public virtual ICollection<ConsignmentPackagesMeasure> ConsignmentPackagesMeasures { get; set; } = new List<ConsignmentPackagesMeasure>();

    public virtual ICollection<ConsignmentRegisteredFacility> ConsignmentRegisteredFacilities { get; set; } = new List<ConsignmentRegisteredFacility>();

    public virtual Declaration ? Declaration { get; set; } 
}
