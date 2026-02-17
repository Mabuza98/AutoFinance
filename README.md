# AutoFinance AI

## Overview
AutoFinance AI is a full-stack car finance project that calculates car loan affordability, risk scoring, and suggests a safer car price for users. The project demonstrates backend financial logic, API development, and frontend integration.

---

## Project Structure

- `AutoFinance.API/` → Backend (ASP.NET Core Web API)  
- `AutoFinanceAPI-Frontend/` → Frontend (React / Angular / Blazor)  

---

## Features

- Calculates monthly loan installments  
- Calculates Debt-to-Income ratio  
- Provides risk level and approval probability  
- Suggests safer car price if DTI is too high  
- Clean layered architecture: Models → Services → Controllers  
- Swagger UI ready for API testing  

---

## Tech Stack

- Backend: ASP.NET Core Web API  
- Frontend: React / Angular / Blazor  
- Language: C#  
- Tools: Visual Studio, Node.js, npm  

---

## How to Run

### Backend
bash
cd AutoFinance.API
dotnet run
Backend runs at http://localhost:5206/swagger

### Frontend
powershell
cd autofinanceapi-frontend
npm start
Frontend runs at http://localhost:3000
