namespace MyPSO.Engine
{
    public class ParticleSwarm
    {
        public ParticleSwarm(int numParticles, double[] minPosition, double[] maxPosition)
        {
            MaxPosition = maxPosition;
            MinPosition = minPosition;
            Particles = new Particle[numParticles];
            for (int i = 0; i < numParticles; i++)
            {
                Particles[i] = new Particle(this);
            }
        }
        
        public Particle[] Particles { get; private set; }
        
        public double[] MinPosition { get; private set; }
        public double[] MaxPosition { get; private set; }
        
        public double[] BestPosition { get; set; }
        public double BestCost { get; set; }
    }
}