using NUnit.Framework;
using Zombie;

namespace Tests
{
    [TestFixture]
    public class Examples
    {
        /// <summary>
        /// Search for "zombie.js" and check it's the first result.
        /// </summary>
        [Test]
        public void GettingStarted()
        {
            dynamic zombie = new ZombieDriver();

            zombie.visit("https://duckduckgo.com");
            zombie.fill("q", "zombie.js");
            zombie.click("input[type=submit]");

            zombie.assert.text("title", "zombie.js at DuckDuckGo");
            zombie.assert.text(".result:first-child .result__a", "Zombie.js");
        }

        /// <summary>
        /// Visit my blog and navigate back to the earliest post.
        /// </summary>
        [Test]
        public void Navigation()
        {
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
            try { zombie.click(".post:last-child a"); }
            catch
            {
                // Disqus JavaScript intermittently throws errors :-(
            }

            zombie.assert.text("title", "Hello World · Colin the Geek");
            zombie.assert.url("http://colinthegeek.com/2015/01/31/hello-world/");
        }

        /// <summary>
        /// Fill out the Facebook sign up form and check the required email validation is triggered.
        /// </summary>
        [Test]
        public void FormSubmission()
        {
            dynamic zombie = new ZombieDriver();
            zombie.waitDuration = "30s";

            try { zombie.visit("https://www.facebook.com"); }
            catch { /* We get a 404 from one of Facebook's frames :-( */ }
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
        }
    }
}
