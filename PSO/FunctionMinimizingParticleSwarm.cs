using PSO.Particles;

namespace PSO
{
	public sealed class FunctionMinimizingParticleSwarm : ParticleSwarm
	{
		public FunctionMinimizingParticleSwarm(
			FunctionBase function,
			int swarmSize)
		{
			// Create the swarm:
			InitSwarm(swarmSize, function);

			// Sort according to the cost of each particle:
			SortParticles();
		}
		//---------------------------------------------------------------------
		public void InitSwarm(int swarmSize, FunctionBase function)
		{
			// Create the array of particles:
			Particles = new FunctionMinimizingParticle[swarmSize];
			for (int i = 0; i < swarmSize; i++)
			{
				// Init with random position in [-3,3]:
				double x = Rnd.NextDouble() * 6 - 3;
				double y = Rnd.NextDouble() * 6 - 3;
				double[] particlePosition = { x, y };

				// Random initial velocity [-3,3]:
				double vx = Rnd.NextDouble() * 6 - 3;
				double vy = Rnd.NextDouble() * 6 - 3;
				double[] particleVelocity = { vx, vy };

				this[i] = new FunctionMinimizingParticle(
					function,
					this,
					particlePosition,
					particleVelocity);
			}
		}
	}
}