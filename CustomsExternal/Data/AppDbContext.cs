using System;
using System.Collections.Generic;
using CustomsExternal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomsExternal.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consignment> Consignments { get; set; }

    public virtual DbSet<ConsignmentPackagesMeasure> ConsignmentPackagesMeasures { get; set; }

    public virtual DbSet<ConsignmentRegisteredFacility> ConsignmentRegisteredFacilities { get; set; }

    public virtual DbSet<Declaration> Declarations { get; set; }

    public virtual DbSet<DeclarationTaxis> DeclarationTaxes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentAttribute> DocumentAttributes { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<SupplierInvoice> SupplierInvoices { get; set; }

    public virtual DbSet<SupplierInvoiceItem> SupplierInvoiceItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AGRO-AYALA;Database=CustomsExternal;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consignment>(entity =>
        {
            entity.Property(e => e.SecondCargoId).HasColumnName("SecondCargoID");
            entity.Property(e => e.ThirdCargoId).HasColumnName("ThirdCargoID");
            entity.Property(e => e.TransportContractDocumentId).HasColumnName("TransportContractDocumentID");
            entity.Property(e => e.UnloadingLocationId).HasColumnName("UnloadingLocationID");

            entity.HasOne(d => d.Declaration).WithMany(p => p.Consignments).HasForeignKey(d => d.DeclarationId);
        });

        modelBuilder.Entity<ConsignmentPackagesMeasure>(entity =>
        {
            entity.Property(e => e.GrossMassMeasure).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPackageQuantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Consignment).WithMany(p => p.ConsignmentPackagesMeasures).HasForeignKey(d => d.ConsignmentId);

            entity.HasOne(d => d.Declaration).WithMany(p => p.ConsignmentPackagesMeasures)
                .HasForeignKey(d => d.DeclarationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ConsignmentRegisteredFacility>(entity =>
        {
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

            entity.HasOne(d => d.Consignment).WithMany(p => p.ConsignmentRegisteredFacilities).HasForeignKey(d => d.ConsignmentId);

            entity.HasOne(d => d.Declaration).WithMany(p => p.ConsignmentRegisteredFacilities)
                .HasForeignKey(d => d.DeclarationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Declaration>(entity =>
        {
            entity.Property(e => e.AcceptanceDateTime).HasColumnType("datetime");
            entity.Property(e => e.AgentFileReferenceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AgentFileReferenceID");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.CifValueNis)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CifValueNIS");
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.DeclarationOfficeId).HasColumnName("DeclarationOfficeID");
            entity.Property(e => e.ExpenseLoadingFactor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExternalDeclarationId).HasColumnName("ExternalDeclarationID");
            entity.Property(e => e.ImporterId).HasColumnName("ImporterID");
            entity.Property(e => e.IssueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReleaseDateTime).HasColumnType("datetime");
            entity.Property(e => e.TaxAssessedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxationDateTime).HasColumnType("datetime");
            entity.Property(e => e.TehilaDeclarationId).HasColumnName("TehilaDeclarationID");
            entity.Property(e => e.TotalDealValueAmountNis)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalDealValueAmountNIS");
            entity.Property(e => e.TotalMaddealValueAmountNis)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalMADDealValueAmountNIS");
            entity.Property(e => e.VersionId).HasColumnName("VersionID");
        });

        modelBuilder.Entity<DeclarationTaxis>(entity =>
        {
            entity.Property(e => e.AdValoremTaxBaseAmount)
                .HasComment("בסיס לחישוב מס")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Amount)
                .HasComment("סכום מס")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeferedTaxAmount)
                .HasComment("סכום מס נדחה")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxTypeCode).HasComment("סעיף מס שחושב");

            entity.HasOne(d => d.Declaration).WithMany(p => p.DeclarationTaxes).HasForeignKey(d => d.DeclarationId);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.InternalId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("InternalID");
            entity.Property(e => e.RelatedEntity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RelatedId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RelatedID");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<DocumentAttribute>(entity =>
        {
            entity.ToTable("DocumentAttribute");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Attribute).HasMaxLength(50);
            entity.Property(e => e.AttributeVlaue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Attribute_Vlaue");
            entity.Property(e => e.DocId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("DocID");
            entity.Property(e => e.PointerId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PointerID");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK_Registiration");

            entity.ToTable("Registration");

            entity.Property(e => e.RowId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CustomerType)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(35);
            entity.Property(e => e.LastName).HasMaxLength(35);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupplierInvoice>(entity =>
        {
            entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.RateNumeric).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Declaration).WithMany(p => p.SupplierInvoices).HasForeignKey(d => d.DeclarationId);
        });

        modelBuilder.Entity<SupplierInvoiceItem>(entity =>
        {
            entity.Property(e => e.ClassificationId).HasColumnName("ClassificationID");
            entity.Property(e => e.CustomsValueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MeasureQualifier)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.HasOne(d => d.SupplierInvoice).WithMany(p => p.SupplierInvoiceItems).HasForeignKey(d => d.SupplierInvoiceId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
