# 🧾 Order Creation & ERP Integration API (.NET 8)

## Overview

This is a lightweight **Order Management API** built using **ASP.NET Core Web API (.NET 8)**.  
It provides functionality to:

- Fetch product data (SKU, Quantity)
- Create orders using product SKUs
- Authenticate with an external ERP system via JWT
- Cancel orders to the ERP system

The architecture follows **SOLID principles** using a clean two-layer structure:
- **Presentation Layer (Controllers)**
- **Service Layer (Business Logic)**

---

## 🛠 Tech Stack

- **Framework**: ASP.NET Core 8 (Web API)
- **Language**: C#
- **Authentication**: JWT (via external ERP)
- **Tools**: Swagger (OpenAPI), HttpClient

---

## 📚 Features

- ✅ Get available products (from Products API)
- ✅ Sign in to external ERP using provided API and get JWT token
- ✅ Create a new order using product SKUs and quantities
- ✅ Send the order to ERP with authentication
- ✅ Cancel the order to ERP with authentication
- ✅ Clean, testable architecture using SOLID

---
