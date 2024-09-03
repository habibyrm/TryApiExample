using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Kullanici
{
    public int IdKullanici { get; set; }

    public string? Ad { get; set; }

    public string? KisiTc { get; set; }

    public string? Parola { get; set; }

    public virtual ICollection<OlayGecmisi> OlayGecmisi { get; set; } = new List<OlayGecmisi>();
}
