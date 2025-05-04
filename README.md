# Flexi 
## Overview
Flexi is a mobile application designed to be your ultimate travel companion, helping travelers of all kinds plan their adventures, share experiences, and connect with the community. With AI-driven suggestions powered by xAI's Grok, Flexi helps users maximize their time in specific areas of New Zealand(we will start here as we are based in NZ). The app includes gamification features to encourage sharing, such as badges for reviews, flags for visited locations, and a Strava-inspired social feed to showcase travel experiences. Flexi is built using .NET, Hybrid Blazor with MAUI for cross-platform mobile development, and integrates with a backend API for data management.

## Features
- **Travel Planning**: Users can plan their day in specific areas of New Zealand, with AI-driven suggestions tailored to their interests (e.g., cultural experiences, food tours, relaxation, or adventure).
- **Community Sharing**: Users can share their travel experiences in a social feed, similar to Strava, and earn badges or flags for their contributions.
- **Gamification**:
  - **Badges**: Earn badges for activities like "First Review," "Frequent Traveler," or "Cultural Explorer" based on contributions and travel milestones.
  - **Flags**: Collect virtual flags for each new area visited in New Zealand (e.g., a flag for Auckland, Queenstown, etc.).
  - **Social Feed**: Share travel experiences (photos, reviews, itineraries) with the community, with likes and comments to encourage engagement.
- **Personalized Suggestions**: Powered by xAI's Grok, the app provides tailored recommendations based on user interests and travel history.

## Tech Stack
- **Frontend**: Hybrid Blazor with .NET MAUI (cross-platform mobile app for iOS and Android)
- **Backend**: ASP.NET Core Web API (RESTful API for data management)
- **AI Integration**: xAI's Grok for generating personalized travel suggestions
- **Database**: SQLite (local storage for offline use) and SqlServer(cloud database for user data, reviews, and gamification stats)
- **Authentication**: JWT-based authentication using ASP.NET Core Identity
- **Deployment**: Azure for hosting the backend API and PostgreSQL database

## Project Structure
Flexi/ ├── Flexi.App/ # Hybrid Blazor MAUI Mobile App │ ├── Components/ # Blazor components (e.g., TravelCard, BadgeDisplay, SocialFeed) │ ├── Pages/ # Blazor pages (e.g., Home, PlanTrip, SocialFeed, Profile) │ ├── Services/ # Services for API calls and Grok integration │ └── MauiProgram.cs # MAUI app entry point ├── Flexi.Api/ # ASP.NET Core Web API │ ├── Controllers/ # API controllers (e.g., TripController, ReviewController, GamificationController) │ ├── Models/ # Data models (e.g., Trip, Review, User, Badge, Flag) │ ├── Services/ # Business logic (e.g., TripService, GamificationService, GrokService) │ └── Program.cs # API entry point ├── Flexi.Tests/ # Unit and integration tests └── README.md # Project documentation
text
CollapseWrapCopy

