# 🧪 DummyDataGenerator
## End-to-End Data Generator with API and CI/CD Integration

A lightweight tool for generating customizable mock data — ideal for seeding databases, testing APIs, or populating UIs. Includes a **console application**, a **Web API**, and a **comprehensive test suite**.

---

## 🚀 Core Features

| Area         | Features Included                                                                 |
|--------------|-----------------------------------------------------------------------------------|
| 🎛 Console   | Step-by-step prompts for field creation, row count, preview, and file export     |
| 🌐 API       | `POST /api/generate` returns data in JSON or CSV via simple request body         |
| 💾 Output    | Saves files in `/output/` with timestamped naming, previewable via CLI           |
| 🧪 Testing   | Full suite: Unit, Validation, Boundary, API, Integration, and GitHub CI          |
| 🔁 Reusability | Shared services for both API and console for easy maintenance and expansion    |

---

## 🖥️ Console App Flow

1. **Define Fields & Types**
   - Example: `name string`, `email email`, `age int`, `createdAt date`

2. **Specify Row Count**
   - Prompt: `"How many rows would you like to generate?"`

3. **Preview Output**
   - Displays the first 5 rows in the terminal

4. **Export Options**
   - Saves to `/output/`
   - Optional: delete most recent file with confirmation

---

## 🌐 API Endpoint

Start the Web API project and use:

**POST** `/api/generate`  
**Content-Type**: `application/json`

```json
{
  "fields": [
    { "name": "email" },
    { "name": "date" },
    { "name": "name" }
  ],
  "rowCount": 3,
  "format": "json"
}
```

✅ **Response:**  
Returns structured dummy data in JSON (or CSV if specified)

---

## 🧪 Testing Coverage

All testing handled in the `DummyDataGenerator.Tests` project:

| Type           | Coverage Details                                                    |
|----------------|---------------------------------------------------------------------|
| ✅ Unit Tests   | Core generation logic (BuildInMemory, GetValueForType)            |
| 🔍 Validation   | User input correctness, null fields, invalid types                |
| 📏 Boundary     | Row limits, field limits, string/number boundaries                |
| 🌐 API Tests    | End-to-end calls to `/api/generate` for JSON and CSV              |
| 📂 Integration  | File creation and preview checks from the `/output` directory     |
| ⚙️ CI Setup     | GitHub Actions automatically builds and runs tests on every push  |

---

## ✅ Accomplishments

- 🔄 Console and API modes built using shared logic
- 📂 Organized project structure (.sln with 3 subprojects)
- 🧪 Over 35 automated tests grouped by category
- 🛠️ Full GitHub Actions CI workflow running on push
- 🚫 Cleaned up broken submodule and fixed cross-platform issues
- 🔁 Reusable services for extensibility
- 📦 Ready for frontend consumption or deployment

---

## Installation & Usage

1. Clone the repository
2. Run `dotnet build` to build the solution
3. For console app: `dotnet run --project DummyDataGenerator.Console`
4. For API: `dotnet run --project DummyDataGenerator.API`
5. Run tests: `dotnet test`

---

*Let me know if you want this turned into a downloadable file, or want a version that includes project badges (like GitHub Actions passing, .NET version, etc.).*
