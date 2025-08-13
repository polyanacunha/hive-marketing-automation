# HiveFrontend

## Development server

To run the application on development, without CORS errors, run the Hive.Frontend project with:

```bash
ng serve --ssl
```

And then run the Hive.API project with:

```bash
dotnet dev-certs https --trust
```

and then:

```bash
dotnet run
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

## para rodar as migrations, rodar com o startup-project

dotnet ef migrations add InitialCreate --startup-project ../Hive.API

dotnet ef database update --startup-project ../Hive.API
