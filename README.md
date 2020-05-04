# bdmI project backend - ASP.NET Core with C#

## The project

This repository contains the WebAPI for the website called bdmI. It is a four sprint long project which aims to create a RESTful API that serves the bdmI web application.

### Used technologies

⚬ C# targeting .NET Core 3.1
⚬ ASP.NET Core
⚬ Entity Framework with PostgreSQL relational database management system
⚬ Identity Framework
⚬ Repository pattern, UnitOfWork

## How to run

To be able to run the WebAPI you have to follow these simple steps:

Create an empty database
Populate your newly created database with the `create-tables-or-reset-database.sql` file.
Setup your environment variables with the following key-value pairs:
⚬ `COONECTION_STRING : Host=DBHost; Database=DBName; Username=YourUsername; Password=YourPassword;`
⚬ `DOMAIN_STRING : https://bdmi.netlify.app/`
⚬ `SECRET_KEY : your secret key`
⚬ `EMAIL_PASSWORD : your emailservice password`
⚬ `REDIRECT : http://localhost:3000`
Run the application
To set up the EmailService part of the project, please create an e-mail account to send the confirmation to the user at registration.

## Frontend

The bdmI frontend that acts as the presentation layer for the project is available at the following link: [bdmI-frontend](https://github.com/AnnaCzinege/bdmI-frontend)
**Heroku tends to fall asleep when the application is not running, therefore it is advised to wait 20-30 seconds after launching the website so the backend has time to load**

## Contributors

The contributors of this project are all students of Codecool Ltd.

⚬ [Anna Czinege](https://github.com/AnnaCzinege)</br>
⚬ [Eszter Mázi](https://github.com/esztermazi)</br>
⚬ [Norbert Benkó](https://github.com/Rasgacode)</br>
