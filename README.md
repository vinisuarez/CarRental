### Car Rental Management 1.0

This application is set of API meant for managing a car rental company

for running the project first clone it to your machine open the "CarRental.csproj" in visual studio and press run :)

api documentation can be found on https://localhost:5001/swagger/index.html

Register and Login are the only non admin api that don't require jwt token.
The Login request will return the token and that should be included in the `Authorization` header as "Bearer $Token"
If you using the swagger to test the application in the top right corner there is a button to add the header that you get from the login.

The admin APIs are for now hardcoded to use the "Bearer duH5g4FA+1FpAaEzBZoXew==", of course in real application those should not be in the code.
But all APIs can be called with this `Authorization` header for test purpose.
I have added adminAddInitalCars.sh script to add 4 cars with curl to make it easy to test.


### Comments about the task:

Was nice to dive in a new language and try out c#, probably there can be some newbie mistakes - sorry in advance :)

First i was thinking to create Sub class of car for each type, but the only difference between those is the bonus amount. I thought the Enum approach was simpler and easier to maintain (if we add more car types we update where the Enum is used and don't need to worry about saving multiple sub classes to DB).

Also for the rent first i thought to group all/multiple car rentals in 1 Id, but since the cars can be return in different time imo is cleaner and simpler that each car rental get its own rent db row. The total price for all the rental are combined and charged together since we don't want to have multiple chargers if we are paying for more than 1 rent at the same time.

If 1 of the car sent on the rent request is already rented or in maintenance I've decided that the whole request should fail instead of partial success. In a real application the front should not let you rent already rented or in maintenance cars, so we consider the whole request bad.

I've added some fake service for payment gateway that of course should be implemented in the for reals in the final application.

Some unit tests can be found in the CarRentalTest project.


Typical usage of the API:

	1. Register at `/api/carrental/v1/customer/register`
	2. Login at `/api/carrental/v1/customer/login`
	3. Copy the token from login response to Authorization header "Bearer $Token"
	4. run the script on the root folder (./adminAddInitalCars.sh) to add some cars or use the Admin apis
	5. List The cars at `/api/carrental/v1/cars/list`  so you can see the ids of the cars.
	6. now you can calculate the price for rentals before you rent at `/api/carrental/v1/cars/calculate`
	7. use the rent API for actually rent the car `/api/carrental/v1/cars/rent`
	8. maybe you can check now the rents history and transactions in `/api/carrental/v1/customer/rentHistory`
	9. return the car at `/api/carrental/v1/cars/return`

