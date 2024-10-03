# Media Player 

This project is a media management system built using C# and .NET, following Clean Architecture principles. It separates the application into three layers: Core, Service, and Infrastructure, promoting scalability, maintainability, and modularity.

## Getting Started

### Prerequisites
Before you can run the project, ensure you have the following installed:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/)

### Installation
1. **Clone the Repository**:  
   Clone the project from the GitHub repository to your local machine:
   ```bash
   git clone https://github.com/actuallyyun/media-player.git
   ```
2. **Navigate to the Project Directory**:  
   ```bash
   cd MediaPlayer.Infrastructure
   ```

3. **Install Dependencies**:
Run the following command to restore the necessary packages:

```bash
dotnet restore
```

4. **Build the Project**:
Use the following command to build the project:
```bash
dotnet build
```

5. **Run the Application:**
Once the project is built, use the following command to run the application:

```bash
dotnet run
```

### Running Tests
To run the unit tests for the project, navigate to the test folder and use the following command:

```bash
dotnet test
```

## Features
- Authentication: Role-based authentication for users and admins.
- Media Management: Manage audio and video files, supporting multiple media types.
- Playlists: Create, manage, and control playlists (play, pause, stop functionality).
- Notification System: Separate notification entities for users and admins.

## Design Principles

### Clean Architecture
The application adheres to Clean Architecture principles, separating responsibilities into three main layers:

### 1. Core Layer
This layer contains the domain entities, interfaces, and enums.

#### Abstractions
- `INofity` interface defines the shape of global notification.
- `IMediaPlayerMonitor` interface defines the shape of the media player status observer.
- `IAudioble` and `IVideoable` interfaces define the shape of the audio and video media files.

#### RepositoryAbstractions
These abstractions define the shape of the repositories that manage the user, media and playlists in the application.

They are implemented in the Infrastructure layer.

#### Entities
- A base `User` class that contains the common properties of all users.
- `User` class has a `virtual` property `UserType` that can be overridden by derived classes.
- `Admin` class inherits from `User` class and overrides the `UserType` property.
- An abstract `Media` class that contains the common properties of all media files. this class should not be directly instantiated.
- `Video` and `Audio` classes inherit from the `Media` class. Each class implements the `MediaType` property.
- `Video` and `Audio` classes implement the `IVideoable` and `IAudioble` interface respectly.
- A `PlayList` class that contains the common properties of all playtracks.
- UserNotification Entity and AdminNotification Entity

#### Enums
All Enums are kept in this layer.

#### Utils
Helper classes such as validator.

### 2.Service Layer
This layer contains the business logic of the application.
- DTO => defines the shape of Data transfer among different layers.
- Service => deals with business logic, all the validation, and transformation should be done here.
- Utils => Helper classes such as factory functions.

#### More on Service
- `MediaService` and `UserService` are only accessable by Admins. This is achieved through mandotary Admin injection. 
- Media repository and user repository are also injected as dependencies of each service.
- Data validation and business logic are handled in the service layer, but CRUD operations are communicated in the repository layer.
- `PlayListService` deals the CRUD operations of playlists.
- `PlaylistControlService` deals with the play, pause, stop operations of the playtracks.


### 3. Infrastructure Layer
This is the outermost layer of the application.
- It contains the entry point to the application - `Program.cs`.
- Data from external sources such as databases is accessed in this layer.
- The implementation of the repositories is in this layer.

## SOLID Principles
- Single Responsibility Principle: each class is responsible for a single task.
- Open/Closed Principle: the classes are open for extension but closed for modification. For instance, the `User` class is open for extension by derived classes but closed for modification.
- Liskov Substitution Principle: derived classes can be substituted for their base classes. For instance, the `Audio` class can be substituted for the `Media` class.
- Interface Segregation Principle: the interfaces are specific to the classes that use them. For instance, the `IVideoable` and `IAudioble` interfaces are specific to the `Video` and `Audio` classes.
- Dependency Inversion Principle: the high-level modules depend on abstractions, not on concrete implementations. For instance, the `MediaService` class depends on the `IMediaRepository` interface, not on the `MediaRepository` class.
