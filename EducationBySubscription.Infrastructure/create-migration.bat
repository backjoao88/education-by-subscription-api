@echo off
dotnet-ef migrations add %1 -o Persistence/Migrations -s ../EducationBySubscription.Api