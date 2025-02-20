using System;
using System.Collections.Generic;

namespace Demo_SQL.Entities;

public partial class UserTable
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreateTime { get; set; }
}
