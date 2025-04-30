# 🐞 Bug Ticketing System

## 📌 Overview

The **Bug Ticketing System** is a web-based application designed to help teams manage software bugs and issues more efficiently. It supports multiple user roles—**Managers**, **Developers**, and **Testers**—and allows users to report, assign, track, and resolve bugs across different projects.

The system also supports uploading attachments (like screenshots) for better bug documentation and communication.

---

## 🚀 Features

- ✅ User registration and login with secure JWT authentication  
- 🏗️ Project creation and detailed viewing  
- 🐛 Bug reporting and management (create, update, view)  
- 👥 Assigning and unassigning users to bugs  
- 📎 Uploading and managing attachments related to bugs  
- 🔐 Role-based access control  
- 🌐 RESTful API following best practices  

---

## 🧩 Key Components

- **Users**: Each user has a role (Manager, Developer, or Tester) and can be assigned to multiple bugs.  
- **Projects**: A project contains multiple bugs and groups them logically.  
- **Bugs**: Each bug belongs to a project, can have multiple assignees, and may include attachments.  
- **Attachments**: Files (e.g., images, logs) linked to bugs to help clarify or explain issues.  

---

## 🔐 User Management Endpoints

### 📍 1. Register User  
**POST** `/api/users/register`  
Creates a new user account.  
- Input: Name, full name, Password, Role(s)  
- Behavior:
  - Validates data
  - Hashes password
  - Saves to database
  - Returns success or error message  

### 📍 2. Login User  
**POST** `/api/users/login`  
Authenticates a user and returns a JWT token.  
- Input: Email, Password  
- Behavior:
  - Validates credentials
  - Returns JWT for use in protected endpoints  

---

## 🗂️ Project Management Endpoints

### 📍 3. Create Project  
**POST** `/api/projects`  
Creates a new software project.  
- Input:Id, Name, Description  
- Output: Project details  

### 📍 4. Get All Projects  
**GET** `/api/projects`  
Returns a list of all available projects.  
- Behavior: Fetches project summaries  

### 📍 5. Get Project Details  
**GET** `/api/projects/:id`  
Retrieves detailed info of a specific project.  
- Includes: Project name, description, related bugs  
- Returns `404` if project is not found  

---

## 🐞 Bug Management Endpoints

### 📍 6. Create Bug  
**POST** `/api/bugs`  
Reports a new bug related to a project.  
- Input:Id,Title, Description, Project ID, etc.  
- Behavior: Validates and saves bug data  

### 📍 7. Get All Bugs  
**GET** `/api/bugs`  
Lists all bugs   
- Output: Array of bug summaries  

### 📍 8. Get Bug Details  
**GET** `/api/bugs/:id`  
Shows detailed information about a specific bug.  
- Includes: Description, project, assignees, attachments  
- Returns `404` if bug is not found  

---

## 👥 User-Bug Relationship Endpoints

### 📍 9. Assign User to Bug  
**POST** `/api/bugs/:id/assignees`  
Assigns a user to a specific bug.  
- Input: User ID  
- Behavior: Adds user to the bug’s assignees list  

### 📍 10. Remove User from Bug  
**DELETE** `/api/bugs/:id/assignees/:userId`  
Unassigns a user from a bug.  
- Behavior: Removes user from bug’s assignees  
- Returns success or not found message  

---

## 📎 Attachment Management Endpoints

### 📍 11. Upload Attachment  
**POST** `/api/bugs/:id/attachments`  
Uploads a file (e.g., screenshot) related to a bug.  
- Input: File via multipart form-data  
- Behavior:  
  - Saves file to storage  
  - Links file to bug in DB  

### 📍 12. Get Attachments for Bug  
**GET** `/api/bugs/:id/attachments`  
Retrieves all attachments linked to a specific bug.  
- Output: List of files with metadata  

### 📍 13. Delete Attachment  
**DELETE** `/api/bugs/:id/attachments/:attachmentId`  
Deletes a specific file linked to a bug.  
- Behavior:  
  - Removes file from storage  
  - Deletes DB record  

---

## 🛠️ Technologies Used

- 💻 **ASP.NET Core Web API**  
- 🗃️ **Entity Framework Core**  
- 🛢️ **SQL Server**  
- 🔐 **JWT Authentication**  
- 🧱 **N-Tier Architecture**

---
