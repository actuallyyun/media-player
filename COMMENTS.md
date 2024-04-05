# Design Principles

The project follows Clean Architecture principles, which separates the application into three layers: Core, Service, and Infrastructure.

## Core Layer
This layer contains the domain entities, interfaces, and enums.

### Abstractions
- `INofity` interface defines the shape of global notification.
- `IMediaPlayerMonitor` interface defines the shape of the media player status observer.
- `IAudioble` and `IVideoable` interfaces define the shape of the audio and video media files.

### RepositoryAbstractions
These abstractions define the shape of the repositories that manage the user, media and playlists in the application.

They are implemented in the Infrastructure layer.

### Entities
- A base `User` class that contains the common properties of all users.
- `User` class has a `virtual` property `UserType` that can be overridden by derived classes.
- `Admin` class inherits from `User` class and overrides the `UserType` property.
- An abstract `Media` class that contains the common properties of all media files. this class should not be directly instantiated.
- `Video` and `Audio` classes inherit from the `Media` class. Each class implements the `MediaType` property.
- `Video` and `Audio` classes implement the `IVideoable` and `IAudioble` interface respectly.
- A `PlayList` class that contains the common properties of all playtracks.
- UserNotification Entity and AdminNotification Entity

### Enums
All Enums are kept in this layer.

### Utils
Helper classes such as validator.

## Service Layer
This layer contains the business logic of the application.
- DTO => defines the shape of Data transfer among different layers.
- Service => deals with business logic, all the validation, and transformation should be done here.
- Utils => Helper classes such as factory functions.

### More on Service
- `MediaService` and `UserService` are only accessable by Admins. This is achieved through mandotary Admin injection. 
- Media repository and user repository are also injected as dependencies of each service.
- Data validation and business logic are handled in the service layer, but CRUD operations are communicated in the repository layer.
- `PlayListService` deals the CRUD operations of playlists.
- `PlaylistControlService` deals with the play, pause, stop operations of the playtracks.


## Infrastructure Layer
This is the outermost layer of the application.
- It contains the entry point to the application - `Program.cs`.
- Data from external sources such as databases is accessed in this layer.
- The implementation of the repositories is in this layer.

## Summary
The application data is an one-way flow from the Infrastructure layer to the Core layer. The Service layer is responsible for the business logic and data transformation that sits inbetween the Core and Infrastructure layers.



## SOLID Principles
- Single Responsibility Principle: each class is responsible for a single task.
- Open/Closed Principle: the classes are open for extension but closed for modification. For instance, the `User` class is open for extension by derived classes but closed for modification.
- Liskov Substitution Principle: derived classes can be substituted for their base classes. For instance, the `Audio` class can be substituted for the `Media` class.
- Interface Segregation Principle: the interfaces are specific to the classes that use them. For instance, the `IVideoable` and `IAudioble` interfaces are specific to the `Video` and `Audio` classes.
- Dependency Inversion Principle: the high-level modules depend on abstractions, not on concrete implementations. For instance, the `MediaService` class depends on the `IMediaRepository` interface, not on the `MediaRepository` class.

## Factory Pattern
- Create a `MediaFactory` class that creates a media object based on the media type.
- Create a `UserFactory` class that creates a user object based on the user type.
- Create a `PlayListFactory` class that creates a playtrack object.



### Useful tools

**Create project references** 
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

## Other notes

## Interface vs. Abstract Class
If the classes that use the interface or abstract class share common implementation, an abstract class is a better choice. If the classes will implment the interface seperately, then an interface is more appropriate.

## Service layer
It deals with data transformation and validation, it shouldn't deal with source.
The source should come from the repositorary interface in the core. 
Dependency injection is the preferred way to do it.


