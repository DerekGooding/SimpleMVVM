# SimpleInjection

A lightweight dependency injection library for C# that combines simple DI container functionality with powerful source generation for content management.

## Features

### 🚀 Simple Dependency Injection
- **Attribute-based registration** - Mark classes with `[Singleton]`, `[Scoped]`, or `[Transient]`
- **Automatic service discovery** - No manual registration required
- **Constructor injection** - Automatic dependency resolution
- **Scope management** - Built-in scoped service lifetime management

### ⚡ Content Source Generation
- **Automatic enum generation** - Creates enums from your content collections
- **Type-safe access** - Generated helper methods for accessing content by enum or index
- **Performance optimized** - Uses `NamedComparer<T>` for fast dictionary lookups
- **Roslyn analyzers** - Enforces best practices and catches common mistakes

## Quick Start

### 1. Install the Package
```bash
dotnet add package SimpleInjection
```

### 2. Dependency Injection Usage

Mark your classes with lifetime attributes:

```csharp
[Singleton]
public class DatabaseService
{
    public void Connect() => Console.WriteLine("Connected to database");
}

[Scoped]
public class UserService
{
    private readonly DatabaseService _database;
    
    public UserService(DatabaseService database)
    {
        _database = database;
    }
    
    public void GetUser() => _database.Connect();
}
```

Initialize and use the host:

```csharp
var host = Host.Initialize();

// Get singleton services directly
var dbService = host.Get<DatabaseService>();

// Create scopes for scoped services
using var scope = host.CreateScope();
var userService = scope.Get<UserService>();
```

### 3. Content Generation Usage

Define your content classes:

```csharp
// Your content item must implement INamed
public record Material(string Name, string Color, int Durability) : INamed;

// Your content collection must implement IContent<T>
[Singleton]
public partial class Materials : IContent<Material>
{
    public Material[] All { get; } = 
    [
        new("Steel", "Gray", 100),
        new("Wood", "Brown", 50),
        new("Gold", "Yellow", 25)
    ];
}
```

The source generator automatically creates:

```csharp
// Generated enum
public enum MaterialsType
{
    Steel,
    Wood,
    Gold
}

// Generated helper methods
public partial class Materials
{
    public Material Get(MaterialsType type) => All[(int)type];
    public Material this[MaterialsType type] => All[(int)type];
    public Material GetById(int id) => All[id];
    public Material Steel => All[0];
    public Material Wood => All[1];
    public Material Gold => All[2];
}
```

Use the generated code:

```csharp
var materials = host.Get<Materials>();

// Type-safe access using enums
var steel = materials[MaterialsType.Steel];
var wood = materials.Get(MaterialsType.Wood);

// Direct property access
var gold = materials.Gold;

// Index-based access
var firstMaterial = materials.GetById(0);
```

## Advanced Features

### Performance Optimizations

The library includes `NamedComparer<T>` for efficient dictionary operations with `INamed` keys:

```csharp
// Use ToNamedDictionary extension method
var materialDict = materials.All.ToNamedDictionary(m => m.Durability);

// Or explicitly specify the comparer
var dict = new Dictionary<Material, int>(new NamedComparer<Material>());
```

### SubContent Collections

For hierarchical content organization:

```csharp
public class WeaponStats : ISubContent<Material, int>
{
    public Dictionary<Material, int> ByKey { get; }
    public int this[Material material] => ByKey[material];
}
```

### Roslyn Analyzers

The package includes analyzers that help you:
- **NC001**: Ensures `Dictionary<TKey, TValue>` uses `NamedComparer<T>` for `INamed` keys
- **TND001**: Suggests using `ToNamedDictionary()` instead of `ToDictionary()` for `INamed` keys

## Requirements

- .NET 8.0 or .NET 9.0
- C# with nullable reference types enabled (recommended)

## How It Works

1. **Service Discovery**: The host scans all loaded assemblies for classes marked with lifetime attributes
2. **Dependency Resolution**: Constructor parameters are automatically resolved from registered services
3. **Source Generation**: The generator scans for classes implementing `IContent<T>` and generates enums and helper methods
4. **Code Analysis**: Roslyn analyzers ensure best practices for dictionary usage with named keys

## Best Practices

- Use `[Singleton]` for stateless services and shared resources
- Use `[Scoped]` for services that should be unique per operation/request
- Use `[Transient]` for lightweight, stateless services that need fresh instances
- Always implement `INamed` for content objects to enable source generation
- Use the generated enums for type-safe content access
- Leverage `ToNamedDictionary()` for performance when working with `INamed` collections

## License

MIT License - see the license file for details.
