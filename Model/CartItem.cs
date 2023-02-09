using SMS.Models;
using System;
using System.Collections.Generic;

namespace semester_project_web_app.Model;

public partial class CartItem: FullAuditModel
{
   

    public int UserId { get; set; }

    public int ProductId { get; set; }
}
