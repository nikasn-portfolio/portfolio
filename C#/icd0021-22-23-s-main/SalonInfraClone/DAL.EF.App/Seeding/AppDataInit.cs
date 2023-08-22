using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JSType = System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("bc7458ac-cbb0-4ecd-be79-d5abf19f8c77");
    private static (Guid id, string email, string pwd, string firstName, string lastName)[] _userDataArray = new (Guid, string, string, string, string)[10];
    
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if(userManager.Users.Any()) return;
        (Guid id, string email, string pwd, string firstName, string lastName) userData = (adminId, "admin@app.com", "a", "Nikita", "Kasnikov");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        // add first default user to array
        _userDataArray[0] = userData;
        for (int i = 1; i <= 5; i++)
        {
            Guid id = Guid.NewGuid();
            string email = RandomString(5) + "@" + "app" + ".com";
            string pwd = "a";
            string firstName = RandomString(6);
            string lastName = RandomString(6);

            // Store the generated data in the array
            _userDataArray[i] = (id, email, pwd, firstName, lastName);
        }
        if (user == null)
        {
            for (int i = 0; i <= 5; i++)
            {
                user = new AppUser()
                {
                    Id = _userDataArray[i].id,
                    Email = _userDataArray[i].email,
                    UserName = _userDataArray[i].email,
                    FirstName = _userDataArray[i].firstName,
                    LastName = _userDataArray[i].lastName,
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(user, userData.pwd).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Cannot seed users, {result.ToString()}");
                }
            }

        }
        // create roles
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new AppRole
            {
                Name = "Admin"
            };
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed roles, {result.ToString()}");
            }
        }
        if (!roleManager.RoleExistsAsync("Administrator").Result)
        {
            var role = new AppRole
            {
                Name = "Administrator"
            };
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed roles, {result.ToString()}");
            }
        }
        if (!roleManager.RoleExistsAsync("Worker").Result)
        {
            var role = new AppRole
            {
                Name = "Worker"
            };
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed roles, {result.ToString()}");
            }
        }
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            var role = new AppRole
            {
                Name = "User"
            };
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed roles, {result.ToString()}");
            }
        }
        //assign role to users
        user = userManager.FindByEmailAsync("admin@app.com").Result;

        if (user != null)
        {
            var userToRole = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!userToRole.Succeeded)
            {
                throw new ApplicationException($"Cannot assign role to user, {userToRole.ToString()}");
            } 
        }
        

        for (int i = 1; i <= 5; i++)
        {
            user = userManager.FindByEmailAsync(_userDataArray[i].email).Result;
            if (user != null)
            {
                IdentityResult? result;
                if (i == _userDataArray.Length - 1)
                {
                    result = userManager.AddToRoleAsync(user, "User").Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException($"Cannot assign role to user, {result.ToString()}");
                    }
                }
                user = userManager.FindByEmailAsync(_userDataArray[i].email).Result;
                result = userManager.AddToRoleAsync(user!, "Worker").Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Cannot assign role to user, {result.ToString()}");
                }
            }
        }
    }

    public static void SeedAppData(ApplicationDbContext context,UserManager<AppUser> userManager)
    {
        SeedAppLanguages(context);
        context.SaveChanges();
        SeedAppDataPersons(context,userManager);
        SeedAppDataCategories(context);
        context.SaveChanges();
        SeedAppDataClients(context);
        context.SaveChanges();
        SeedAppDataRecords(context,userManager);
        context.SaveChanges();
        SeedAppDataServices(context);
        context.SaveChanges();
        SeedAppRecordService(context);
        context.SaveChanges();
        SeedAppCompanies(context);
        context.SaveChanges();
        SeedAppPaymentMethods(context);
        context.SaveChanges();
        SeedAppInvoiceFooters(context);
        context.SaveChanges();
        SeedAppInvoices(context,userManager);
        context.SaveChanges();

    }

    private static  void SeedAppDataPersons(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if (context.Persons.Any()) return;
        var listOfUsers = Task.FromResult(userManager.Users.ToArray()).Result;
        var listOfLanguages = context.Languages.ToList();
        for (int i = 0; i < listOfUsers.Length; i++)
        {
            context.Persons.Add(
                new Person()
                {
                    AppUserId = listOfUsers[i].Id,
                    LanguageId = listOfLanguages.ElementAt(RandomNumberGenerator.GetInt32(2)).Id,
                    PersonName = "Nikita" + i.ToString(),
                    PersonSurname = "Kasnikov" + i.ToString(),
                    PersonPhoneNumber = "+372" + RandomNumberString(6)
            
                });
            //1
        }

        
    }
    
    private static  void SeedAppDataClients(ApplicationDbContext context)
    {
        if(context.Clients.Any()) return;
        for (int i = 0; i < 5; i++)
        {
            var client = new Client()
            {
                FirstName = RandomString(5),
                LastName = RandomString(5),
                PhoneNumber = "+372" + RandomNumberString(5)
            };
            context.Clients.Add(client);

        }

    }

    private static  void SeedAppDataCategories(ApplicationDbContext context)
    {
        if(context.Categories.Any()) return;
        context.Categories.Add(
            new Category()
            {
                CategoryName = "For man",
                CategoryImageUrl = "https://png.pngtree.com/element_pic/00/16/07/29579adcfd54b43.jpg"
            }
        );
        //1
        context.Categories.Add(
            new Category()
            {
                CategoryName = "For Woman",
                CategoryImageUrl = "https://qph.cf2.quoracdn.net/main-qimg-52a9fe2e87e1d2c33b7a8e4ffde9e61b"
            }
        );
        //1

    }
    
    

    private static  void SeedAppDataServices(ApplicationDbContext context)
    {
        if(context.Services.Any()) return;
        var listOfCategories = context.Categories.ToList();
        var listOfRecords = context.Records.ToList();
        var lowerBound = 0; 
        var upperBound = 1;
        for (int i = 0; i < 5; i++)
        {
            context.Services.Add(new Service()
            {
                CategoryId = listOfCategories.ElementAt(RandomNumberGenerator.GetInt32(lowerBound,upperBound)).Id,
                ServiceName = "Random Service" + i,
                ServiceDuration = new TimeSpan(0,30,0),
                ServicePrice = 20,
            });
            //1
        }
    }
    
    private static  void SeedAppDataRecords(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if(context.Records.Any()) return;
        
        // Define the timezone you want to convert to
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        

// Convert the date to the target timezone
        DateTime targetDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetTimeZone);

