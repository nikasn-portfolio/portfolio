# aw-struct-train

## Apple Watch Structured Training



# Generate db migration

~~~bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 

# apply migration
dotnet ef database update --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
~~~


# generate rest controllers

Add nuget packages
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
~~~bash
# install tooling
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp
# MVC
dotnet aspnet-codegenerator controller -m Owner -name OwnersController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Pet   -name PetsController   -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
# Rest API
dotnet aspnet-codegenerator controller -m Owner -name OwnersController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Pet   -name PetsController   -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f

# Rest API
dotnet aspnet-codegenerator controller -m Category -name CategoriesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Company -name CompaniesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Discount -name DiscountsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Invoice -name InvoicesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceFooter -name InvoiceFootersController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceRow -name InvoiceRowsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Language -name LanguagesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m PaymentMethod -name PaymentMethodsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Person -name PersonsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Record -name RecordsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Service -name ServicesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m UserRecord -name UserRecordsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f

~~~

~~~bash
# Rest API
dotnet aspnet-codegenerator controller -m Category -name CategoriesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Company -name CompaniesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Discount -name DiscountsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Invoice -name InvoicesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceFooter -name InvoiceFootersController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceRow -name InvoiceRowsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Language -name LanguagesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m PaymentMethod -name PaymentMethodsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Person -name PersonsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Record -name RecordsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Service -name ServicesController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m UserRecord -name UserRecordsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Client -name ClientsController -outDir Api -api -dc ApplicationDbContext  -udl --referenceScriptLibraries -f

# MVC
dotnet aspnet-codegenerator controller -m Category -name CategoriesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Company -name CompaniesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Discount -name DiscountsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Invoice -name InvoicesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceFooter -name InvoiceFootersController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InvoiceRow -name InvoiceRowsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Language -name LanguagesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m PaymentMethod -name PaymentMethodsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Person -name PersonsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Record -name RecordsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Service -name ServicesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m UserRecord -name UserRecordsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f

~~~