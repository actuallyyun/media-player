# Media Player Application

Build a media player application that demonstrates advanced topics in C# programming, including SOLID principles, Clean architecture, Factory pattern, Singleton pattern, Observer pattern, object lifetime, and thread safety.

## Features

The media player application is a robust software which contains collections of different media files and users. Each user could create their own playtracks and perform other actions on the media file in their playtracks. One user can have multiple playtracks. Application should not have identical users.

- Only Admins of the application should be able to add, remove, update, delete all the files and users in the application.
- Users should be able to manage their playtracks, including adding, removing, playing, pausing, stopping the media files.
- Media files can be further adjusted while playing:
    - Videos can change volume, brightness (can simply use `int` or `string`)
    - Audios can change volume, sound effect (can simply use `int` or `string`)
- Handle potential errors and exceptions gracefully, providing meaningful error messages to the user.

## Requirement:

- Design a solid and clean architecture for the media player application.
- While all instances of database, repositories, services, controllers could be initiated in `Program.cs`, all functionalities should be handled via controllers only.