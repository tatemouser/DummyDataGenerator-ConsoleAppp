# 🧪 DummyDataGenerator

A lightweight console application for generating realistic, customizable dummy data — ideal for seeding databases, testing APIs, or populating user interfaces.

---

## 🎯 Goals

- Generate mock data with user-defined fields and types
- Provide a simple command-line interface (CLI)
- Export data in common formats (CSV, JSON)
- Enable quick testing without writing manual SQL inserts

---

## 🔄 Flow of Application

### 1. **Select Data Type**
- Choose from predefined templates (`User`, `Order`, etc.) or define a **custom table**

### 2. **Define Table Structure (Custom Mode)**
- Enter fields one at a time:
  - `name string`
  - `email email`
  - `age int`
  - `createdAt date`

- Type `done` when finished

#### Supported Data Types:
| Type     | Description                         |
|----------|-------------------------------------|
| `string` | Random words or names               |
| `int`    | Random integer                      |
| `date`   | Random past date                    |
| `email`  | Fake email address                  |
| `phone`  | Formatted phone number              |
| `uuid`   | Unique identifier (GUID)            |
| `bool`   | Random true/false                   |

---

### 3. **Specify Row Count**
- Prompt:  
`"How many rows do you want to generate?"`  
- Example: `100`

---

### 4. **Preview Sample Output**
- Display first few rows for review:
	- name,email,age,createdAt
	- Alice,a1@mockmail.com,29,2024-12-14
	- Bob,b7@mockmail.com,41,2024-03-22


---

### 5. **Export Options**
- Choose export format:
- `CSV` — Import into SQL Server, Excel, etc.
- `JSON` — For APIs, configs, or Mongo-like input

---

### 6. **Navigation**
- Return to main menu
- Start a new generation cycle
- Exit the program

---



## TODO: Plugin alternative to starting app manually

## 🧪 Example Usage

```plaintext
Select data type (Custom/User/Order): Custom

Enter column name and type (e.g., name string), or 'done' to finish:
> name string
> email email
> age int
> createdAt date
> done

How many rows to generate?
> 100

Here is a preview of your data:
name,email,age,createdAt
Alice,a1@mockmail.com,29,2024-12-14
...

Choose export format (CSV/JSON):
> CSV

✅ Data exported to /output/data_2025-05-27.csv



## TODO: Plugin alternative to starting app manually
