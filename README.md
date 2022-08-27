# Pierre's Sweet and Savory Treats

#### By: Viktoriia Zubarieva

## Description

A web application that can market sweet and savory treats, with user authentication and a many-to-many relationship. Here are the features in this application:

- The application has a user authentication. A user is able to log in and log out. Only logged in users have create, update and delete functionality. All users have reading functionality.
- There is a many-to-many relationship between Treats and Flavors. The treat has many flavors (such as sweet, savory, spicy, or creamy) and the flavor has many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on.
- A user is be able to navigate to a splash page that lists all treats and flavors. User is be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.

![project-screenshot](SweetAndSavory/wwwroot/img/Screenshot.png)

## Technologies Used

- C#
- .Net 5.0
- Git
- ASP.NET Core MVC
- Microsoft.EntityFrameworkCore
- Dotnet EntityFramework Tool
- Microsoft.EntityFrameworkCore.Design
- Microsoft.AspNetCore.Identity.EntityFrameworkCore

## Setup/Installation Requirements

- Clone this project to your desktop with the link provided on the its Github [repository](https://github.com/vzubarieva/Pierre-s-Sweet-and-Savory-Treats.Solution)
- Navigate to the top level of the directory
- Create appsettings.json in Pierre-s-Sweet-and-Savory-Treats.Solution /SweetAndSavory/ directory

- Copy this code into appsettings.json, replacing YOUR_PASSWORD with your MySQL password
  { "ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3306;database=factory;uid=root;pwd=YOUR_PASSWORD;" } }

- open new terminal and run SQL

  $ mysql -uroot -p{your_password}

- open MySQL Workbench

- In terminal, navigate into Pierre-s-Sweet-and-Savory-Treats.Solution /SweetAndSavory/ and enter this command, to install necessary packages

  $ dotnet restore

- enter this command to build the program

  $ dotnet build

- enter this command to build your database

  $ dotnet ef database update

- check MySql Workbench to make sure the correct database has built

- enter this command to view this application in your browser

  $ dotnet run

## Known Bugs

- _No known bugs_

## License

_Message to viktoria.dubinina@gmail.com with any comments or contributions. This software is licensed under the MIT license_

Copyright (c) _August 2022_ _Viktoriia Zubarieva_
cd
