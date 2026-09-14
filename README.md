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
