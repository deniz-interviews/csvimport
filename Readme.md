# Csv Import
Csv Import project for IRECKONU

## Technologies
- Asp.Net Core
- Entity Framework Core
- Sql Express or InMemoryDb
- Memory cache storing intermediate CSV results
- Dependency Injection

## Data Structure
`Product Family`-> `Product` -> `ProductDetails`

- ProductFamily: Nike Shirt
- Product: XL-Red Nike Shirt, L-Red Nike Shirt, M-Red Nike Shirt etc.
- ProductDetails: Details and Descriptions like XL, Red, Nike etc. 

## Assumptions
- We are working with the customer first time. So he/she does not know the data structure.
- Manual corrections on CSV are possible.
- CSV has predefined fields.
- Product / Product Family list is relatively small. We are not building a system like Amazon.
- Categories cannot be renamed by CSV Import.
- SKUs should be unique
- If header of CSV is wrong, we do not accept it.
- Discount Price should be smaller than normal Price
- Prices cannot be 0, Discount Price can be null
- Does the DB Migration automatically.

## Notes / Things to improve
- As I am current working, I had only 3 evenings to finish. Apprx 10 hours.
- There are many TODOs in the Project. Please check them.
- Unfortunately I had no time to do Integration Tests
- Only implemented ProductManager unit tests. Repository unit tests also required
- I focused mostly on backend on my limited time. FE is just a placeholder.

## Some TODOs
- Adding models to `Host` Project. Using Automapper to map them with BL models/entities.
- Adding swagger and proper API documentation
- More fancy FE. Maybe using a HTML template.
- More failure unit tests
- Proper Api/MVC Controller differentiation.
- Logging exceptions to Application Insights with ILogger
- Identity for authentication