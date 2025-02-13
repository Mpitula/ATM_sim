# ATM Simulation
An ATM (Automated Teller Machine) simulation for students is a simplified, educational version of real-world banking systems designed to help students understand basic banking operations. Here's a breakdown of how it works:

# Key Features
+ User Authentication: Students log in using a simulated account number and PIN. Ensures security and personalized access.
+ Account Overview: Displays account balance and basic details. It helps students track their virtual funds.
+ Deposit Simulation: Students can "deposit" virtual money into their account. Teaches the concept of adding funds to an account.
+ Withdrawal Simulation: Students can "withdraw" virtual money, ensuring they don’t exceed their balance.

# Introduces the idea of managing funds responsibly.

+ Balance Inquiry: Students can check their account balance at any time. Reinforces the importance of monitoring finances.
+ Transaction History: Displays a list of recent transactions (deposits, withdrawals). Aid students understand the flow of money.
+ Simple Interface: Designed to be user-friendly, with clear menus and instructions. Suitable for students with no prior banking experience.

# Getting Started

+ Download the Source Code: Obtain the complete source code of the ATM simulation system from the repository or appropriate source.
+ Install Visual Studio: Ensure that you have Visual Studio (preferably the latest version) installed on your computer. You can download it from Visual Studio's official website.
+ Install SQL Server: The application uses an SQL Server database, ensure SQL Server is installed on your system. You can use SQL Server Express which is free from the Microsoft site.
+ Create a Database: Open SQL Server Management Studio (SSMS). Create a new database named ATMDb, and attach the ATMDb.MDF file located in the project directory to the new database.
+ Setting Up Connection String: Open the project in Visual Studio.
If necessary, adjust the connection string within the code to point to the location of the database file on your machine.
+ Modify this line if your database path differs: SqlConnection conn = new SqlConnection(@"Data Source(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\path_to\ATMDb.mdf"";Integrated Security=True");
+ Build the Project: Once the database is set up and the connection string is updated, build the project using Ctrl + Shift + B or click on Build from the menu.
+ Run the Application: Start the application by pressing F5 or selecting Debug > Start Debugging.  The application should load up and display the login screen.
+ First-Time Setup: Upon first launch, you may need to create an admin user or student account as prompted. 

# Technical Requirements

1. Operating System: Windows 10 or above
2. Processor: Dual-core processor, 1.8 GHz or faster
3. RAM: Minimum 4 GB RAM (8 GB recommended)
4. HDD Space: At least 500 MB of free space
5. Software: The latest version of Visual Studio. SQL Server (Express or LocalDB)
 **Recommended System**
1. Operating System: Windows 10 or above
2. Processor: Quad-core processor, 2.5 GHz or faster
4. RAM: 8 GB or more
5. HDD Space: 1 GB or more for installation and data storage
6. Software: Latest version of Visual Studio, SQL Server (Express or LocalDB), Features, User authentication (Student and Admin), Student registration, Password recovery functionality, Balance inquiry, deposits, and withdrawals, Transaction management (including mini statements), Feedback system (optional), Admin controls for managing users and transactions.

# User Roles

+ Student: Register by providing the necessary details, log in to check your balance, deposit or withdraw funds, View mini statements of transactions, and Update your password.
+ Admin: Login to manage accounts and view transactions, reset passwords for students, if necessary, and View feedback entered by students.
+ Navigation: After logging in, users can navigate through various functionalities such as Balance Checking, Transactions, Feedback submission, and Logout options.
+ Error Handling: Any errors encountered during action will be displayed via message boxes notifying appropriate feedback, Troubleshooting
+ Connection Issues: Ensure that the connection string is pointing to the correct database file, Check if SQL Server is running and accessible.
+ Invalid Credentials: Ensure correct login details. Check that the account is active.
+ Database Error: Ensure that the database schema matches the application requirements.
+ Debugging: Utilize Visual Studio’s logging and debugging tools by checking the console output for any errors when running the solution.
+ Help and Support: Consult online forums or the official Visual Studio documentation for more comprehensive troubleshooting steps.


Contact Information For any issues or queries regarding the project, please contact:[Alonemapitlula@gmail.com]
