# Zombie.js Driver
A dynamic driver for running [Zombie.js][1] from .NET

[![Build status][2]][3]

## Installation
Zombie.js Driver can be installed from NuGet.  Run the following commands from
the Package Manager Console:

```bash
PM> Install-Package zombie.js
PM> Install-Package zombie.js.driver
```

Note: the `zombie.js` package is a pre-built Windows version of Zombie.js.  If
you already have Node and Zombie.js installed then you can skip this step and
just install the driver.

## Examples
### Getting Started
```csharp
// C# version                                                // JavaScript version
dynamic zombie = new ZombieDriver();                         const zombie = new Browser();

zombie.visit("http://www.google.com");                       zombie.visit("http://www.google.com").then(() => {
zombie.fill("q", "zombie.js");                                  zombie.fill("q", "zombie.js");
zombie.click("input[value=Search]");                            zombie.click("input[value=Search]").then(() => {

zombie.assert.text("title", "zombie.js - Google Search");         zombie.assert.text("title", "zombie.js - Google Search");
zombie.assert.text("#f", "Zombie by assaf - JS.ORG");             zombie.assert.text("#f", "Zombie by assaf - JS.ORG");
                                                               });
                                                            });
```

[1]: http://zombie.js.org/
[2]: https://ci.appveyor.com/api/projects/status/ba0wcbvar1vo5voy?svg=true
[3]: https://ci.appveyor.com/project/ColinOrr/zombie-net
