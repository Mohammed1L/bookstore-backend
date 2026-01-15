# Bookstore Project Backend (.NET)

The **Bookstore Project Backend** is a RESTful API built with **C# and .NET** for an online bookstore system.  
It is developed as a training project to apply **clean architecture, SOLID principles, and best practices in backend development**.

The backend is responsible for:
- Authentication and authorization
- Business logic and validations
- Database access and transactions
- Exposing RESTful APIs for the frontend (Angular)

---

## Design Principles

The backend is designed with:

### SOLID Principles
- **Single Responsibility**  
  Controllers, Services, and Repositories have clear and isolated responsibilities.
- **Open/Closed**  
  Services are extensible via interfaces without modifying existing logic.
- **Liskov Substitution**  
  Interfaces are respected across implementations.
- **Interface Segregation**  
  Small, role-specific interfaces.
- **Dependency Inversion**  
  High-level modules depend on abstractions, not concrete implementations.

### Architecture
- Controllers (API Layer)
- Services (Business Logic)
- Repositories (Data Access)
- DTOs and Mapping
- Domain Models

Dependency Injection is implemented using the built-in .NET container.

---

## 🚀 Tech Stack (Backend)

- **Language:** C#
- **Framework:** ASP.NET Core (.NET)
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT
- **Architecture:** Layered, interface-based design

---

## 👥 User Roles

### Admin
- Manage users and sellers
- Control permissions

### Seller
- Add and manage books

### Regular User
- Register and authenticate
- Browse and buy books

---
## 🗄️ ER Diagram

<img width="845" height="585" alt="image" src="https://github.com/user-attachments/assets/3320df17-7712-4b18-8f0f-f3ba44cfef07" />


---

## 🔄 Flow Chart

<img width="860" height="470" alt="image" src="https://github.com/user-attachments/assets/5b93eaa6-be24-4edd-87f0-b167ce5b449a" />


---

## 📜 Sequence Diagrams

- **Addin a book to cart**  
  <img width="834" height="481" alt="image" src="https://github.com/user-attachments/assets/d4b8d548-d063-47c1-85d1-45705f92ba22" />
 

- **Book Purchase**  
  <img width="817" height="385" alt="image" src="https://github.com/user-attachments/assets/94a9ec55-f10c-406a-ada7-aff81736fe2c" />


---

## 🌐 API Endpoints
### **Authentication (AuthController)**
| Method | Endpoint | Request Body | Response |
|--------|----------|--------------|----------|
| POST | `/api/auth/register` | `{ name, email, password, role }` | **201 Created** `{ success: true, message: "User registered successfully.", data: { user, token }, errors: null }` |
| POST | `/api/auth/login` | `{ email, password }` | **200 OK** `{ success: true, message: "Login successful.", data: { user, token }, errors: null }` |

---

### **Books (BookController)**
| Method | Endpoint | Request Body | Response |
|--------|----------|--------------|----------|
| GET | `/api/books` | – | **200 OK** `{ success: true, message: "Books retrieved successfully.", data: [ … ], errors: null }` |
| GET | `/api/books/{id}` | – | **200 OK** `{ success: true, message: "Book details fetched successfully.", data: { book }, errors: null }` |
| POST | `/api/books` | `{ title, authorId, price, categoryIds[], inventory }` | **201 Created** `{ success: true, message: "Book created successfully.", data: { bookId }, errors: null }` |
| PUT | `/api/books/{id}` | `{ title?, price?, inventory? }` | **200 OK** `{ success: true, message: "Book updated successfully.", data: null, errors: null }` |
| DELETE | `/api/books/{id}` | – | **204 No Content** `{ success: true, message: "Book deleted successfully.", data: null, errors: null }` |

---

### **Orders (OrderController)**
| Method | Endpoint | Request Body | Response |
|--------|----------|--------------|----------|
| GET | `/api/order` | – | **200 OK** `{ success: true, message: "Orders retrieved successfully.", data: [ … ], errors: null }` |
| GET | `/api/order/{id}` | – | **200 OK** `{ success: true, message: "Order details fetched successfully.", data: { order, items }, errors: null }` |
| POST | `/api/order` | `{ items: [{ bookId, quantity }] }` | **201 Created** `{ success: true, message: "Order placed successfully.", data: { orderId }, errors: null }` |
| PUT | `/api/order/{id}` | `{ items: [{ bookId, quantity }] }` | **200 OK** `{ success: true, message: "Order updated successfully.", data: null, errors: null }` |
| DELETE | `/api/order/{id}` | – | **204 No Content** `{ success: true, message: "Order deleted successfully.", data: null, errors: null }` |

---

### **Users (UserController)**
| Method | Endpoint | Request Body | Response |
|--------|----------|--------------|----------|
| GET | `/api/user` | – | **200 OK** `{ success: true, message: "Users retrieved successfully.", data: [ … ], errors: null }` |
| GET | `/api/user/{id}` | – | **200 OK** `{ success: true, message: "User details fetched successfully.", data: { user }, errors: null }` |
| POST | `/api/user` | `{ name, email, password, role }` | **201 Created** `{ success: true, message: "User created successfully.", data: { userId }, errors: null }` |
| PUT | `/api/user/{id}` | `{ roleName?, name?, email?, password? }` | **200 OK** `{ success: true, message: "User updated successfully.", data: null, errors: null }` |
| DELETE | `/api/user/{id}` | – | **204 No Content** `{ success: true, message: "User deleted successfully.", data: null, errors: null }` |

---

## 🌐 Wireframes

<img width="797" height="366" alt="image" src="https://github.com/user-attachments/assets/f3c49c2f-c937-40d1-be13-58f5f0d8fc81" />
Related Repositories
	•	Frontend (Angular): https://github.com/Mohammed1L/bookstore-frontend
