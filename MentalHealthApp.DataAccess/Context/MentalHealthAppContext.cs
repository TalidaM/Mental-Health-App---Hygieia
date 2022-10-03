using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MentalHealthApp.Entities;
using MentalHealthApp.Entities.Entities;

namespace MentalHealthApp.DataAccess.Context
{
    public partial class MentalHealthAppContext : DbContext
    {
        public MentalHealthAppContext()
        {
        }

        public MentalHealthAppContext(DbContextOptions<MentalHealthAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresa> Adresas { get; set; } = null!;
        public virtual DbSet<CameraConferintum> CameraConferinta { get; set; } = null!;
        public virtual DbSet<ComentariiDiscutie> ComentariiDiscuties { get; set; } = null!;
        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<Discutie> Discuties { get; set; } = null!;
        public virtual DbSet<IdentityRole> IdentityRoles { get; set; } = null!;
        public virtual DbSet<IdentityUser> IdentityUsers { get; set; } = null!;
        public virtual DbSet<IdentityUserToken> IdentityUserTokens { get; set; } = null!;
        public virtual DbSet<IdentityUserTokenConfirmation> IdentityUserTokenConfirmations { get; set; } = null!;
        public virtual DbSet<IstoricDiagnosticUtilizator> IstoricDiagnosticUtilizators { get; set; } = null!;
        public virtual DbSet<Programare> Programares { get; set; } = null!;
        public virtual DbSet<Simptome> Simptomes { get; set; } = null!;
        public virtual DbSet<Specialist> Specialists { get; set; } = null!;
        public virtual DbSet<Utilizator> Utilizators { get; set; } = null!;
        public virtual DbSet<Valabilitate> Valabilitati { get; set; } = null!;
        public virtual DbSet<TopReads> TopReads { get; set; } = null!;
        public virtual DbSet<ConditionsPost> ConditionsPosts { get; set; } = null!;
        public virtual DbSet<MedicalReport> MedicalReports { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Initial Catalog=MentalHealthProject;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresa>(entity =>
            {
                entity.ToTable("Adresa");

                entity.Property(e => e.BlocScaraApartament).HasMaxLength(50);

                entity.Property(e => e.CodPostal)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.Judet).HasMaxLength(100);

                entity.Property(e => e.Oras).HasMaxLength(100);

                entity.Property(e => e.Sector).HasMaxLength(50);

                entity.Property(e => e.StradaNumar).HasMaxLength(100);

                entity.Property(e => e.Tara).HasMaxLength(50);
            });

            modelBuilder.Entity<MedicalReport>(entity =>
            {
                entity.ToTable("MedicalReport");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ReportDate)
                .HasColumnType("datetime")
                .HasColumnName("raportDate");

                entity.Property(e => e.ReportDescription)
                    .HasMaxLength(800)
                    .HasColumnName("reportDescription");

                entity.Property(e => e.MedicalHistory)
                .HasColumnName("medicalHistory")
                .HasMaxLength(800);

                entity.Property(e => e.Condition)
                    .HasColumnName("condition")
                    .HasMaxLength(50);

                entity.Property(e => e.Strategy)
                    .HasColumnName("strategy")
                    .HasMaxLength(800);

                entity.Property(e => e.Prescription)
                .HasColumnName("prescription")
                .HasMaxLength(800);

                entity.Property(e => e.DoctorNotes)
                    .HasColumnName("doctorNotes")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Utilizator)
                    .WithMany(p => p.MedicalReports)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.MedicalReports)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicalReport_SpecialistId");
            });

            modelBuilder.Entity<CameraConferintum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.OraInceput).HasMaxLength(20);

                entity.Property(e => e.OraSfarsit).HasMaxLength(20);

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.CameraConferinta)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialistIdConf");

                entity.HasOne(d => d.Utilizator)
                    .WithMany(p => p.CameraConferinta)
                    .HasForeignKey(d => d.UtilizatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UtilizatorIdConf");
            });

            modelBuilder.Entity<ComentariiDiscutie>(entity =>
            {
                entity.ToTable("ComentariiDiscutie");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Continut).HasColumnName("continut");

                entity.Property(e => e.DataComentariu)
                    .HasColumnType("datetime")
                    .HasColumnName("dataComentariu");
                entity.Property(e => e.CommentReaction)
                    .HasColumnName("commentReaction");
                entity.HasOne(d => d.Discutie)
                    .WithMany(p => p.ComentariiDiscuties)
                    .HasForeignKey(d => d.DiscutieId);


                entity.HasOne(d => d.User)
                    .WithMany(p => p.ComentariiDiscuties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComentariiDiscutie_User_UserId");
            });

          


            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.ToTable("Diagnostic");

                entity.HasIndex(e => e.Denumire, "UQ__Diagnost__19701A4F23232DD2")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denumire).HasMaxLength(50);

                entity.Property(e => e.Descriere).HasMaxLength(100);

                entity.HasMany(d => d.Simptomes)
                    .WithMany(p => p.Diagnostics)
                    .UsingEntity<Dictionary<string, object>>(
                        "SimptomeDiagnostic",
                        l => l.HasOne<Simptome>().WithMany().HasForeignKey("SimptomeId").HasConstraintName("FK_DiagnosticSimptome_Simptome_SimptomeId"),
                        r => r.HasOne<Diagnostic>().WithMany().HasForeignKey("DiagnosticId").HasConstraintName("FK_DiagnosticSimptome_Diagnostic_DiagnosticId"),
                        j =>
                        {
                            j.HasKey("DiagnosticId", "SimptomeId");

                            j.ToTable("SimptomeDiagnostic");
                        });
            });

            modelBuilder.Entity<Discutie>(entity =>
            {
                entity.ToTable("Discutie");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Continut).HasColumnName("continut");

                entity.Property(e => e.DataCreare)
                    .HasColumnType("datetime")
                    .HasColumnName("dataCreare");
                entity.Property(e => e.CommentReaction)
                   .HasColumnName("commentReaction");
                entity.Property(e => e.Titlu)
                    .HasMaxLength(100)
                    .HasColumnName("titlu");

                entity.Property(e => e.Topic)
                    .HasMaxLength(50)
                    .HasColumnName("topic");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discuties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discutie_User_UserId");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("IdentityRole");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("IdentityUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(500);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PhoneNumberCountryPrefix).HasMaxLength(6);
               
                entity.HasOne(d => d.Adresa)
                    .WithMany(p => p.IdentityUsers)
                    .HasForeignKey(d => d.AdresaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdresaId");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "IdentityUserIdentityRole",
                        l => l.HasOne<IdentityRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("IdentityUserIdentityRole");
                        });
               entity.Property(e => e.UserImage).HasColumnType("varbinary(MAX)");
            });

            modelBuilder.Entity<IdentityUserToken>(entity =>
            {
                entity.ToTable("IdentityUserToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RefreshTokenValue).HasMaxLength(800);

                entity.Property(e => e.TokenValue).HasMaxLength(800);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdentityUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<IdentityUserTokenConfirmation>(entity =>
            {
                entity.ToTable("IdentityUserTokenConfirmation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ConfirmationToken).HasMaxLength(500);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdentityUserTokenConfirmations)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<IstoricDiagnosticUtilizator>(entity =>
            {
                entity.ToTable("IstoricDiagnosticUtilizator");

                entity.Property(e => e.DataDiagnosticare).HasColumnType("datetime");

                entity.HasOne(d => d.Diagnostic)
                    .WithMany(p => p.IstoricDiagnosticUtilizators)
                    .HasForeignKey(d => d.DiagnosticId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Utilizator)
                    .WithMany(p => p.IstoricDiagnosticUtilizators)
                    .HasForeignKey(d => d.UtilizatorId);
            });

            modelBuilder.Entity<Programare>(entity =>
            {
                entity.ToTable("Programare");

                entity.Property(e => e.DataProgramare).HasMaxLength(50);


                entity.Property(e => e.StareProgramare).HasMaxLength(20);

                entity.Property(e => e.TipProgramare).HasMaxLength(50);

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Programares)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SpecialistIdProg");

                entity.HasOne(d => d.Utilizator)
                    .WithMany(p => p.Programares)
                    .HasForeignKey(d => d.UtilizatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UtilizatorId");
            });
            

            modelBuilder.Entity<Simptome>(entity =>
            {
                entity.ToTable("Simptome");

                entity.HasIndex(e => e.Denumire, "UQ__Simptome__19701A4F05BDFAFB")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denumire).HasMaxLength(50);

                entity.Property(e => e.Descriere).HasMaxLength(100);
            });

            modelBuilder.Entity<Specialist>(entity =>
            {
                entity.ToTable("Specialist");

                entity.Property(e => e.SpecialistId).ValueGeneratedNever();

                entity.Property(e => e.Specialitate).HasMaxLength(100);

                entity.HasOne(d => d.SpecialistNavigation)
                    .WithOne(p => p.Specialist)
                    .HasForeignKey<Specialist>(d => d.SpecialistId)
                    .HasConstraintName("FK_SpecialistId");
            });

            modelBuilder.Entity<Utilizator>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Utilizator");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Asigurat).HasMaxLength(5);

                entity.Property(e => e.Categorie).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Utilizator)
                    .HasForeignKey<Utilizator>(d => d.UserId)
                    .HasConstraintName("FK_UserId");

                entity.HasMany(d => d.Simptomes)
                    .WithMany(p => p.Utilizators)
                    .UsingEntity<Dictionary<string, object>>(
                        "UtilizatorSimptome",
                        l => l.HasOne<Simptome>().WithMany().HasForeignKey("SimptomeId"),
                        r => r.HasOne<Utilizator>().WithMany().HasForeignKey("UtilizatorId").HasConstraintName("FK_UtilizatorSimptome_Utilizator_UserId"),
                        j =>
                        {
                            j.HasKey("UtilizatorId", "SimptomeId");

                            j.ToTable("UtilizatorSimptome");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
