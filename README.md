# learn.dotnet.minimalapi

* [freecodecamp course](https://www.freecodecamp.org/news/build-minimal-apis-in-net-7/)

## Run & Test

```bash
cd MagicVilla_CouponAPI
dotnet run
```

* [Swagger](http://127.0.0.1:5123/swagger/index.html)

## Nuget Config

* created nuget.config file

```bash
dotnet nuget list source
```

## Notes

* should we set nullable to be disabled in the csproj?

## Dependency Injection


Dependency Injection (DI) is a design pattern used in software development to achieve Inversion of Control (IoC) between classes and their dependencies. It's a technique for achieving loose coupling between objects and their collaborators, or dependencies. Here's a breakdown of what it involves and its benefits:

### Basic Concept

- Dependencies: These are objects or services that a class needs to perform its function. For example, if you have a Car class, it might depend on a Engine and Wheels objects.
- Injecting Dependencies: Instead of a class creating instances of its dependencies (using the new keyword, for instance), these instances are provided to the class (typically to a constructor, but also to properties or methods). This is the core of dependency injection.
- 
### Types of Dependency Injection

1. Constructor Injection: Dependencies are provided through a class constructor.
1. Setter Injection: Dependencies are provided through setter methods.
1. Interface Injection: The dependency provides an injector method that will inject the dependency into any client passed to it. Clients must implement an interface that exposes a setter method that accepts the dependency.


### Advantages

- Loose Coupling: Classes are less tightly bound to their dependencies, making the system more modular and easier to adapt or replace components.
- Easier Unit Testing: By injecting dependencies, it's easier to replace real dependencies with mock objects during testing.
- Improved Code Maintainability: Changes to dependencies or their creation do not affect the classes using them.
- Increased Flexibility and Scalability: It's easier to add new functionalities or services since existing code doesn't need to be modified.

### Example

```csharp

No DI

```csharp
class Car {
    private Engine engine;

    Car() {
        this.engine = new Engine(); // Car class directly depends on the Engine class
    }
}

```

with DIs

```csharp
class Car {
    private Engine engine;

    Car(Engine engine) {
        this.engine = engine; // Engine is injected into Car, Car no longer directly depends on Engine's instantiation
    }
}

class Main {
    public static void main(String[] args) {
        Engine engine = new Engine();
        Car car = new Car(engine); // Injecting the dependency
    }
}

```

### Use in Frameworks

Many modern software frameworks and libraries, especially in web development (like Spring for Java, ASP.NET Core for .NET, Angular for JavaScript), are built with dependency injection as a fundamental feature, providing built-in tools for implementing DI easily.

In summary, Dependency Injection is about removing the hard-coded dependencies and making it possible to change them, either at runtime or compile time, leading to more modular, testable, and maintainable code.

## DTO (Data Transfer Object)

DTO stands for Data Transfer Object. It's a design pattern used to transfer data between software application subsystems or layers. DTOs are often used in the context of networking communication, serialization, and the transferring of data between different parts of a software application, such as from the service layer to the presentation layer.

### Key Characteristics of DTOs:

1. Simple Objects: DTOs are simple objects that should not contain any business logic. They typically have public setters and getters to access their data.
1. Data Aggregation: They are used to encapsulate and aggregate data for transfer. Rather than sending individual data items, a DTO can be used to package multiple data elements into a single object.
1. Reduced Network Calls: In distributed systems or client-server architectures, DTOs can reduce the number of network calls by aggregating data that will be transferred together.
1. Layer Separation: DTOs help maintain a clear separation of concerns between different layers of an application, like the presentation layer, business logic, and data access layers.
1. Serialization: DTOs are often designed to be easily serializable for transportation across networks. This is particularly important in API development, such as REST or SOAP services.

### Example Usage:

Consider a web application with a client-server architecture where the client needs to display user information. The server has a User entity with properties like id, username, password, email, dateOfBirth, etc. However, for security reasons, you might not want to send the password or dateOfBirth over the network to the client.

Here, a DTO can be used:

- A UserDTO class is created with only the necessary fields (e.g., id, username, email).
- The server then maps the User entity to UserDTO, excluding sensitive details.
- This UserDTO is then sent over the network to the client.

### Benefits:

- Security: Sensitive data can be excluded from DTOs.
- Reduced Bandwidth Usage: Only necessary data is transferred.
- Decoupling: DTOs help in decoupling different layers of an application by providing a layer-specific view of data.

### Best Practices:

- DTOs should be serializable to facilitate easy transmission.
- Keep DTOs simple: They should not contain complex business logic or behavior.
- Use DTOs to define APIs: They can provide a stable interface while the underlying application might change.
- Mapping between Domain Models and DTOs: Use tools/libraries for mapping (like AutoMapper in .NET, ModelMapper in Java) to simplify the conversion process.
  
## When to Use DTOs:

- Distributed Systems: In scenarios where data needs to be transferred across different parts of a distributed system.
- API Development: When exposing data from a server, DTOs can be used to shape the data that's sent over the network.
- Performance Optimization: When dealing with large datasets or complex domain models, DTOs can help reduce the amount of data that needs to be transferred or processed.
- Security and Data Exposure: To control the exposure of certain data elements, especially in public-facing APIs.

### When Not to Use DTOs:

- Simple Applications: In small or simple applications where adding DTOs might introduce unnecessary complexity.
- Direct Database Operations: In scenarios where data is not being transferred between layers or over a network, and is directly used for database operations.


In summary, DTOs are a useful pattern for efficiently and securely transferring data between different layers or parts of an application, especially in scenarios involving network communication or when a clear separation of concerns is required. They should be simple, focused on data transfer, and devoid of business logic.

## Setup
  
```bash
dotnet new web -o MagicVilla_CouponAPI
cd MagicVilla_CouponAPI
code -r ../MagicVilla_CouponAPI
````

## Required packages

```bash
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.OpenApi
``````

