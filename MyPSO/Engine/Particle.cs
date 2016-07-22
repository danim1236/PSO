namespace MyPSO.Engine
{
    public class Particle
    {
        public Particle(ParticleSwarm swarm)
        {
            Swarm = swarm;
        }

        public double[] Position { get; set; }
        public double[] Velocity { get; set; }
        public double Cost { get; set; }

        public double[] BestPosition { get; set; }
        public double BestCost { get; set; }

        public double[] BestGlobalPosition
        {
            get { return Swarm.BestPosition; }
            set { Swarm.BestPosition = value; }
        }

        public double BestGlobalCost
        {
            get { return Swarm.BestCost; }
            set { Swarm.BestCost = value; }
        }

        public ParticleSwarm Swarm { get; private set; }
    }
}
