# Copilot Instructions for This Repository

This repository contains a Blazor application using a client-server architecture. Please follow these guidelines when generating code or answering questions:

- The **client** project uses Blazor WebAssembly (Blazor WASM) and should not contain any server-side code or direct database access.
- The **server** project is an ASP.NET Core Web API that exposes endpoints consumed by the Blazor client via HTTP (usually using HttpClient).
- Use **dependency injection** for services in both client and server projects.
- Use asynchronous programming (`async`/`await`) for all network and long-running operations.
- For state management in the client, prefer built-in Blazor mechanisms (such as cascading parameters or DI services).
- Follow C# coding conventions: PascalCase for methods and properties, camelCase for local variables, and use explicit access modifiers.
- When writing sample code, use clear and descriptive names for classes, methods, and variables.
- Never downgrade my packages to LTS versions or suggest using outdated libraries/packages.
- NEVER downgrade my dotnet version, the lowest version I use is dotnet 9.0.

Do not generate code that mixes client and server concerns. Always keep UI logic in the client and 
business/data logic in the server unless explicitly instructed otherwise.

I repeat: 
* NEVER downgrade my dotnet version to anything lower than 9.0.
* DO NOT downgrade packages.