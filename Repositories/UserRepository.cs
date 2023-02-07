using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace semester_project_web_app.Model
{
    public class UserRepository
    {

        public List<User> getUsers()
        {
            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Users.Load();

            return foodDbc.Users.ToList();
        }
        public void addUser(User user,IWebHostEnvironment webHostEnvironment) {

            //string fileName = Path.GetFileNameWithoutExtension(user.Image);
            //string extension = Path.GetExtension(user.Image);
            //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //user.Image = "~/Image/" + fileName;
            //string contentRootPath = webHostEnvironment.ContentRootPath;
            //string path = "";
            //path = Path.Combine(contentRootPath , "Images");

            

            //user.ImageFile.ToString();


            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Add(user);
            foodDbc.SaveChanges();


        }



    }
}
