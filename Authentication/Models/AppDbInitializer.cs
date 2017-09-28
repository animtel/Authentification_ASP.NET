using Authentication;
using Authentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace RolesIdentityApp.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole{ Name = "guest" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            var user = new ApplicationUser {Email = "user@mail.ru", UserName = "user@mail.ru"};
            string passwordUser = "ad46D_ewr3";
            var res = userManager.Create(user, passwordUser);

            var guest = new ApplicationUser {Email = "guest@mail.ru", UserName = "guest@mail.ru"};
            string passwordGuest = "ad46D_ewr3";

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(user.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}