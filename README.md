# 🧪 DummyDataGenerator

A lightweight tool for generating realistic, customizable dummy data — ideal for seeding databases, testing APIs, or populating UIs. Supports both a **console interface** and a **simple Web API** for flexibility.

---

## 🎯 Goals

- Generate mock data with user-defined fields and types
- Provide both a simple command-line interface (CLI) and Web API
- Export data in common formats (CSV, JSON)
- Enable quick testing without writing manual SQL inserts

---

## 🖥️ Console App Flow

### 1. **Define Table Structure**
- Enter fields one at a time:
  - `name`
  - `email`
  - `age`
  - `createdAt`

- Select data type for each:
  - `string`, `int`, `date`, `bool`

---

### 2. **Specify Row Count**
- Prompt:  
  "How many rows would you like to generate?"

---

### 3. **Preview Sample Output**
- Displays first few rows for review:
  name,email  
  User1234,user5678@example.com  
  ...

---

### 4. **Export Options**
- Choose whether to:
  - Save to `/output/` folder
  - Delete the generated file

---

## 🌐 API Support

Run the Web API project to generate dummy data programmatically via `POST /api/generate`.

### 🔗 Example Request (JSON)
POST /api/generate  
Content-Type: application/json

{
  "fields": [
    { "name": "email" },
    { "name": "date" },
    { "name": "name" }
  ],
  "rowCount": 3,
  "format": "json"
}

### ✅ Expected Response

[
  { "email": "user1283@example.com", "date": "2024-03-10", "name": "User9837" },
  ...
]

Also supports `"format": "csv"` to return a downloadable CSV file.

---

## 🧪 Example CLI Session

Enter column name for column #1: name  
Type for 'name': string  
Enter column name for column #2: email  
Type for 'email': string  
How many rows? > 100

✅ Data preview:  
name,email  
User1234,user7890@example.com  
...

✅ Exported to /output/data_2025-05-28.csv

---

## ✅ Features Covered

- Console mode with live prompts
- Output preview
- Save or discard option
- Reusable generation logic
- API endpoint for programmatic use

---

## 🔧 TODO

- Add frontend UI for API consumption
- Add unit tests for generation service
- Add plugin to skip CLI manual launch
