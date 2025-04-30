Overview
The Bug Ticketing System is a web-based application designed to help teams manage software bugs and issues more efficiently. It supports multiple user roles—Managers, Developers, and Testers—and allows users to report, assign, track, and resolve bugs within different projects. The system also supports uploading attachments (like screenshots) for better communication.

🚀 Features
User registration and login with secure authentication

Project creation and viewing

Bug reporting and detailed bug management

Assigning and unassigning users to bugs

Uploading and managing bug-related attachments

Role-based access and functionality

RESTful API following best practices

🧩 Key Components
Users: Each user can be a Manager, Developer, or Tester, and can be assigned to multiple bugs.
Projects: Each project contains multiple bugs.
Bugs: Each bug can belong to a project, have multiple assignees, and contain attachments.
Attachments: Each attachment is linked to a specific bug and helps with documentation or clarification.

🔐 User Management Endpoints
1. Register User
POST /api/users/register

Purpose: Creates a new user account in the system.

Used by: Anyone (open endpoint).

Expected Input: User’s name, email, password, and role(s).

Behavior:

Validates input.

Saves the user in the database.

Hashes the password securely.

Returns a success message or validation error.

2. Login User
POST /api/users/login

Purpose: Authenticates a user and returns a JWT token.

Used by: Registered users.

Expected Input: Email and password.

Behavior:

Validates credentials.

If correct, generates a JWT token.

Returns token to be used in protected routes.

🗂️ Project Management Endpoints
3. Create Project
POST /api/projects

Purpose: Allows authorized users (like Managers) to create new projects.

Expected Input: Project name and description.

Behavior:

Creates a new project record.

Returns the project details after creation.

4. Get All Projects
GET /api/projects

Purpose: Returns a list of all projects in the system.

Used by: Any authenticated user.

Behavior:

Fetches all projects from the database.

Returns summary info for each.

5. Get Project Details
GET /api/projects/:id

Purpose: Returns detailed information for a specific project.

Behavior:

Finds the project by ID.

Includes related bugs in the response.

Returns 404 if not found.

🐞 Bug Management Endpoints
6. Create Bug
POST /api/bugs

Purpose: Allows users to report new bugs.

Expected Input: Title, description, project ID, severity, etc.

Behavior:

Validates and creates the bug under the specified project.

May auto-assign to the reporting user or leave unassigned.

7. Get All Bugs
GET /api/bugs

Purpose: Lists all bugs in the system.

Used by: Any authenticated user.

Behavior:

Retrieves all bugs.

Can include optional filters (like project, status, severity).

8. Get Bug Details
GET /api/bugs/:id

Purpose: View detailed information about a specific bug.

Behavior:

Includes description, project info, assignees, and attachments.

Returns 404 if the bug ID is not found.

👥 User-Bug Relationship Endpoints
9. Assign User to Bug
POST /api/bugs/:id/assignees

Purpose: Assigns a user to work on a specific bug.

Expected Input: User ID to be assigned.

Behavior:

Adds the user to the bug's assignees list.

Prevents duplicates.

10. Remove User from Bug
DELETE /api/bugs/:id/assignees/:userId

Purpose: Removes a user from the list of people working on a bug.

Behavior:

Unlinks the user from the bug.

Returns a success or not-found message.

📎 Attachment/File Management Endpoints
11. Upload Attachment
POST /api/bugs/:id/attachments

Purpose: Upload a file (e.g., image, log) related to a bug.

Expected Input: Multipart form-data (file upload).

Behavior:

Saves the file to a folder or cloud.

Stores the file path and info in the database.

Links it to the corresponding bug.

12. Get Attachments for Bug
GET /api/bugs/:id/attachments

Purpose: Returns all attachments linked to a specific bug.

Behavior:

Lists files with names, upload dates, and download URLs.

13. Delete Attachment
DELETE /api/bugs/:id/attachments/:attachmentId

Purpose: Deletes a specific attachment from a bug.

Behavior:

Removes the file from storage.

Deletes the database record.

Returns success or error if the attachment doesn’t exist.

🛠️ Technologies Used
ASP.NET Web API

Entity Framework Core

SQL Server

JWT Authentication

N-Tier Architecture
