using System;
using System.Collections.Generic;

namespace semester_project_web_app.Model;

public partial class CartItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }
}
