using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace stajapi.Entities;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aile> Aile { get; set; }

    public virtual DbSet<Cilt> Cilt { get; set; }

    public virtual DbSet<Cinsiyet> Cinsiyet { get; set; }

    public virtual DbSet<Durum> Durum { get; set; }

    public virtual DbSet<Kisi> Kisi { get; set; }

    public virtual DbSet<Kullanici> Kullanici { get; set; }

    public virtual DbSet<Medenihal> Medenihal { get; set; }

    public virtual DbSet<Olay> Olay { get; set; }

    public virtual DbSet<OlayGecmisi> OlayGecmisi { get; set; }

    public virtual DbSet<Sehir> Sehir { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localHost;Port=5432;Database=stajdeneme;Username=postgres;Password=abc123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Aile>(entity =>
        {
            entity.HasKey(e => e.IdAile).HasName("aile_id_pk");

            entity.ToTable("aile");

            entity.HasIndex(e => new { e.AileSiraNo, e.BireySiraNo }, "ailebireyUnique").IsUnique();

            entity.Property(e => e.IdAile).HasColumnName("id_aile");
            entity.Property(e => e.AileSiraNo).HasColumnName("aile_sira_no");
            entity.Property(e => e.AnneTc)
                .HasMaxLength(11)
                .HasColumnName("anne_tc");
            entity.Property(e => e.BabaTc)
                .HasMaxLength(11)
                .HasColumnName("baba_tc");
            entity.Property(e => e.BireySiraNo).HasColumnName("birey_sira_no");
            entity.Property(e => e.CiltKodu)
                .HasDefaultValue(1)
                .HasColumnName("cilt_kodu");
            entity.Property(e => e.EsTc)
                .HasMaxLength(11)
                .HasColumnName("es_tc");

            entity.HasOne(d => d.Cilt).WithMany(p => p.Aile)
                .HasForeignKey(d => d.CiltKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aile_sinifi_cilt_fk");
        });

        modelBuilder.Entity<Cilt>(entity =>
        {
            entity.HasKey(e => e.IdCilt).HasName("cilt_id_pk");

            entity.ToTable("cilt");

            entity.Property(e => e.IdCilt).HasColumnName("id_cilt");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
            entity.Property(e => e.SehirKodu).HasColumnName("sehir_kodu");

            entity.HasOne(d => d.Sehir).WithMany(p => p.Cilt)
                .HasPrincipalKey(p => p.Kodu)
                .HasForeignKey(d => d.SehirKodu)
                .HasConstraintName("cilt_sinifi_sehir_fk");
        });

        modelBuilder.Entity<Cinsiyet>(entity =>
        {
            entity.HasKey(e => e.IdCinsiyet).HasName("cinsiyet_id_pk");

            entity.ToTable("cinsiyet");

            entity.Property(e => e.IdCinsiyet).HasColumnName("id_cinsiyet");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
        });

        modelBuilder.Entity<Durum>(entity =>
        {
            entity.HasKey(e => e.IdDurum).HasName("durum_id_pk");

            entity.ToTable("durum");

            entity.Property(e => e.IdDurum).HasColumnName("id_durum");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
        });

        modelBuilder.Entity<Kisi>(entity =>
        {
            entity.HasKey(e => e.IdKisi).HasName("kisi_pkey");

            entity.ToTable("kisi");

            entity.HasIndex(e => e.Tc, "kisiUnique").IsUnique();

            entity.Property(e => e.IdKisi).HasColumnName("id_kisi");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .HasColumnName("ad");
            entity.Property(e => e.AileKodu).HasColumnName("aile_kodu");
            entity.Property(e => e.CinsiyetKodu).HasColumnName("cinsiyet_kodu");
            entity.Property(e => e.DogumYeriKodu).HasColumnName("dogum_yeri_kodu");
            entity.Property(e => e.DurumKodu).HasColumnName("durum_kodu");
            entity.Property(e => e.MedeniHalKodu).HasColumnName("medeni_hal_kodu");
            entity.Property(e => e.Soyad)
                .HasMaxLength(50)
                .HasColumnName("soyad");
            entity.Property(e => e.Tc)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("tc");

            entity.HasOne(d => d.Aile).WithMany(p => p.Kisi)
                .HasForeignKey(d => d.AileKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kisi_sinifi_aile_fk");

            entity.HasOne(d => d.Cinsiyet).WithMany(p => p.Kisi)
                .HasForeignKey(d => d.CinsiyetKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kisi_sinifi_cinsiyet_fk");

            entity.HasOne(d => d.DogumYeri).WithMany(p => p.Kisi)
                .HasForeignKey(d => d.DogumYeriKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kisi_sinifi_dogumyeri_fk");

            entity.HasOne(d => d.Durum).WithMany(p => p.Kisi)
                .HasForeignKey(d => d.DurumKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kisi_sinifi_durum_fk");

            entity.HasOne(d => d.MedeniHal).WithMany(p => p.Kisi)
                .HasForeignKey(d => d.MedeniHalKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kisi_sinifi_medenihal_fk");
        });

        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.HasKey(e => e.IdKullanici).HasName("kullanici_pkey");

            entity.ToTable("kullanici");

            entity.HasIndex(e => e.KisiTc, "kisi_tc_unique").IsUnique();

            entity.Property(e => e.IdKullanici).HasColumnName("id_kullanici");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .HasColumnName("ad");
            entity.Property(e => e.KisiTc)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("kisi_tc");
            entity.Property(e => e.Parola)
                .HasMaxLength(200)
                .HasColumnName("parola");
        });

        modelBuilder.Entity<Medenihal>(entity =>
        {
            entity.HasKey(e => e.IdMedenihal).HasName("medenihal_id_pk");

            entity.ToTable("medenihal");

            entity.Property(e => e.IdMedenihal).HasColumnName("id_medenihal");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
        });

        modelBuilder.Entity<Olay>(entity =>
        {
            entity.HasKey(e => e.IdOlay).HasName("olay_id_pk");

            entity.ToTable("olay");

            entity.Property(e => e.IdOlay).HasColumnName("id_olay");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
            entity.Property(e => e.OlayYeri)
                .HasMaxLength(50)
                .HasColumnName("olay_yeri");
        });

        modelBuilder.Entity<OlayGecmisi>(entity =>
        {
            entity.HasKey(e => e.IdOlayGecmisi).HasName("olaygecmisi_sinifi_id_pk");

            entity.ToTable("olay_gecmisi");

            entity.Property(e => e.IdOlayGecmisi).HasColumnName("id_olay_gecmisi");
            entity.Property(e => e.EsTc)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("es_tc");
            entity.Property(e => e.KisiTc)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("kisi_tc");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.OlayKodu).HasColumnName("olay_kodu");
            entity.Property(e => e.Zaman)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("zaman");

            entity.HasOne(d => d.EsTcNavigation).WithMany(p => p.OlayGecmisi)
                .HasPrincipalKey(p => p.Tc)
                .HasForeignKey(d => d.EsTc)
                .HasConstraintName("olaygecmisi_sinifi_estc_fk");
            // 'KisiTcNavigation' navigasyon özelliğini göz ardı et
            modelBuilder.Entity<OlayGecmisi>()
                .Ignore(og => og.EsTcNavigation);
            entity.HasOne(d => d.KisiTcNavigation).WithMany(p => p.OlayGecmisi)
                .HasPrincipalKey(p => p.Tc)
                .HasForeignKey(d => d.KisiTc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("olaygecmisi_sinifi_kisitc_fk");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.OlayGecmisi)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("olaygecmisi_sinifi_kullanici_id_fk");

            entity.HasOne(d => d.Olay).WithMany(p => p.OlayGecmisi)
                .HasForeignKey(d => d.OlayKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("olaygecmisi_sinifi_olaykodu_fk");
        });

        modelBuilder.Entity<Sehir>(entity =>
        {
            entity.HasKey(e => e.IdSehir).HasName("sehir_id_pk");

            entity.ToTable("sehir");

            entity.HasIndex(e => e.Kodu, "sehirunique").IsUnique();

            entity.Property(e => e.IdSehir).HasColumnName("id_sehir");
            entity.Property(e => e.Aciklamasi)
                .HasMaxLength(50)
                .HasColumnName("aciklamasi");
            entity.Property(e => e.Kodu)
                .IsRequired()
                .HasColumnName("kodu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
