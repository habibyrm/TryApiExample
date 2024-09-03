using System;
using System.Collections.Generic;

namespace stajapi.Entities;

public partial class Medenihal
{
    public int IdMedenihal { get; set; }

    public string? Aciklamasi { get; set; }

    public virtual ICollection<Kisi> Kisi { get; set; } = new List<Kisi>();
}
