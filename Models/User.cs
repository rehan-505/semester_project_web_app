using System;
using System.Collections.Generic;

namespace semester_project_web_app.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;
}
