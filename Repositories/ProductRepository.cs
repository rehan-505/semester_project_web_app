using Microsoft.EntityFrameworkCore;

namespace semester_project_web_app.Model
{
    public class ProductRepository
    {

       public List<Product> getProducts() {

            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Products.Load();

            return foodDbc.Products.ToList();

        }
    }
}
