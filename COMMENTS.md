# Design Principles

## SOLID Principles

## Core Layer
### User Entity
- A base `User` class that contains the common properties of all users.
- `User` class has a `virtual` property `IsAdmin` that can be overridden by derived classes.
- `Admin` class inherits from `User` class and overrides the `IsAdmin` property.
- `User` class implments the `IUserAction` interface.
- `Admin` class implments the `IAdminAction` interface.

### Media Entity
- An abstract `Media` class that contains the common properties of all media files. this class should not be directly instantiated.
- `Video` and `Audio` classes inherit from the `Media` class. Each class implements the `MediaType` property.
- `Video` and `Audio` classes implement the `IVideoable` and `IAudioble` interface respectly.

## Service Layer

### AdminService
- It manages the user and media files in the application, including add, remove, update removeAll operations.
- Repository should be injected into the service.


### UserService
- It manages the user's playtracks, including add, remove, play, pause, stop operations.

## Infrastructure Layer
- It contains the implementation of the repositories and the database.
- The repositories should implement the interfaces defined in the core layer.
- It contains the entry point to the application - `Program.cs`.


## SOLID Principles
- Single Responsibility Principle: each class is responsible for a single task.For instance, the `UserService` class is responsible for managing the user's playtracks.
- Open/Closed Principle: the classes are open for extension but closed for modification. For instance, the `User` class is open for extension by derived classes but closed for modification.
- Liskov Substitution Principle: derived classes can be substituted for their base classes. For instance, the `Audio` class can be substituted for the `Media` class.
- Interface Segregation Principle: the interfaces are specific to the classes that use them. For instance, the `IUserAction` interface is implemented by the `User` class.
- Dependency Inversion Principle: the high-level modules depend on abstractions, not on concrete implementations. For instance, the `AdminService` class depends on the `IUserRepository` and `IMediaRepository` interfaces, not on the `UserRepository` class.

## Factory Pattern
- Create a `MediaFactory` class that creates a media object based on the media type.
- Create a `UserFactory` class that creates a user object based on the user type.
- Create a `PlayListFactory` class that creates a playtrack object.






## Tuesday Task

- [ ] Create a solution
- [ ] Create and add 3 projects to the solution (2 classlibs, 1 console)
- [ ] Add references(Infrastucture should reference Service, Service should reference Core, Core does not reference anything).

### Tools

CLI list:
- create solution folder: `dotnet new sln -o MySolution`
- create project inside solution (for example, classlib templete ): 
`dotnet new classlib -o MySolution.Project1`
- Add Projects to the Solution: `dotnet sln add MySolution.Project1`
- add reference (dependency) for a project (if you are in solution directory): `dotnet add  MySolution.Project1  reference MySolution.Project2` (now Project1 can use classes from Project2). 
- Or add multiple reference to one project: 
`dotnet add reference lib1/lib1.csproj lib2/lib2.csproj`

- List reference
  `dotnet list [<PROJECT>] reference`

- Remove references

```
dotnet remove [<PROJECT>] reference [-f|--framework <FRAMEWORK>]
     <PROJECT_REFERENCES>
```

## Interface vs. Abstract Class
If the classes that use the interface or abstract class share common implementation, an abstract class is a better choice. If the classes will implment the interface seperately, then an interface is more appropriate.

## Service layer
It deals with data transformation and validation, it shouldn't deal with source.
the source should come from the repositorary interface in the core. 
Dependency injection is the preferred way to do it.