// Add 3 hours to the target date
        targetDate = targetDate.AddHours(7);


        var listOfServices = context.Services.ToList();
        var listOfClients = context.Clients.ToList();
        var dateS = targetDate;
        var dateE = targetDate;
        for (int i = 0; i < 5; i++)
        {
            var record = context.Records.Add(new Record()
            {
                ClientId = listOfClients.ElementAt(i).Id,
                AppUserId = userManager.Users.ToList().ElementAt(i).Id,
                Title = "test" + i,
                StartTime = dateS,
                EndTime = dateE,
                Comment = "testComment" + i

            });
            //1
        }
    }

    private static void SeedAppRecordService(ApplicationDbContext context)
    {
        if(context.RecordServices.Any()) return;
        var listOfRecords = context.Records.ToList();
        var listOfServices = context.Services.ToList();
        for (int i = 0; i < listOfServices.Count; i++)
        {
            var recordService = new RecordService()
            {
                RecordId = listOfRecords.ElementAt(i).Id,
                Record = listOfRecords.ElementAt(i),
                Service = listOfServices.ElementAt(i),
                ServiceId = listOfServices.ElementAt(i).Id
            };
            context.RecordServices.Add(recordService);
        }
    }


    /*private static  void SeedAppUserRecords(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if(context.UserRecords.Any()) return;

        var listOfRecords = context.Records.ToList();
        var listOfUsers = userManager.Users.ToList();
        
        for (int i = 0; i < 5; i++)
        {
            context.UserRecords.Add(new UserRecord()
            {
                AppUserId = listOfUsers.ElementAt(RandomNumberGenerator.GetInt32(4)).Id,
                RecordId = listOfRecords.ElementAt(i).Id
                
            });
            //1
        }
    }*/


    private static  void SeedAppLanguages(ApplicationDbContext context)
    {
        if(context.Languages.Any()) return;
        
        
            context.Languages.Add(new Language()
            {
                LanguageName = "EN"
                
            });
            //1
            context.Languages.Add(new Language()
            {
                LanguageName = "ES"
                
            });
            //1
            context.Languages.Add(new Language()
            {
                LanguageName = "RU"
                
            });
            //1

    }



    private static  void SeedAppCompanies(ApplicationDbContext context)
    {
        if(context.Companies.Any()) return;

        for (int i = 0; i < 2; i++)
        {
            context.Companies.Add(new Company()
            {
                CompanyName = RandomString(6),
                Address = RandomString(6),
                VatNumber = RandomNumberString(10),
                IdentificationCode = RandomNumberString(12)
            });
            //1
        }
        
    }

    private static  void SeedAppPaymentMethods(ApplicationDbContext context)
    {
        if(context.PaymentMethods.Any()) return;
        
        context.PaymentMethods.Add(new PaymentMethod()
        {
            PaymentMethodName = "MasterCard"
        });
        //1
        
        context.PaymentMethods.Add(new PaymentMethod()
        {
            PaymentMethodName = "Visa"
        });
        //1
        
        context.PaymentMethods.Add(new PaymentMethod()
        {
            PaymentMethodName = "Cash"
        });
        //1
    }

    private static  void SeedAppInvoiceFooters(ApplicationDbContext context)
    {
        if(context.InvoiceFooters.Any()) return;
        
        for (int i = 0; i < 2; i++)
        {
            context.InvoiceFooters.Add(new InvoiceFooter()
            {
                CompanyName = RandomString(6),
                Address = RandomString(6),
                Iban = RandomNumberString(10),
                Email = RandomString(4) + "@app.com"
            });
            //1
        }
    }

    private static  void SeedAppInvoices(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if(context.Invoices.Any()) return;

        
        var listOfInvoicesFooters = context.InvoiceFooters.ToList();
        var listOfRecords = context.Records.ToList();
        var listOfPaymentMethods = context.PaymentMethods.ToList();
        var listOfCompanies = context.Companies.ToList();
        var listOfUsers = userManager.Users.ToArray();
        var listOfClients = context.Clients.ToList();
        
            context.Invoices.Add(new Invoice()
            {
                AppUserId = listOfUsers.ElementAt(0).Id,
                ClientId = listOfClients.ElementAt(0).Id,
                InvoiceFooterId = listOfInvoicesFooters.ElementAt(0).Id,
                RecordId = listOfRecords.ElementAt(0).Id,
                PaymentMethodId = listOfPaymentMethods.ElementAt(0).Id,
                InvoiceNumber = RandomString(20),
            });
            //1
        
        
        
        context.Invoices.Add(new Invoice()
        {
            AppUserId = listOfUsers.ElementAt(1).Id,
            ClientId = listOfClients.ElementAt(1).Id,
            CompanyId = listOfCompanies.ElementAt(0).Id,
            InvoiceFooterId = listOfInvoicesFooters.ElementAt(1).Id,
            RecordId = listOfRecords.ElementAt(1).Id,
            PaymentMethodId = listOfPaymentMethods.ElementAt(1).Id,
            InvoiceNumber = RandomString(20),
            IsCompany = true
        });
        //1
    }


    
    // Helper method to generate a random string of a given length
    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Helper method to generate a random integer between a given range
    private static string RandomNumberString(int length)
    {
        const string chars = "0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}






