# PurplePete
EnvestNetBuddyAi is a simple Blazor WebAssembly app with a chatbot interface powered by Purple Pete Assistant. It features a robot avatar that provides responses to user messages, typing animations, and a neat chat interface using MudBlazor components. The app can also pull data from Confluence and process the output using a future integration with **LLama3** for more sophisticated responses.

## Prerequisites

Before running the project, ensure that you have the following installed on your machine:

1. **.NET SDK** (6.0 or later):
   - You can download and install it from the official [Microsoft .NET download page](https://dotnet.microsoft.com/download).

2. **Visual Studio Code / Visual Studio** 

3. **MudBlazor NuGet Package**:
   - This project uses the MudBlazor component library for UI elements. The library should be included in the project by default, but ensure it's properly installed with the following command:
     ```bash
     dotnet add package MudBlazor
     ```
### 1. Clone the Repository

First, clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/EnvestNetBuddyAi.git
cd EnvestNetBuddyAi
```

### 2. Install Dependencies

Ensure you have all the required dependencies installed. From the project directory, run:

```bash
dotnet restore
```

### 3. Run the Project

To start the application locally, run:

```bash
dotnet run
```

This will build and launch the project in your default browser. The application should be available at `https://localhost:5001` (or another port if specified).

### 4. Open the Project

If you're using **Visual Studio Code** or **Visual Studio**, open the project and press `F5` (or click the "Run" button) to start the application in debug mode.

---

## Features

- **Chat Interface**: A chat box where you can send messages to Purple Pete.
- **Typing Animation**: Bot replies are typed out one character at a time to mimic typing.
- **Robot Avatar**: Purple Pete's avatar changes while typing and responding.
- **Confluence Service (Optional)**: Can fetch data from Confluence (requires the Confluence Service to be properly configured).
- **LLama3 Integration (Future)**: The application plans to integrate with LLama3 for improved chatbot response management and more sophisticated output formatting.

## File Structure

- `Pages/Home.razor`: Main page of the app with the chat interface and logic.
- `Components/`: Contains reusable UI components like the chat messages display.
  - `Components/Models/ChatMessage.cs`: Defines the `ChatMessage` model, which represents a chat message sent by either the user or the bot.
  - `Components/Services/ConfluenceService.cs`: Contains the `ConfluenceService` responsible for fetching Confluence data.
- `wwwroot/`: Static files like avatars and CSS.

## Technologies Used

- **Blazor WebAssembly**: Front-end framework for building interactive web UIs with C#.
- **MudBlazor**: Component library for creating modern, responsive UI elements.
- **C# & .NET**: Backend and frontend logic written in C#.

## TODO

- **Fix Bug with Chat**: Investigate and resolve any chat-related bugs or issues.
- **Remove Unused Code**: Clean up and remove any unused or redundant code in the project.
- **Clean Up UI**: Improve the UI by refining styles, animations, and responsiveness.
- **Separate Chat Logic into Its Own Service**: Move the chat handling logic into a dedicated service to follow the separation of concerns principle.
- **Implement LLama3**: Integrate LLama3 for enhanced output management and bot response handling. This will allow for smarter, more accurate, and context-aware responses.
- **Fix ConfluenceService**: Ensure the `ConfluenceService` works properly with the current Confluence setup, including fetching the necessary data and handling errors gracefully.
- **Account Info**: Pulls Account information such as x,y,z.
