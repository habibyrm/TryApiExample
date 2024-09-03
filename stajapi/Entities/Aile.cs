using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Aile
{
    public int IdAile { get; set; }

    public int? AileSiraNo { get; set; }

    public int? BireySiraNo { get; set; }

    public string AnneTc { get; set; } = null!;

    public string BabaTc { get; set; } = null!;

    public string? EsTc { get; set; }

    public int CiltKodu { get; set; }

    public virtual Cilt Cilt { get; set; } = null!;

    public virtual ICollection<Kisi> Kisi { get; set; } = new List<Kisi>();
}
