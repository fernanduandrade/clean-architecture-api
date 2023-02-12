Exemplo de REST API .NET CORE com CQRS implementado usando DDD seguindo Clean Architecture.
==============================================================

## CI

[![.NET](https://github.com/fernanduandrade/clean-architecture-api/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/fernanduandrade/clean-architecture-api/actions/workflows/dotnet.yml)

## Ajude com uma estrela! :star:

Se você gostou desse projeto, aprendeu algo a partir dele ou está usando em suas aplicações, ajude dando uma estrela. Obrigado!

## Descrição
Examplo de uma aplicação do tipo REST API utilizando .NET CORE, implementando o básico da abordagem de [CQRS](https://docs.microsoft.com/en-us/azure/architecture/guide/architecture-styles/cqrs) e Domain Driven Design.


## Arquitetura [Clean Architecture](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## CQRS

Read Model / Write Model - abordagem Domain Driven Design (usando Entity Framework Core).

Commands/Queries/Domain Eventos utilizam o pacote [MediatR](https://github.com/jbogard/MediatR).


## Validação
Validação dos dados utiliza [FluentValidation](https://github.com/JeremySkinner/FluentValidation)


## Como rodar
1. Crie um banco de dados vazio.
2. Execute o `script.sql`.
2. Altere a conexão no seu appsettings.json.
3. Execute!

## Como rodar testes de integração
1. Tenha o [docker](https://docs.docker.com/engine/install/ubuntu/) instalado.
2. Execute `dotnet test` no projeto [Bot.IntegrationTests/](Bot.IntegrationTests/).