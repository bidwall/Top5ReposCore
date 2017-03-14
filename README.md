Top5ReposCore 
-------------
Forked from [Top5Repos](https://github.com/bidwall/Top5Repos), welcome Top5ReposCore!

This is an ASP.NET Core web application which lists the top five starred GitHub repositories, alongside some basic information for a given user.

It uses the [GitHub API](https://api.github.com) to query information about the user and their repositories.

This solution uses...

- C#
- ASP.NET Core 1.1
- Bower
- xUnit
- Moq
- FluentAssertions
- gulp

Install
------------

Restore nuget packages

`dotnet restore`

Restore npm packages

`npm install`

Restore bower packages

`bower install`

Restore static resources (minified css/js & bootstrap fonts)

`gulp`
