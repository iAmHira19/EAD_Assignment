# 🎓 Exam Management System

A comprehensive, secure, and scalable **Exam Management System** designed for academic institutions to automate and streamline exam-related administrative processes such as **batch management**, **room allocation**, **conflict-free scheduling**, and **attendance sheet generation**. Developed using **ASP.NET Core** with **Entity Framework Core**, and styled with **Tailwind CSS/Bootstrap**, this web-based solution enforces strict **role-based access control (RBAC)** and adheres to modern software engineering best practices.

---

## 🚀 Features

- ✅ **Batch Management**
  - Add students manually or via Excel import
  - CRUD operations: Add, Edit, Delete, View
  - Filter/search students by any attribute
- ✅ **Room Management**
  - Manage rooms with ID, name, and capacity
  - Full CRUD functionality and filtering
- ✅ **Exam Scheduling & Room Allocation**
  - Select batch, section, and room from dropdowns
  - Validate room capacity vs student count
  - Generate conflict-free seating plans
  - Export attendance sheets in **Excel/PDF**
- ✅ **Role-Based Access Control (RBAC)**
  - **Super Admin**: Manage users and roles
  - **Admin**: Manage batches and rooms
  - **Clerk**: Generate seating and attendance sheets
- ✅ **Secure Authentication**
  - Login, logout, forgot password, change password
- ✅ **Modern UI**
  - Built using **Tailwind CSS** or **Bootstrap**
  - Fully responsive and user-friendly interface
- ✅ **Backend Architecture**
  - **ASP.NET Core** + **Entity Framework Core**
  - Clean MVC pattern with layered design
  - Dependency injection & robust exception handling

---

## 🏗️ Project Structure

ExamManagementSystem/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── Data/
├── Services/
├── wwwroot/
└── Program.cs / Startup.cs


---

## 🛠️ Technologies Used

- **ASP.NET Core**
- **Entity Framework Core (EF Core)**
- **Tailwind CSS** / **Bootstrap**
- **SQL Server**
- **C#**
- **HTML5 / CSS3 / JavaScript**
- **Excel/PDF export libraries**

---

## 📂 Modules

### 🔹 1. Batch Module
- Courses, sections, and students management
- Student Fields: ID, Name, Roll No., Phone, Email, Address, Age, Gender, CNIC

### 🔹 2. Room Management
- Room Fields: ID, Name, Capacity

### 🔹 3. Room Allocation & Scheduling
- Input: Batch, Section, Room
- Output: 
  - Seating Plan: Room No., Course, Date/Time, Students
  - Attendance Sheet: Empty signature fields

---

## 🧑‍💻 User Roles

| Role         | Permissions                                |
|--------------|---------------------------------------------|
| Super Admin  | Add/Remove users, assign/change roles       |
| Admin        | Manage batches and rooms                    |
| Clerk        | Generate seating and attendance documents   |

---

## 📥 Installation Instructions

1. **Clone the repository**  
   ```bash
   git clone https://github.com/iAmHira19/EAD_Assignment.git
   cd EAD_Assignment

   Setup the database
Update your appsettings.json with your local SQL Server credentials and run migrations:

dotnet ef database update
Run the project

dotnet run
Access the system via browser
Navigate to https://localhost:5001 or the assigned port

📌 Future Improvements
LMS integration (Learning Management Systems)

Cloud deployment support (Azure, AWS)

Email notifications for students and staff

Real-time exam invigilation dashboard

📧 Contact
Developed by: Hira Amanat
📧 Email: hiraamanatali19@gmail.com
🔗 LinkedIn: linkedin.com/in/hira-amanat-800104245

📃 License
This project is licensed under the MIT License.









