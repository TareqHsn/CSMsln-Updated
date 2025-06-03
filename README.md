Assalamuyalaikum,

Project Overview:
Welcome to my project repository. This is a task management application built to demonstrate my coding skills and proficiency in .NET Core development. Below, you will find a detailed guide to set up and run the project.

Technologies Used:
Backend: .NET Core (MVC) Version 8
Database: Microsoft SQL Server (MSSQL)

Setup Instructions:

To get started with the project, please follow these steps:

Configure the Database:
Update the MSSQL server name in the appsettings.json file to match your database configuration.

Apply Database Migrations:
Open the Package Manager Console in Visual Studio.
Set the default project to CSM.Infrastructure.
Run the command update-database to apply the existing migrations. (Note: No need to run add-migration as migrations are already included.)

Run the Application:
Launch the project in your preferred IDE.

Navigate to the registration page and click Register as a new user to create an account. You can create multiple accounts for task assignment purposes.

Email Confirmation:
After registration, locate the "Click here to confirm your account" link in the confirmation email to verify your account.

Access the Dashboard:
Once logged in, click on the Dashboard from the side navigation bar to access the main application interface.

Task Management:

Navigate to the Tasks section to perform CRUD operations:
Insert: Create new tasks.
Update: Modify existing tasks.
Delete: Remove tasks as needed.

Notes:
Ensure your MSSQL server is running and accessible before starting the application.

For any issues during setup, please verify the database connection string and ensure all dependencies are correctly installed.

Thank you for reviewing my project. For any questions or feedback, please feel free to reach out.
