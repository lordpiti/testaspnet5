# testaspnet5
Test web app for ASP.NET 5, using EF7, MVC 6 and angularJS

This is just a toy web app to test some of the new features of ASP.NET 5. It includes the basic CRUD functionality for a collection of items, some graphs using the google charts API and a RSS feed reader.

The technologies used are:

-Front end: angularJS (additional modules included), SASS, google charts API, bootstrap 3.
-Back end: ASP.NET 5, MVC 6, AutoMapper, EF7, Web API.
-Task runner and package managers: Gulp, Bower, Nuget, Node.js.

The app displays a list of items on which you can perform CRUD operations. It uses a RESTful API that can be changed easily. The project includes one written in Web API with two different implementations for the data access layer, one using EF7 with a SQL Server relational database and another one using MongoDB.

There is another implementation of the REST API written in Node.js which is also available on my github account and can be used instead without need of changing any code 
