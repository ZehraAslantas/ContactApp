# 📇 Contact Management App (ASP.NET Core MVC)

A responsive, layered Contact Management web application built with **ASP.NET Core MVC**. This project demonstrates full **CRUD** (Create, Read, Update, Delete) operations, in-memory data seeding, search/filtering mechanisms, and foundational web security practices.

---

## 🚀 Key Features

* **Full CRUD Lifecycle:** Create, view, edit, and delete contacts seamlessly.
* **Search & Filtering:** Dynamic contact filtering via Query String (`searchTerm`) without altering underlying storage.
* **Post-Redirect-Get (PRG) Pattern:** Prevents duplicate form submissions on page refresh across form actions.
* **CSRF Protection:** Integrated `[ValidateAntiForgeryToken]` tokens on all state-altering POST requests.
* **Layered Architecture:** Clear separation of concerns between Controllers, Services/Repositories, Models, and Views.
* **Responsive UI:** Clean and accessible layout built using Bootstrap 5 and Razor Tag Helpers.

---

## 🛠️ Tech Stack & Architecture

* **Framework:** ASP.NET Core MVC (.NET 8)
* **Language:** C#
* **Frontend:** Razor Views (.cshtml), HTML5, CSS3, Bootstrap 5
* **Data Storage:** In-Memory Seed Data via Repository/Service pattern
* **Patterns & Principles:** MVC, Dependency Injection, Repository Pattern, PRG Pattern

---

## 📂 Project Structure

```text
├── Controllers/
│   └── ContactsController.cs     # Request handling, routing & navigation flow
├── Models/
│   └── Contact.cs                # Contact data entity/model
├── Services/ (or Repositories/)
│   ├── IContactService.cs        # Service interface abstraction
│   └── ContactService.cs         # In-Memory data management & business logic
├── Views/
│   ├── Contacts/
│   │   ├── Index.cshtml          # List view with search bar & action buttons
│   │   ├── Create.cshtml         # Contact creation form
│   │   ├── Edit.cshtml           # Contact update form
│   │   └── Delete.cshtml         # Deletion confirmation view
│   └── Shared/
│       └── _Layout.cshtml        # Main application layout & navbar
└── Program.cs                    # Application setup & DI service registrations
