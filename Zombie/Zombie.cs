using Zombie.Properties;

namespace Zombie
{
    /// <summary>
    /// A dynamic wrapper for the Zombie.js browser.
    /// </summary>
    public class ZombieDriver : Bridge
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZombieDriver() : base(Resources.ZombieDriver) { }
    }
}
