# Zombie.js Driver
A dynamic driver for running [Zombie.js][1] from .NET

[![Build status][2]][3]
[![NuGet version][4]][5]

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
The following example searches "zombie.js" and checks that assaf's site is the
first result.

```csharp
// C# version                                               
dynamic zombie = new ZombieDriver();                        

zombie.visit("https://duckduckgo.com");
zombie.fill("q", "zombie.js");
zombie.click("input[type=submit]");

zombie.assert.text("title", "zombie.js at DuckDuckGo");
zombie.assert.text(".result:first-child .result__a", "Zombie.js");
```

```javascript
// JavaScript version
const zombie = new Browser();

zombie.visit("https://duckduckgo.com").then(() => {
  zombie.fill("q", "zombie.js");
  zombie.click("input[type=submit]").then(() => {

    zombie.assert.text("title", "zombie.js at DuckDuckGo");
    zombie.assert.text(".result:first-child .result__a", "Zombie.js");
  });
});
```

The driver's API is more or less identical to the JavaScript version.  That's
because it passes method calls directly to Zombie.js running in Node behind the
scenes.

### Navigation
Visit [Colin the Geek][6] and navigate back to the earliest post:

```csharp
dynamic zombie = new ZombieDriver();

zombie.visit("http://colinthegeek.com");
zombie.assert.text("title", "Colin the Geek");

// Page back to the earliest post
while (zombie.query("a.older") != null)
{
    zombie.click("a.older");
}
zombie.assert.text(".post:last-child h1", "Hello World");

// Open the earliest post
zombie.click(".post:last-child a");
zombie.assert.text("title", "Hello World · Colin the Geek");
zombie.assert.url("http://colinthegeek.com/2015/01/31/hello-world/");
```

### Forms
Fill out the Facebook sign up form and check the required email validation is
triggered:

```csharp
dynamic zombie = new ZombieDriver();
zombie.waitDuration = "30s";

zombie.visit("https://www.facebook.com");
zombie.assert.text("title", "Facebook - Log In or Sign Up");

zombie.fill("firstname", "Rick");
zombie.fill("lastname", "Grimes");
zombie.fill("input[type=password]", "SheriffR1ck");

zombie.select("birthday_day", "14");
zombie.select("birthday_month", "9");
zombie.select("birthday_year", "1983");

zombie.choose("Male");

zombie.click("button[name=websubmit]");

//  Validation error - Rick has no email address (due to the zombie apocolypse)
zombie.assert.text("#reg_error_inner", "An error occurred. Please try again.");
```

[1]: http://zombie.js.org/
[2]: https://ci.appveyor.com/api/projects/status/ba0wcbvar1vo5voy?svg=true
[3]: https://ci.appveyor.com/project/ColinOrr/zombie-net
[4]: https://badge.fury.io/nu/zombie.js.driver.svg
[5]: https://www.nuget.org/packages/zombie.js.driver
[6]: http://colinthegeek.com
