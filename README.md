# BelanjaYuk

BelanjaYuk is a web-based e-commerce application developed using a Client-Server architecture. The system provides a complete shopping flow for Buyers, starting from registration and login, browsing products, filtering by category, managing the shopping cart, checking out, selecting a payment method, viewing transaction history, and managing user profiles.

## Features

### Authentication

- Buyer registration
- Login using email or phone number
- Logout
- Session management

### Products & Categories

- View product list
- View product details
- Filter products by category
- View active product categories

### Shopping Cart

- Add products to the cart
- Update product quantity
- Remove products from the cart
- Calculate subtotal and total amount

### Checkout & Payment

- Select a payment method
- Checkout shopping cart
- Create a transaction based on cart items
- Clear the cart after a successful checkout

### Transaction History

- View transaction history
- View payment method
- View products included in each transaction
- View quantity, price, discount, subtotal, and final total

### Profile

- View user profile
- Update username
- Update first name and last name
- Update email
- Update phone number
- Update date of birth
- Update gender

## Technology Stack

### Backend

- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- Microsoft SQL Server

### Frontend

- ASP.NET Core MVC
- .NET 8
- Razor Views
- Bootstrap

### Database

- Microsoft SQL Server
- Database: `belanjayuk`

## System Architecture

BelanjaYuk uses a Client-Server architecture.

```text
Buyer
  ↓
WebClient (ASP.NET Core MVC)
  ↓
REST API
  ↓
Backend (ASP.NET Core Web API)
  ↓
Entity Framework Core
  ↓
Microsoft SQL Server
````

The frontend does not access the database directly. All data operations are handled through the REST API provided by the Backend.

## Project Structure

```text
Belanja_yuk/
├── Backend/
│   └── BelanjaYuk.API/
│       ├── Controllers/
│       ├── Data/
│       ├── Models/
│       ├── Program.cs
│       └── appsettings.json
│
├── WebClient/
│   └── BelanjaYuk.Web/
│       ├── Controllers/
│       ├── Models/
│       ├── Views/
│       ├── wwwroot/
│       ├── Program.cs
│       └── appsettings.json
│
├── Database/
│   ├── belanjayuk_schema.sql
│   └── belanjayuk_data.sql
│
├── .gitignore
└── README.md
```

## Main User Flow

```text
Register
   ↓
Login
   ↓
Products
   ↓
Category Filter
   ↓
Add to Cart
   ↓
Update / Delete Cart
   ↓
Select Payment
   ↓
Checkout
   ↓
Transaction History
   ↓
Profile
   ↓
Edit Profile
```

## API Endpoints

### Authentication

```text
POST /api/Auth/login
POST /api/Auth/register
```

### Products

```text
GET /api/Products
GET /api/Products/{id}
GET /api/Products?category={categoryId}
```

### Categories

```text
GET /api/Categories
GET /api/Categories/active
GET /api/Categories/{id}
```

### Cart

```text
GET    /api/Cart/{userId}
POST   /api/Cart
PUT    /api/Cart/{id}
DELETE /api/Cart/{id}
```

### Payment

```text
GET /api/Payments
GET /api/Payments/{id}
```

### Transaction

```text
POST /api/Transaction/checkout/{userId}
GET  /api/Transaction/user/{userId}
```

### Profile

```text
GET /api/Profile/{userId}
PUT /api/Profile/{userId}
```

## Database

BelanjaYuk uses Microsoft SQL Server with the following database name:

```text
belanjayuk
```

The database files are stored in:

```text
Database/
├── belanjayuk_schema.sql
└── belanjayuk_data.sql
```

### Database Schema

The `belanjayuk_schema.sql` file is used to create the database and the table structures required by the application.

Some of the main tables used by the application are:

```text
MsUser
MsUserPassword
MsProduct
LtCategory
LtGender
LtPayment
TrBuyerCart
TrBuyerTransaction
TrBuyerTransactionDetail
```

The database also contains additional supporting tables for the overall system.

### Database Data

The `belanjayuk_data.sql` file is used to populate the database with initial data, including:

* Product categories
* Gender data
* Payment methods
* User data
* User password data
* Product data
* Other supporting data

### Database Setup Order

To create the database from scratch, execute the SQL files in the following order:

1. Run `Database/belanjayuk_schema.sql`
2. After the database and tables have been created successfully, run `Database/belanjayuk_data.sql`

Make sure the `belanjayuk` database and all required tables have been created before running the data script.

## How to Run

### 1. Prepare the Database

Make sure Microsoft SQL Server and the SQL Server instance used by the project are running.

Open SQL Server Management Studio (SSMS), then execute:

```text
Database/belanjayuk_schema.sql
```

After the database and tables have been created successfully, execute:

```text
Database/belanjayuk_data.sql
```

The two files must be executed in the specified order so that the database structure and initial data are available.

### 2. Run the Backend

Open a terminal and navigate to:

```text
Backend/BelanjaYuk.API
```

Run:

```bash
dotnet restore
dotnet build
dotnet run
```

The Backend runs at:

```text
http://localhost:5296
```

### 3. Run the WebClient

Open another terminal and navigate to:

```text
WebClient/BelanjaYuk.Web
```

Run:

```bash
dotnet restore
dotnet build
dotnet run
```

The WebClient runs at:

```text
http://localhost:5165
```

Make sure the Backend is running before using the WebClient.

### 4. Open the Application

After both the Backend and WebClient are running, open:

```text
http://localhost:5165
```

The main application flow is:

```text
Register
   ↓
Login
   ↓
Products
   ↓
Category Filter
   ↓
Add to Cart
   ↓
Checkout
   ↓
Payment
   ↓
Transaction History
   ↓
Profile
```

## Testing

The main system flow has been tested end-to-end:

```text
Register
→ Login
→ Products
→ Category Filter
→ Add Cart
→ Update Cart
→ Delete Cart
→ Re-add Cart
→ Payment
→ Checkout
→ Transaction History
→ Profile
→ Edit Profile
```

The complete core Buyer flow has been successfully tested.

## Scope

This version focuses on the **Buyer** side of the e-commerce application.

Seller and Admin features are not included in the main user flow of this version. Database tables related to Seller/Admin functionality are retained to maintain compatibility with the existing database structure.

## Development Notes

The project is developed using a modular architecture with separation between:

* Presentation layer
* API layer
* Business logic
* Data access
* Database

The separation between the Backend and WebClient allows each component to be developed and maintained independently.

## Status

**Project Status: Completed**

The core Buyer e-commerce flow has been implemented and tested end-to-end.

Selesai itu README GitHub-mu sudah English juga. 👍
