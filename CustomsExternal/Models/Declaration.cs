using System;
using System.Collections.Generic;

namespace CustomsExternal.Models;

public partial class Declaration
{
    public int Id { get; set; }

    public string? AgentFileReferenceId { get; set; }

    public DateTime? AcceptanceDateTime { get; set; }

    public string? DeclarationOfficeId { get; set; }

    public string? DeclarationNumber { get; set; }

    public DateTime? IssueDateTime { get; set; }

    public string? TypeCode { get; set; }

    public DateTime? TaxationDateTime { get; set; }

    public string? ExternalDeclarationId { get; set; }

    public string? VersionId { get; set; }

    public decimal? ExpenseLoadingFactor { get; set; }

    public string? AutonomyRegionType { get; set; }

    public string? TehilaDeclarationId { get; set; }

    public DateTime? ReleaseDateTime { get; set; }

    public decimal? TotalDealValueAmountNis { get; set; }

    public decimal? CifValueNis { get; set; }

    public decimal? TaxAssessedAmount { get; set; }

    public string? AgentId { get; set; }

    public string? RoleCode { get; set; }

    public string? GovernmentProcedure { get; set; }

    public string? ImporterId { get; set; }

    public string? ImporterRoleCode { get; set; }

    public string? EntitlementTypeCode { get; set; }

    public string? IssueLocation { get; set; }

    public string? ImporterName { get; set; }

    public string? ImporterCountry { get; set; }

    public string? PreviousDocument { get; set; }

    public string? PreviousDocumentType { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? DeclarationStatusCode { get; set; }

    public int? CustomsStatus { get; set; }

    public decimal? TotalMaddealValueAmountNis { get; set; }

    public virtual ICollection<ConsignmentPackagesMeasure> ConsignmentPackagesMeasures { get; set; } = new List<ConsignmentPackagesMeasure>();

    public virtual ICollection<ConsignmentRegisteredFacility> ConsignmentRegisteredFacilities { get; set; } = new List<ConsignmentRegisteredFacility>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<DeclarationTaxis> DeclarationTaxes { get; set; } = new List<DeclarationTaxis>();

    public virtual ICollection<SupplierInvoice> SupplierInvoices { get; set; } = new List<SupplierInvoice>();
}
