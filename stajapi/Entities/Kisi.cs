using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Kisi
{
    public int IdKisi { get; set; }

    public string Tc { get; set; } = null!;

    public int DurumKodu { get; set; }

    public int MedeniHalKodu { get; set; }

    public int CinsiyetKodu { get; set; }

    public int DogumYeriKodu { get; set; }

    public int AileKodu { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public virtual Aile Aile { get; set; } = null!;

    public virtual Cinsiyet Cinsiyet { get; set; } = null!;

    public virtual Sehir DogumYeri { get; set; } = null!;

    public virtual Durum Durum { get; set; } = null!;

    public virtual Medenihal MedeniHal { get; set; } = null!;

    public virtual ICollection<OlayGecmisi> OlayGecmisi { get; set; } = new List<OlayGecmisi>();
}
