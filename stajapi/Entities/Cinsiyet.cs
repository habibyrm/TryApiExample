using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Cinsiyet
{
    public int IdCinsiyet { get; set; }

    public string? Aciklamasi { get; set; }

    public virtual ICollection<Kisi> Kisi { get; set; } = new List<Kisi>();
}
