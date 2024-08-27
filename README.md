# GamerShop API project uses Entity Framework to worth with database and MongoDB as cache.
### To configure DB we need these shell commands:
### dotnet tool install --global dotnet-ef
### dotnet ef migrations add InitialCreate (Migration Creation - In this case, InitialCreate is the name of the migration)
### dotnet ef database update (Executing the migration on the database)

Project contains 3 Controllers:
>UserController / ProductController / OrderController

UserController
METHOD GET GetAllUsers returns Json of all existing users.
>Return
```json
  {
    "id": 2,
    "firstName": "Rayko",
    "lastName": "Jugger",
    "email": "rayko@mail.com",
    "phoneNumber": "+37068585585"
  },
  {
    "id": 4,
    "firstName": "Roker",
    "lastName": "Roker",
    "email": "Roker@mail.com",
    "phoneNumber": "+1654611235"
  },
```
METHOD GET GetUserById returns Json of selected user by ID.
>Return
```json
  {
    "id": 2,
    "firstName": "Rayko",
    "lastName": "Jugger",
    "email": "rayko@mail.com",
    "phoneNumber": "+37068585585"
  }
```
METHOD POST CreateUser inserts user into DB table. As input it takes "userType" and then checks if its "Buyer" or "Seller".
METHOD PATCH UpdateUser updates Json of selected user.
METHOD DELETE DeleteUser deletes user by inputted ID.

ProductController
METHOD GET GetAllProducts returns Json of all existing products.
>Return
```json
[
  {
    "id": 1,
    "productName": "Witcher 3 The Wild Hunt",
    "price": 15.99,
    "productType": 0,
    "countInStorage": 5
  },
  {
    "id": 2,
    "productName": "Fable The Lost Chapters",
    "price": 9.99,
    "productType": 0,
    "countInStorage": 5
  }
]
```
METHOD GET GetProductById returns Json of selected product by ID.
>Return
```json
{
  "id": 2,
  "productName": "Fable The Lost Chapters",
  "price": 9.99,
  "productType": 0,
  "countInStorage": 5
}
```
METHOD POST CreateProduct inserts product into DB table.
METHOD PATCH UpdateProduct updates Json of selected product.
METHOD DELETE DeleteProduct deletes product by inputted ID.

OrderController
METHOD GET GetAllOrders returns Json of all existing orders.
>Return
```json
[
  {
    "orderId": 1,
    "product": {
      "id": 2,
      "productName": "Fable The Lost Chapters 2",
      "price": 9.99,
      "productType": 0,
      "countInStorage": 5
    },
    "user": {
      "id": 1,
      "firstName": "Flint",
      "lastName": "Flint",
      "email": "flint@mail.com",
      "phoneNumber": "+37068585585"
    },
    "orderDate": "2024-08-26T07:51:35.138",
    "quantity": 2
  },
  {
    "orderId": 3,
    "product": {
      "id": 2,
      "productName": "Worms 3",
      "price": 1.99,
      "productType": 0,
      "countInStorage": 5
    },
    "user": {
      "id": 2,
      "firstName": "Rayko",
      "lastName": "Jugger",
      "email": "rayko@mail.com",
      "phoneNumber": "+37068585585"
    },
    "orderDate": "2024-08-27T07:15:47.369",
    "quantity": 25
  }
]
```
METHOD GET GetOrderById returns Json of selected order by ID.
>Return
```json
{
  "orderId": 2,
  "product": {
    "id": 2,
    "productName": "Fable The Lost Chapters 2",
    "price": 9.99,
    "productType": 0,
    "countInStorage": 5
  },
  "user": {
    "id": 2,
    "firstName": "Rayko",
    "lastName": "Jugger",
    "email": "rayko@mail.com",
    "phoneNumber": "+37068585585"
  },
  "orderDate": "2024-08-27T01:15:47.369Z",
  "quantity": 25
}
```
METHOD POST CreateOrder inserts order into DB table.
METHOD PATCH UpdateOrder updates Json of selected order.
METHOD DELETE DeleteOrder deletes order by inputted ID.
