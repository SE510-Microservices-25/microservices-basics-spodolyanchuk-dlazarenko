# Lesson 2:

### 0. Lesson goals and grading scope
Setting up Entity Framework Core in an ASP.NET Web API

## Step 1: Install Entity Framework Core Packages
Open **Package Manager Console (PMC)** and run:

```powershell
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
```

Alternatively, using **.NET CLI**:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## Step 2: Define the Model
Create a `Models` folder and add a `Product.cs` file:

```csharp
namespace MyApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

---

## Step 3: Create the DbContext
Inside a `Data` folder, add `AppDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
```

---

## Step 4: Configure the Connection String
In `appsettings.json`, add the database connection string:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyApiDb;Trusted_Connection=True;"
}
```

---

## Step 5: Register EF Core in Dependency Injection (DI)
In `Program.cs` (or `Startup.cs` for older versions):

```csharp
using Microsoft.EntityFrameworkCore;
using MyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

---

## Step 6: Apply Migrations and Update Database
Run the following commands in **Package Manager Console (PMC)**:

```powershell
Add-Migration InitialCreate
Update-Database
```

Or using **.NET CLI**:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Step 7: Create API Controller
Generate a controller to expose CRUD operations.

Run this command:

```bash
dotnet aspnet-codegenerator controller -name ProductsController -async -api -m Product -dc AppDbContext -outDir Controllers
```

Or manually create `ProductsController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }
    }
}
```

---

## Step 8: Run the API
Use the command:

```bash
dotnet run
```

Navigate to `https://localhost:<port>/swagger` to test the API using **Swagger UI**.

---

## Step 9: Test with Postman or Swagger
- **GET**: `https://localhost:<port>/api/products` → Fetch all products.
- **POST**: `https://localhost:<port>/api/products`
  ```json
  {
    "name": "Laptop",
    "price": 1200
  }
  ```
  → Adds a new product.

---