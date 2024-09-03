using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class OlayGecmisi
{
    public int IdOlayGecmisi { get; set; }

    public int OlayKodu { get; set; }

    public string KisiTc { get; set; } = null!;

    public string? EsTc { get; set; }

    public DateTime? Zaman { get; set; }

    public int? KullaniciId { get; set; }

    public virtual Kisi? EsTcNavigation { get; set; }

    public virtual Kisi KisiTcNavigation { get; set; } = null!;

    public virtual Kullanici? Kullanici { get; set; }

    public virtual Olay Olay { get; set; } = null!;
}
