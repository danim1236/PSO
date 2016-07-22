using System;

namespace MyPSO.Engine
{
    public class BananaEngine : PSOEngine
    {
        public override void Iterate(ParticleSwarm swarm, int numIterations, int stepSize)
        {
            IterateFunc(swarm, numIterations, stepSize, BananaFunction);
        }

        public double BananaFunction(double[] position)
        {
            var x = position[0];
            var y = position[1];
            return 100 * Math.Pow(y - (x * x), 2) + Math.Pow(1 - x, 2);
        }
    }
}