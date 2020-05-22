# my-card-game
How to RUN

Be shure to have MsBuild installed:
https://www.microsoft.com/en-us/download/details.aspx?id=40760

Lounch cmd
C:
CD Windows\Microsoft.NET\Framework\v4.0.30319
msbuild E:\GitHub\my-card-game\MyGame.Console\MyGame.Console.csproj
E:\GitHub\my-card-game\MyGame.Console\bin\Debug\MyGame.Console.exe

Dependencies are mannaged with nugget package manager.
Test can be run within Visual Studio (open solution file).

To the output of the application I added the information about cards in play from previous round (PlayedDeck).