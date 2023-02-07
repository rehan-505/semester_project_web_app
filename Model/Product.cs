using System;
using System.Collections.Generic;

namespace semester_project_web_app.Model;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Des { get; set; } = null!;

    public double Price { get; set; }
}
