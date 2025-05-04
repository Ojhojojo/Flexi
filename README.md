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
4. **Database**: SQLServer for persistent storage and SQLite for offline caching.