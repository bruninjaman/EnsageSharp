using Ensage;

namespace ChallengeAcceptedSharp.Core
{
    using Ensage.Common.Objects.UtilityObjects;
    using Menu;

    internal static class Variables
    {

        public static Hero Hero { get; set; }

        public static Hero Target { get; set; }

        public static MultiSleeper Sleeper { get; set; }

        public static MenuManager Menu { get; set; }

        public static ParticleEffect TargetParticle { get; set; }
    }
}
