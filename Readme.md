# Projects (Tasks/To Do List)

1. Web API - ASP.NET CORE 7
2. Portal - NuxtJS (VueJS)

# How To Run
There are two ways to run the projects.

1. Docker Compose
2. Manual

## Docker Compose

1. Install Docker Desktop from https://www.docker.com/products/docker-desktop/ .
2. Download project source code from Google Drive or clone the project from my public repository https://github.com/hafiz-personal/iv-msa-test
3. Open terminal and navigate to the root folder of the project.
4. Make sure there is no services running on port 1433, 3000 and 5000 as the project will be using these ports.
5. (Optional) If there is one of the mentioned ports running, open file docker-compose.yml and edit the ports section on the left hand side only. Eg. if you want to change port 1433 to 8080, change from 1433:1433 to 8080:1433.
6. Run `docker-compose up`. This command will setup and configure SQL Server and also spins up the Web Api and the Portal.
7. Wait for the process to be completed until you see listening to ports 3000 and also 5000.
8. Navigate to http://localhost:3000 to start using the system.
9. (Optional) Navigate to http://localhost:5000/swagger to view all the REST APIs.
10. (Optional) Open any database client (SSMS, DBeaver, etc) to connect to the database (userid: sa, password: 123qwe***).

## Manual (TODO)

1. Nuxt - run `npm run dev`
2. ASP.NET Core
3. SQL Server
