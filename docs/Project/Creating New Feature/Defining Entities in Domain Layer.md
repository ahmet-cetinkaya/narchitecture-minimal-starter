[`🏠`](../README.md) > [`Project`](../README.md) > [`Creating New Feature`](../README.md) > `Defining Entities in Domain Layer`

# 📦 Defining Entities in Domain Layer

Entities represent the core business objects in your domain layer. This document will guide you through defining entities in NArchitecture.

## 📌 Steps to Define an Entity

1. Create an appropriate folder structure:
   ```
   Domain/
   ├── Features/
   │   └── [FeatureName]/
   │       └── Entities/
   │           └── YourEntity.cs
   ```

2. Create your entity class inheriting from the appropriate base class
```csharp
using NArchitecture.Core.Persistence.Abstractions.Repositories;

namespace NArchitecture.Starter.Domain.Features.Products.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public string Description { get; private set; }
    public string SKU { get; private set; }

    public Product(
        string name,
        decimal price,
        int stockQuantity,
        string description,
        string sku
    )
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description;
        SKU = sku;
    }
}
```
### 📚 Base Entity Properties

All entities automatically include these properties from `BaseEntity<TId>`:

- `Id`: Unique identifier of the entity
- `CreatedAt`: Creation timestamp
- `UpdatedAt`: Last update timestamp
- `DeletedAt`: Soft delete timestamp
- `RowVersion`: Version information for concurrency control
