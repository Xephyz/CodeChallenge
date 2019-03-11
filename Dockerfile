FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CodeChallengeV2/CodeChallengeV2.csproj", "CodeChallengeV2/"]
RUN dotnet restore "CodeChallengeV2/CodeChallengeV2.csproj"
COPY ["CodeChallengeV2.Tests/CodeChallengeV2.Tests.csproj", "CodeChallengeV2.Tests/"]
RUN dotnet restore "CodeChallengeV2.Tests/CodeChallengeV2.Tests.csproj"
COPY . .


RUN dotnet build "CodeChallengeV2/CodeChallengeV2.csproj" -c Release -o /app

#RUN dotnet test "CodeChallengeV2.Tests/CodeChallengeV2.Tests.csproj" -c Release -o /app



FROM build AS publish
RUN dotnet publish "CodeChallengeV2/CodeChallengeV2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CodeChallengeV2.dll"]