### Introduction

- frontend built in react
- backend built in .NET6
- redis as underlying database

### Development

1. Start redis docker

`docker run -d --name redis-stack-server -p 6379:6379 redis/redis-stack-server:latest`

2. Run backend

`dotnet run`

3. Run frontend

`npm run`

### Todo:

1. Add tests
2. Break out secrets to vault
3. Build in release modes and/or containerize
