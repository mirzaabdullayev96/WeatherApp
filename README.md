# 🌤️ Weather Forecast API Service

Welcome to our **Weather Forecast API Service**! This project provides weather forecasts from three different API providers, utilizing smart caching and rate-limiting techniques for optimal performance. The service is fully Dockerized and deployed to Azure.

## 📂 Project Structure

- **`Controllers`**: Handles all incoming API requests for weather data.

- **`Models`**: Defines data models for handling responses from various weather API providers.

- **`Services`**
  - **Implementations**: Contains services that integrate with different weather APIs.
  - **Interfaces**: Defines the contracts for service implementations.

- **`Utilities`**: Provides helper functions and extensions to support API calls and data handling.

- **`Validators`**: Validates incoming requests (e.g., city, country, datetime fields) using **FluentValidation**.

- **Configuration Files**
  - `appsettings.json`: Configures caching, rate limiting, and other settings.
  - `Dockerfile`: Instructions for containerizing the application.

## 🚀 Features

- **🔄 Double Caching**: 
  - **5-Minute Response Caching**: Utilizes C# response caching to store responses for 5 minutes.
  - **1-Hour Redis Caching**: Caches data for 1 hour using **Redis**, reducing multiple requests to external APIs.

- **⏱️ Rate Limiting**: Restricts API usage to 60 requests per hour using **AspNetCoreRateLimit**.

- **🛠️ Validation**: Ensures valid input for city, country, and datetime fields with **FluentValidation**.

- **🧰 Libraries Used**: NewtonJson, Redis, FluentValidation, AspNetCoreRateLimit.

## 🌐 Deployment

The application is **Dockerized** and deployed to **Azure**. You can access the deployed service [here](https://weatherappforecast-c2g0bnb5ema6gzf3.westeurope-01.azurewebsites.net/WeatherForecast/2024-09-12?Country=IT&City=Milan).
Swagger Documentation: The Swagger UI is publicly available to explore the API endpoints. A **Postman Collection** is also available for testing the API.

## 📥 Getting Started

1. **Clone the repository**.
2. **Run Docker**: Execute `docker-compose up` to start the application.
3. **Test with Postman**: Use our [Postman Collection](https://github.com/mirzaabdullayev96/WeatherApp/blob/main/WeatherForecast.postman_collection) to test the API endpoints.

---