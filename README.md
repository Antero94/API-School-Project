# API-School-Project
API that we worked on during the final school year for our portfolio.

The purpose and function of the API is to demonstrate the students ability to build simple APIs that are connected to a MySQL Database.

The API uses controllers with a variety of CRUD commands. When a controller is interacted with it sends the information to the service layer of the API
that handles all the code, such as authorization and mapping. Once this has been handled the service layer then sends the code to the repository, which has
direct contact with the MySQL Database.

EntityFrameWork Core was used to establish the database, and Pomelo to bypass the need to write SQL syntax.

The API has middleware that handles error logging via Serilog, as well as basic authentiation for a simple login system.

The API uses a mapper that censors sensitive information that has been retrieved from the database, such as passwords. This is being handled by the 
service layer which maps information to the database as "Entities" and to the user as "DTOs".

FluentValidation is being used to validate input to the database, such as valid email addresses and the length of parameters.
