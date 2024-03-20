using System;
using System.Collections.Generic;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class Status
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
