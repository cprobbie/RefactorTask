
## The Problem

This is a API service which is required to manage products for a users.

The current solution has the following major issues:

- Vulnerable to SQL injection
- Business logic in Controller, no separation of concerns
- No input validation
- No tests

## The Task

The API needs to be refactored to meet production standard as well as easy for other developers to understand and develop new features. To achieve this goal, it needs to have a good and suitable software architecture, dependency injections for good unit test coverage, exception handling, logging and API documenting capability.

## My Approach

In terms of software architecture, I used a Domain-centric 3-Layer architecture. The three layers are API layer which dictates the input and output, Domain layer which contains all the business logic and Infrastructure layer which handles the interaction with database. In my option, this architecture is a good fit for this project as it needs to achieve CRUD functionality and without having complex business logic. This architecture follows Inversion of Control (IoC) principle where API and Infrastructure depend on Domain but Domain does not depend on either of them. 

As business logic is centralised in the Domain layer, even though the unit tests only care about the class being tested, with the use of Test-Driven-Development approach writing the processors, business logic is well assured. All the exceptions thrown are caught by the Exception filter which will then send the corresponding HTTP code and message in the API response.

To solve the SQL injection vulnerability, I removed all the raw SQL and introduced Entity Framework Core for database access.

I have implemented the following frameworks and Nuget packages to refactor the API to meet production standard:

- Entity Framework Core
- Upgraded to ASP.NET Core 3.1
- Swagger for API documentation and testing
- Serilog for logging into console
- nUnit and FluentAssertion for unit testing

## How To Run

Once you have cloned/unzipped the code, open `RefactorThis.sln` using Visual Studio.

The recommended way to run it is to use Visual Studio. Everything is ready, just need to click Start (IIS Express). You may prefer to run the tests first, they should all be passed.

## How To Test

Once the project has been started, it will land on the Swagger index by default. Alternatively, you can go to this url: [`https://localhost:44335/swagger/index.html`](https://localhost:44335/swagger/index.html)

You should see RefactorThis API v1 endpoints and you are ready to play. After clicking on the endpoint, it will show a [Try it out] button. Click on it and you can enter your inputs and click on the Execute button. You will see the HTTP status code and response message in the Responses section.

You may want to start with GET Products to find out what products currently in the database.

I have included some example payloads for POST and PUT endpoints below.

**POST** `/api/v1/Products` 

**PUT** `/api/v1/Product/{id}`

```json
{
  "Name": "iPad Pro",
  "Description": "a nice and big iPad",
  "Price": 999.98,
  "DeliveryPrice": 7.99
}
```

**POST** `/api/v1/products/{id}/options/{optionId}` 

**PUT** `/api/v1/products/{id}/options/{optionId}`

```json
{
  "Name": "128G",
  "Description": "128G storage"
}
```

## How to check logs

It's important that you can keep track of the processes. I have implemented logging in the controllers.

In Visual Studio, open Output window, go to `Show output from:` and select `RefactorThis.Api - ASP.NET Core Web Server`.  For each successful request, you will see an information log message confirming it.

---

# refactor-this
The attached project is a poorly written products API in C#.

Please evaluate and refactor areas where you think can be improved. 

Consider all aspects of good software engineering and show us how you'll make it #beautiful and make it a production ready code.

## Getting started for applicants

There should be these endpoints:

1. `GET /products` - gets all products.
2. `GET /products?name={name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.

All models are specified in the `/Models` folder, but should conform to:

**Product:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description",
  "Price": 123.45,
  "DeliveryPrice": 12.34
}
```

**Products:**
```
{
  "Items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description"
}
```

**Product Options:**
```
{
  "Items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```
