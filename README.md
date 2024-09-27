# fiap-tech-challenge-delete-fase3

This is a .NET 8 project that provides a WorkerService for consume messages from a queue. The project is written in C# and uses several technologies and frameworks.

[![Build](https://github.com/lucasfm95/fiap-tech-challenge-fase3/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/lucasfm95/fiap-tech-challenge-fase3/actions/workflows/continuous-integration.yml)

## Technologies Used

- **.NET 8**: The latest version of the .NET framework, used for building high-performance, cross-platform applications.
- **C#**: The primary programming language used in this project.
- **.NET Core**: A framework for building .net applications.
- **Entity Framework Core**: An object-relational mapper (ORM) that simplifies data access by letting you work with relational data using domain-specific objects.
- **PostgreSQL**: The database used for persisting data.
- **Prometheus**: is an open-source systems monitoring and alerting toolkit, used to get metrics about the app.

## Project Structure

- `src/Fiap.TechChallenge.Domain`: This project contains the domain entities and repositories interfaces.
- `src/Fiap.TechChallenge.Application`: This project contains the application services and DTOs.
- `src/Fiap.TechChallenge.Worker`: This project is the workerService layer that consumes messages from queue.
- `src/Fiap.TechChallenge.Infrastructue`: This project contains the implementation of the repositories and the database context.
- `tests/Fiap.TechChallenge.UnitTests`: This project contains the unit tests for the application services.

## Getting Started With Docker Compose
1. Clone the repository.
2. Ensure rabbitMQ and PostgresDb is running on your machine.
3. Navigate to the root directory.
4. Ensure Docker is installed and up on your machine.
5. Run command `docker-compose up -d` in the terminal
6. The worker will start to listen the queue on rabbitMq.

## Getting Started With local app
1. Clone the repository.
2. Ensure rabbitMQ and PostgresDb is running on your machine.
3. Navigate to the `src/Fiap.TechChallenge.Worker` directory.
4. Run `dotnet restore` to restore the NuGet packages.
5. Update the connection string in the `launchSettings.json` file.
6. Run `dotnet run` to start the application.

## Running only the app With Docker
1. Ensure Docker is installed and up on your machine.
2. Ensure rabbitMQ and PostgresDb is running on your machine.
3. Navigate to the root directory.
4. Run `docker build -t tech-challenge-delete .` to start the application.
5. Run `docker run --name tech-challenge-delete -p 8090:80 -d tech-challenge-delete` to start the container.
6. The worker will start to listen the queue on rabbitMq.

## Running the Tests
1. Navigate to the `src/Fiap.TechChallenge.UnitTests` directory.
2. Run `dotnet test` to run the tests.
3. The test results will be displayed in the console.

## Queues

- `contact-delete-queue`: Consume message wich is a representation of a contact and delete it in postgres Db.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.