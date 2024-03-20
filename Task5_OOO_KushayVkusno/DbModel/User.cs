using System;
using System.Collections.Generic;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class User
{
    public int IdUser { get; set; }

    public string UserSurname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPatronymic { get; set; } = null!;

    public int RoleIdRole { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role RoleIdRoleNavigation { get; set; } = null!;
}