## System Architecture
The system follows a client-server architecture with the following components:
1. **Mobile App (Hybrid Blazor MAUI)**: Handles the UI and user interactions.
2. **Backend API (ASP.NET Core)**: Manages data (trips, reviews, user profiles, gamification) and integrates with xAI's Grok.
3. **xAI Grok**: Provides AI-driven suggestions for travel planning.
4. **Database**: PostgreSQL for persistent storage and SQLite for offline caching.
### Mermaid Diagram: System Architecture
```mermaid
graph TD
    A[Mobile App <br> Hybrid Blazor MAUI] -->|HTTP Requests| B[ASP.NET Core API]
    B -->|Queries| C[PostgreSQL Database]
    A -->|Local Storage| D[SQLite Database]
    B -->|API Calls| E[xAI Grok]
    E -->|Suggestions| B
    A -->|User Input| F[User]
    F -->|Reviews/Plans/Shares| A
    B -->|Gamification Logic| G[Gamification Engine]
    G -->|Badges/Flags| C
Mermaid Diagram: API Flow (Share Experience and Earn Badge)
mermaid
CollapseWrapCopy
sequenceDiagram
    participant U as User
    participant M as Mobile App
    participant A as API
    participant G as Gamification Engine
    participant D as Database
U->>M: Share Travel Experience
    M->>A: POST /api/experiences
    A->>D: Save Experience
    A->>G: Check Gamification Rules
    G-->>A: Award Badge/Flag (e.g., "First Share" Badge)
    D-->>A: Update User Profile with Badge/Flag
    A-->>M: Return Updated Profile
    M-->>U: Display New Badge/Flag in Profile
Mermaid Diagram: Social Feed Interaction
mermaid
CollapseWrapCopy
sequenceDiagram
    participant U as User
    participant M as Mobile App
    participant A as API
    participant D as Database
    U->>M: View Social Feed
    M->>A: GET /api/feed
    A->>D: Fetch Recent Experiences
    D-->>A: Return Experiences
    A-->>M: Return Feed Data
    M-->>U: Display Feed
    U->>M: Like/Comment on Post
    M->>A: POST /api/feed/{id}/interact
    A->>D: Update Post with Like/Comment
    A-->>M: Confirm Interaction
API Endpoints
The backend API exposes RESTful endpoints for the mobile app to interact with. Below are the key endpoints, updated to include gamification and social features:
Trip Endpoints
    • GET /api/trips - Fetch all trips or experiences in a specific area 
        ○ Query Params: area (e.g., "Auckland"), category (e.g., "food", "culture")
        ○ Response: List of trips/experiences
    • POST /api/trips/plan - Generate a day plan with AI suggestions 
        ○ Body: { "area": "Auckland", "interests": ["food", "culture"], "date": "2025-03-23" }
        ○ Response: Suggested plan with activities
    • GET /api/trips/{id} - Fetch details of a specific trip/experience 
        ○ Response: Trip details
Review Endpoints
    • GET /api/reviews - Fetch reviews for a trip/experience 
        ○ Query Params: tripId
        ○ Response: List of reviews
    • POST /api/reviews - Create a new review (triggers gamification check) 
        ○ Body: { "tripId": 1, "rating": 5, "comment": "Loved the food tour!" }
        ○ Response: Created review + earned badge (if applicable)
Experience Sharing Endpoints
    • POST /api/experiences - Share a travel experience to the social feed 
        ○ Body: { "title": "My Auckland Food Tour", "description": "Amazing day!", "photos": ["url1", "url2"], "area": "Auckland" }
        ○ Response: Created experience + earned badge/flag (if applicable)
    • GET /api/feed - Fetch the social feed 
        ○ Query Params: userId (optional, to filter by friends/followers)
        ○ Response: List of shared experiences
    • POST /api/feed/{id}/interact - Like or comment on a shared experience 
        ○ Body: { "action": "like" } or { "action": "comment", "text": "Looks fun!" }
        ○ Response: Updated experience
Gamification Endpoints
    • GET /api/gamification/badges - Fetch all available badges 
        ○ Response: List of badges
    • GET /api/gamification/flags - Fetch all available flags (one per area in New Zealand) 
        ○ Response: List of flags
    • GET /api/users/{id}/gamification - Fetch a user's badges and flags 
        ○ Response: User’s earned badges and flags
User Endpoints
    • POST /api/users/register - Register a new user
    • POST /api/users/login - Authenticate a user and return JWT token
    • GET /api/users/{id} - Fetch user profile (includes badges, flags, and shared experiences)
UI Design
The mobile app UI is built using Hybrid Blazor with MAUI, providing a seamless experience across iOS and Android. Key pages now include gamified elements:
    1. Home Page: 
        ○ Displays popular trips/experiences and areas in New Zealand.
        ○ Search bar to filter by area or category (e.g., "food", "culture", "relaxation").
        ○ "Plan My Trip" button to start planning.
        ○ Quick access to the social feed.
    2. Plan Trip Page: 
        ○ Form to input area, interests, and date.
        ○ Displays AI-generated plan with a timeline of activities.
        ○ Option to save the plan or share it to the social feed (earning a badge/flag).
    3. Social Feed Page: 
        ○ Displays a Strava-like feed of shared travel experiences from the community.
        ○ Users can like, comment, and share posts.
        ○ Option to share a new experience with photos and descriptions.
    4. Profile Page: 
        ○ Displays user info, saved plans, and shared experiences.
        ○ Gamification Section: Shows earned badges (e.g., "First Share," "Foodie Explorer") and collected flags for visited areas.
        ○ Option to edit profile or log out.
UI Component Structure (Mermaid)
mermaid
CollapseWrapCopy
classDiagram
    class AppShell {
        +NavigateTo(page)
    }
    class HomePage {
        +TripList
        +SearchBar
        +PlanButton
        +FeedPreview
    }
    class PlanTripPage {
        +InputForm
        +PlanTimeline
        +SaveButton
        +ShareButton
    }
    class SocialFeedPage {
        +FeedList
        +ShareExperienceButton
        +LikeCommentComponent
    }
    class ProfilePage {
        +UserInfo
        +SavedPlans
        +SharedExperiences
        +BadgeDisplay
        +FlagCollection
        +EditProfileButton
        +LogoutButton
    }
AppShell --> HomePage
    AppShell --> PlanTripPage
    AppShell --> SocialFeedPage
    AppShell --> ProfilePage
Gamification Logic
Badges
    • First Share: Awarded after a user shares their first experience.
    • Frequent Traveler: Awarded after visiting 5 unique areas in New Zealand.
    • Cultural Explorer: Awarded after completing 3 cultural activities (e.g., museum visits, cultural tours).
    • Foodie: Awarded after reviewing 5 food-related experiences.
    • Social Star: Awarded after receiving 50 likes on shared experiences.
Flags
    • Users earn a virtual flag for each new area they visit in New Zealand (e.g., Auckland, Wellington, Christchurch, Queenstown, etc.).
    • Flags are displayed in the user’s profile as a visual collection.
Social Feed
    • The feed encourages sharing by showcasing experiences in a visually appealing way (photos, descriptions, likes, comments).
    • Sharing an experience automatically checks for badge/flag eligibility.
Setup Instructions
Prerequisites
    • .NET 8 SDK
    • Visual Studio 2022 (with MAUI workload)
    • PostgreSQL
    • Azure account (for deployment)
    • xAI Grok API key
Steps
    1. Clone the Repository: 
text
CollapseWrapCopy
git clone https://github.com/your-repo/Flexi.git
cd Flexi
    2. Backend Setup: 
        ○ Navigate to Flexi.Api.
        ○ Update appsettings.json with your PostgreSQL connection string and xAI Grok API key.
        ○ Run migrations: 
text
CollapseWrapCopy
dotnet ef database update
        ○ Start the API: 
text
CollapseWrapCopy
dotnet run
    3. Mobile App Setup: 
        ○ Navigate to Flexi.App.
        ○ Update API base URL in Services/ApiService.cs.
        ○ Run the app on a simulator/emulator: 
text
CollapseWrapCopy
dotnet run --project Flexi.App
    4. Testing: 
        ○ Run unit tests: 
text
CollapseWrapCopy
dotnet test Flexi.Tests
Deployment
    • Backend: Deploy the ASP.NET Core API to Azure App Service.
    • Database: Host PostgreSQL on Azure Database for PostgreSQL.
    • Mobile App: Publish the MAUI app to the iOS App Store and Google Play Store.
Future Improvements
    • Add leaderboards to rank users based on badges/flags earned.
    • Integrate push notifications for new likes/comments on shared experiences.
    • Expand to other countries beyond New Zealand, with more flags to collect.
Contributors
    • [Your Name] - Lead Developer
text
CollapseWrapCopy

---
### Explanation of Changes
#### App Name and Focus
- Renamed the app to **Flexi** to reflect its role as a friendly travel buddy for all travelers, not just outdoor enthusiasts.
- Broadened the focus to include diverse travel interests like food, culture, relaxation, and more, ensuring inclusivity.
#### Gamification Features
- **Badges**: Added a system to reward users for contributions (e.g., sharing, reviewing) and milestones (e.g., visiting multiple areas, completing specific types of activities).
- **Flags**: Introduced a flag collection system to gamify exploration of New Zealand areas, similar to collecting virtual souvenirs.
- **Social Feed**: Created a Strava-inspired feed where users can share experiences, like/comment on posts, and earn badges for engagement.
#### API Updates
- Added endpoints for sharing experiences (`/api/experiences`), interacting with the social feed (`/api/feed`), and retrieving gamification data (`/api/gamification`).
- The backend now includes a **Gamification Engine** (represented in the architecture diagram) to handle logic for awarding badges and flags.
#### UI Updates
- Added a **Social Feed Page** to display shared experiences and encourage community interaction.
- Updated the **Profile Page** to showcase badges and flags, making the gamification visible and rewarding.
- The **Plan Trip Page** now includes an option to share the plan directly to the social feed, tying planning to gamification.
#### Mermaid Diagrams
- Updated the **System Architecture** diagram to include the Gamification Engine as a component.
- Added a new **API Flow (Share Experience and Earn Badge)** diagram to show how sharing triggers gamification rewards.
- Added a **Social Feed Interaction** diagram to illustrate how users interact with the feed.
- Updated the **UI Component Structure** to include the new Social Feed Page and gamification elements in the Profile Page.
---
### Next Steps
This updated design incorporates gamification and inclusivity for all travelers while keeping the core functionality