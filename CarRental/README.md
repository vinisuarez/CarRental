### Car Rental Management 1.0

This application is set of API meant for managing a car rental company

for running the project first clone it to your machine open the "CarRental.csproj" in visual studio and press run :)

api documentation can be found on https://localhost:5001/swagger/index.html


Register and Login are the only non admin api that don't require jwt token.
The Login request will return the token and that should be included in the `Authorization` header as "Bearer $Token"
If you using the swagger to test the application in the top right corner there is a button to add the header that you get from the login.

Comments about the task:

Was nice to dive in a new languange and try out c#, probably there can be some newbie mistakes - sorry in advance :)

I tried and fail to add unit tests to project, 