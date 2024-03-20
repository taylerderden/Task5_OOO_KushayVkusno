using System;
using System.Collections.Generic;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class Order
{
    public int IdOrder { get; set; }

    public int UserIdUser { get; set; }

    public DateOnly OrderDate { get; set; }

    public string OrderTime { get; set; } = null!;

    public int OrderStol { get; set; }

    public int DishIdDish { get; set; }

    public string OrderDesc { get; set; } = null!;

    public int StatusIdStatus { get; set; }

    public virtual Dish DishIdDishNavigation { get; set; } = null!;

    public virtual Status StatusIdStatusNavigation { get; set; } = null!;

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
