using System;
using System.Collections.Generic;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class Category
{
    public int IdCategory { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
