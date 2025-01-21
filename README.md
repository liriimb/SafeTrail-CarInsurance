# 🚗 Car Insurance Management System

This is a **Car Insurance Management System** built using **ASP.NET MVC**, **Entity Framework Core**, and **Identity** for user authentication and authorization. The project leverages the **Repository Design Pattern** to ensure clean, maintainable, and testable code.

---

## 🔑 Key Features:
- **🔐 User Registration & Authentication**: Users can register, log in, and manage their profiles securely using Identity.
- **🚘 Car Management**: Register, view, edit, or delete cars easily.
- **📑 Claim Management**: File insurance claims, associate them with registered cars, and provide accident details.
- **🚗 MultiCar Support**: Multi-car policy users can register multiple cars, while single-car policy users are limited to one.
- **🛡️ Role-Based Authorization**: Insurance agents can review claims and update claim statuses, while regular users manage their own claims.
- **📁 File Uploads**: Upload supporting documents for claims, with file size restrictions and metadata stored securely.
- **🔄 Claims Lifecycle**: Claims progress through various statuses like "Sent," "InReview," "Accepted," and "Denied," managed by agents.

---

## 💻 Technologies Used:
- **ASP.NET MVC** for the presentation layer.
- **Entity Framework Core** for database access and management.
- **ASP.NET Identity** for authentication and role-based authorization.
- **Repository Design Pattern** to separate data access concerns.

---

## 🚀 Future Enhancements:
- 📢 **Claim Review Notifications**: Improve the claim review process with real-time notifications.
- 📜 **Detailed Claim History**: Provide users and agents with detailed claim tracking history.
- 💳 **Payment Gateway**: Implement a system for premium payments and claim settlements.
