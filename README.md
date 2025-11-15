# 📘 Notes Management System (ASP.NET MVC, .NET 8, Clean Architecture)

A modular, maintainable Notes Management System built using **ASP.NET Core MVC (.NET 8)**, following **Clean Architecture**, and using **Entity Framework Core (SQL Server)** with **Code-First** approach.


# 🧱 Clean Architecture Solution Structure

NotesManagementSystem/
│
├── NotesApp.Domain/
│ └── Entities (Note, Tag, Comment, Priority Enum)
│
├── NotesApp.Application/
│ └── Interfaces, DTOs, Services (NoteService, TagService etc.)
│
├── NotesApp.Infrastructure/
│ ├── AppDbContext (EF Core)
│ ├── Repositories (NoteRepository, TagRepository)
│ ├── Migrations (Code-First)
│ └── Unit of Work
│
└── NotesApp.WebUI/
├── MVC Controllers
├── Views (Create, Edit, List)
└── Dependency Injection Setup


### ⭐ Layer Responsibilities

- **Domain** → Pure business entities (no EF, no dependencies)  
- **Application** → Abstractions, use-cases, service layer  
- **Infrastructure** → EF Core, SQL Server, repositories  
- **WebUI** → MVC presentation, controllers, Razor views  

This ensures separation of concerns and maintainability.

---

# 🗃 Database Entities & Relationships

### **Note**
- Id  
- Title  
- Content  
- Priority (Low, Medium, High)  
- CreatedOn  
- UpdatedOn  

### **Tag**
- Id  
- Name  
- Many-to-many with Notes  

### **Comment**
- Id  
- NoteId  
- Text  
- CreatedOn  

### Relationships
- Note ↔ Tag (Many-to-Many)  
- Note → Comment (One-to-Many)

---

# 🎨 Features

### 📝 Notes Management
- Create, edit, delete notes  
- Notes sorted by **most recently updated**  
- If never updated → shows **“Never Updated”**  
- Inline editing (optional)

### 🎯 Priority System
- Low → Green  
- Medium → Yellow  
- High → Red  

### 📌 Tags
- Add/remove tags  
- Reusable across notes  

### 💬 Comments
- Add comments to a note  
- Stored in SQL Server  

---

# ⚙️ Technologies Used

- **.NET 8**
- **ASP.NET Core MVC**
- **Entity Framework Core 8**
- **SQL Server**
- **Bootstrap 5**
- **Clean Architecture**
- **Repository + Unit of Work Patterns**



# 🚀 How to Run the Project (Local Setup)

   1️⃣ Clone the repository

git clone https://github.com/<your-username>/NotesManagementSystem.git

    2️⃣ Open the solution
NotesManagementSystem.sln
    3️⃣ Update database connection string
NotesApp.WebUI/appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=NotesManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
    4️⃣ Apply migrations
Update-Database -Project NotesApp.Infrastructure
    5️⃣ Run the WebUI project
Press F5 or Ctrl+F5

📸 Screenshots

<img width="1321" height="944" alt="HomePage" src="https://github.com/user-attachments/assets/41084562-70f5-473c-aa2f-a2c8fe6223f4" />
<img width="1326" height="599" alt="image" src="https://github.com/user-attachments/assets/c542f763-2cc1-41bc-8d2d-c3bf176f375d" />
<img width="1307" height="583" alt="image" src="https://github.com/user-attachments/assets/ce4da1c8-1750-4a05-87a9-91b064bcb255" />
<img width="779" height="244" alt="image" src="https://github.com/user-attachments/assets/cbe7a80f-b7d0-4939-a2d3-835644ef2a69" />


👨‍💻 Author
Yash Katiyar
https://github.com/yashkumarkatiyarIN


