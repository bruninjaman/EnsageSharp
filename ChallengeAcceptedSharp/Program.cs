﻿namespace ChallengeAcceptedSharp
{
    using Core;
    class Program
    {
        private static readonly Bootstrap BootstrapInstance = new Bootstrap();
        private static void Main()
        {
            BootstrapInstance.Initialize();
        }
    }
}
