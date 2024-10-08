﻿using System;
using System.Collections.Generic;

namespace Topic_8_25.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public virtual Order? Order { get; set; }
}
