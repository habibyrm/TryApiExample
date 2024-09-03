using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Cilt
{
    public int IdCilt { get; set; }

    public int? SehirKodu { get; set; }

    public string? Aciklamasi { get; set; }

    public virtual ICollection<Aile> Aile { get; set; } = new List<Aile>();

    public virtual Sehir? Sehir { get; set; }
}
