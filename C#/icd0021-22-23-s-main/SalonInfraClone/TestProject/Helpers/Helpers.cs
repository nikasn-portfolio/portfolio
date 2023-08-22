using DAL;
using DAL.Seeding;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace TestProject.Helpers;

public class Helpers
{
    public void DataSeeder(ApplicationDbContext? db, UserManager<AppUser>? userManager,
        RoleManager<AppRole>? roleManager)
    {
        AppDataInit.SeedIdentity(userManager!, roleManager!);
        AppDataInit.SeedAppData(db!, userManager!);
    }
}