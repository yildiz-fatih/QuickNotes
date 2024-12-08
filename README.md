# **QuickNotes**

## **Why I Did This?**
I created this project to transition from building ASP.NET Core Web APIs to ASP.NET Core MVC.

---
## **How to Use the Project**

### **Prerequisites**
1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Set up a MySQL database and update the connection string in `appsettings.json`.

### **Steps to Run**
1. Clone the repository:
   ```bash
   git clone https://github.com/fatihyd/QuickNotes.git
   ```

2. Navigate to the project directory:
   ```bash
   cd QuickNotes
   ```

3. Apply migrations to set up the database:
   ```bash
   dotnet ef database update --project QuickNotes.Data
   ```

4. Run the application:
   ```bash
   dotnet run --project QuickNotes.Web
   ```

5. Open the application in your browser at `http://localhost:5127` (or the port specified in the console).

---

## **What I Learned**
- MVC Controllers and Action Methods
- Razor Views
- ViewModels
- Forms and Model Binding
- Basic Routing
- Tag Helpers

---

## **What Next...**
### **to learn**
- Validation
- Advanced Routing
- Partial Views and View Components
- ViewData, ViewBag, TempData...
### **to do**
- Validation
- Filters
- Custom Error Pages
- Authentication and Authorization
