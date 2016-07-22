using PSO.FunctionMinimizing.Particles;
using gfoidl.ComputationalIntelligence.Particles;

namespace ParticleSwarm.FunctionMinimizing
{
	public sealed class FunctionMinimizingParticle : Particle
	{
		private FunctionBase _function;
		//---------------------------------------------------------------------
		public FunctionMinimizingParticle(
			FunctionBase function,
            ParticleSwarmClass swarm,
			double[] position,
			double[] velocity)
		{
			_function = function;
			this.Swarm = swarm;
			this.Position = position;
			this.Velocity = velocity;

			this.CalculateCost();
		}
		//---------------------------------------------------------------------
		public override void CalculateCost()
		{
			this.Cost = _function.Function(this.Position);
		}
	}
}