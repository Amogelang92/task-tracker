
# Full Stack Task Tracker

The system allows users to create, update, delete, search and sort tasks.
It includes backend validation anf basic unit tests on both frontend and backend.

## Tech Stack

### Backend

- .Net 8 ASP.NET Core Web API
- InMemory Provider
- MSTest for unit testing
- Swagger
- Created Dockerfile (Optional)

### Frontend

- Angular: 19.2.18
- Node: 18.20.8
- Package Manager: npm
- Typescript: v5.6.3
- Reactive Forms
- Jasmine + Karma for unit testing

## Running the project locally

### Backend API

- Ctrl + F5 to run the project
- Swagger UI on: https://localhost:7069/swagger/index.html


## Frontend Angular

- Navigate to frontend folder: bash cd lexis-frontend
- bash : npm install
- Run the app: ng serve
- It will be available on: http://localhost:4200/

## Running Tests

### Backend API

- bash: dotnet test
- 
### Frontend app

- bash: ng test

## CI Pipeline

A minimal GitHub Actions CI workflow is included.

It automatically:

- Builds the .NET 8 API
- Runs backend MSTest tests



