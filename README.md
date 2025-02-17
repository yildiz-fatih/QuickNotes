# 📝 QuickNotes

QuickNotes is a simple note-taking app

I developed this project to practice the Model-View-Controller (MVC) design pattern, layered architecture and cookie-based authentication.

## **How to Use the App**

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
