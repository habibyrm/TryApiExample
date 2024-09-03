using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Olay
{
    public int IdOlay { get; set; }

    public string? OlayYeri { get; set; }

    public string? Aciklamasi { get; set; }

    public virtual ICollection<OlayGecmisi> OlayGecmisi { get; set; } = new List<OlayGecmisi>();
}
