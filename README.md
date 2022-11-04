# ActivityTrackerPC

## Description
>A program that tracks the use of your linux pc through detecting 
>window changes.
>Tracks the following:
> + Registered user
> + Start/end time of a session
> + Used Applications with Durations
>The tracked data is written to a weekly JSON File
>(year_calendarweek.json) and saved in a MariaDB (or MySQL) 
>database. The program should run as a background process on your PC.

## Dependencies
> The program is written in C# on .Net 6.0.
> The required packages are:
> + Microsoft.EntityFrameworkCore.Design/6.0.2
> + Microsoft.EntityFrameworkCore.Tools/6.0.2
> + Newtonsoft.Json/13.0.2-beta2
> + Pomelo.EntityFrameworkCore.MySql/6.0.2
> + Microsoft.EntityFrameworkCore.Tools.DotNet/6.02

## Before use
>If you want to use the program you have to do the following things:
> + Create the required MariaDB database through a migration
>   + The database approach is code first, this means you could create a migration with Microsoft.EntityFrameworkCore.Tools.DotNet. With this migration you could create the database.
> + Adjust the connection string in ActivityTracerContext.cs (Writer.DB, line: 9)
> + Adjust the path for the json file in FileWriter.cs (Writer, line: 15)


## Entity relationship diagram
![ActivityTrackerEntityRelationship](https://user-images.githubusercontent.com/115177899/199925389-3610b869-3994-43d5-b2d1-581d4c4df0ce.jpeg =580x384)
