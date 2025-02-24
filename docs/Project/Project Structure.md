[`🏠`](../README.md) > [`Project`](./README.md) > `Project Structure`

# 📂 Project Structure
This document provides an overview of the project structure and the different layers within the NArchitecture framework. Understanding the structure will help you navigate the codebase and contribute effectively.

NArchitecture is inspired by Clean Architecture, which emphasizes separation of concerns and promotes a scalable and maintainable codebase.

## 🏗️ Layers

### 1. **Domain Layer**
The Domain Layer contains the core business logic and domain entities. It is independent of any external dependencies and focuses on the business rules and domain-specific logic.

- **Entities**: Represent the core business objects.
- **Value Objects**: Immutable objects that represent a concept in the domain.
- **Aggregates**: Group related entities and value objects.

### 2. **Application Layer**
The Application Layer coordinates the application logic and orchestrates the use cases. It depends on the Domain Layer and provides services to the Presentation Layer.

- **Use Cases**: Application-specific command and queries with business logic.
- **DTOs**: Data Transfer Objects for communication between layers.
- **Services**: Application services that handle use cases.
- **Repositories**: Interfaces for data access.

### 3. **Persistence Layer**
The Persistence Layer provides implementations for the repository interfaces defined in the Application Layer. It includes data access and database configurations.

- **Repositories**: Implementations of the repository interfaces.
- **Data Access**: Database context and configurations.

### 4. **Infrastructure Layer**
The Infrastructure Layer provides implementations for cross-cutting concerns and external services. It includes logging, caching, and other infrastructure concerns.

- **Logging**: Logging implementations.
- **Caching**: Distributed caching configurations.
- **External Services**: Integrations with external systems.

### 5. **Presentation Layer**
The Presentation Layer handles the user interface and API endpoints. It depends on the Application Layer to execute use cases and present the results. This layer can include various types of user interfaces such as Web API, Web App, CLI, or MAUI etc.

- **Controllers**: API endpoints for handling HTTP requests.
- **UI**: User interface components (if applicable).
- **ViewModels**: Data models for the views.
- **Views**: User interface components (if applicable).
