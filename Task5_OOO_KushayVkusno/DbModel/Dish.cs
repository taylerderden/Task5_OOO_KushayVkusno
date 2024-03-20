using System;
using System.Collections.Generic;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class Dish
{
    public int IdDish { get; set; }

    public string DishName { get; set; } = null!;

    public int DishPrice { get; set; }

    public int CategoryIdCategory { get; set; }

    public string? DishSostav { get; set; }

    public byte[]? DishPhoto { get; set; }

    public virtual Category CategoryIdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
