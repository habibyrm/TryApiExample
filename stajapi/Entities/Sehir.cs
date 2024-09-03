using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Sehir
{
    public int IdSehir { get; set; }

    public int? Kodu { get; set; }

    public string? Aciklamasi { get; set; }

    public virtual ICollection<Cilt> Cilt { get; set; } = new List<Cilt>();

    public virtual ICollection<Kisi> Kisi { get; set; } = new List<Kisi>();
}
