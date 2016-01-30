using NUnit.Framework;
using Zombie;

namespace Tests
{
    [TestFixture]
    public class Examples
    {
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
            zombie.click(".post:last-child a");
            zombie.assert.text("title", "Hello World · Colin the Geek");
            zombie.assert.url("http://colinthegeek.com/2015/01/31/hello-world/");
        }

        [Test]
        public void FormSubmission()
        {
            dynamic zombie = new ZombieDriver();

            zombie.visit("https://www.facebook.com");
            zombie.assert.text("title", "Facebook - Log In or Sign Up");

            zombie.fill("firstname", "Rick");
            zombie.fill("lastname", "Grimes");
            zombie.fill("input[type=password]", "Sher1ffR1ck");

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
