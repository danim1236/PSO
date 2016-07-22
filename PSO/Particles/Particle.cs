using System;
using System.Linq;

namespace PSO.Particles
{
	/// <summary>
	/// Provides a basic implementation of a paricle.
	/// </summary>
	public abstract class Particle : IComparable<Particle>
	{
		#region Felder
		protected static Random Rnd = new Random();
		protected double BestCost = double.MaxValue;
		#endregion
		//---------------------------------------------------------------------
		#region Eigenschaften
		/// <summary>
		/// The cost for this particle. The lower the better.
		/// </summary>
		public double Cost { get; set; }
		//---------------------------------------------------------------------
		/// <summary>
		/// The current position of the particle.
		/// </summary>
		public virtual double[] Position { get; set; }
		//---------------------------------------------------------------------
		/// <summary>
		/// The best position of this particle so far.
		/// </summary>
		/// <remarks>pBest.</remarks>
		public double[] BestPosition { get; protected set; }
		//---------------------------------------------------------------------
		/// <summary>
		/// Indexer.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <exception cref="IndexOutOfRangeException">
		/// Der Index ist außerhalb der gültigen Grenzen.
		/// </exception>
		public double this[int index]
		{
			get { return Position[index]; }
			set { Position[index] = value; }
		}
		//---------------------------------------------------------------------
		/// <summary>
		/// The velocity of the particle.
		/// </summary>
		public double[] Velocity { get; protected set; }
		//---------------------------------------------------------------------
		/// <summary>
		/// The particle swarm to whom this particle belongs.
		/// </summary>
		public ParticleSwarm Swarm { get; protected set; }
		#endregion
		//---------------------------------------------------------------------
		#region Methoden
		/// <summary>
		/// Calculates the cost for this particle.
		/// </summary>
		public abstract void CalculateCost();
		//---------------------------------------------------------------------
		/// <summary>
		/// Updates the history for this particle.
		/// </summary>
		public void UpdateHistory()
		{
			if (Cost < BestCost)
			{
				BestCost = Cost;
				BestPosition = GetArrayCopy();
			}
		}
		//---------------------------------------------------------------------
		/// <summary>
		/// Updates the position (and velocity) of the particle.
		/// </summary>
		/// <param name="bestPositionOfSwarm">
		/// The current best position of the particle swarm.
		/// </param>
		public void UpdateVelocityAndPosition(double[] bestPositionOfSwarm)
		{
			if (BestPosition == null)
				UpdateHistory();

			// Determine maximum allowed velocity:
			double xmax = Math.Max(
				Math.Abs(Position.Min()),
				Math.Abs(Position.Max()));
			double vmax = Swarm.PercentMaximumVelocityOfSearchSpace * xmax;

			// Range-Check-Elimination: Therefore get a reference to the arrays
			// on the local stack -> improvement of speed:
			double[] localVelocity = Velocity;
			double[] localPosition = Position;
			double[] localBestPosition = BestPosition;

			for (int i = 0; i < localVelocity.Length; i++)
			{
				// Factors for calculating the velocity:
				double c1 = Swarm.TendencyToOwnBest;
				double r1 = Rnd.NextDouble();
				double c2 = Swarm.TendencyToGlobalBest;
				double r2 = Rnd.NextDouble();
				double m = Swarm.Momentum;

				// New velocity of the particle:
				double newVelocity =
					m * localVelocity[i] +
					c1 * r1 * (localBestPosition[i] - localPosition[i]) +
					c2 * r2 * (bestPositionOfSwarm[i] - localPosition[i]);

				// Limit the velocity to the maximum value:
				if (newVelocity > vmax)
					newVelocity = vmax;
				if (newVelocity < -vmax)
					newVelocity = -vmax;

				// Assign new velocity and calculate the new position:
				localVelocity[i] = newVelocity;
				localPosition[i] += localVelocity[i];
			}
		}
		#endregion
		//---------------------------------------------------------------------
		#region Private Methoden
		/// <summary>
		/// Gets a copy of the current solution vector.
		/// </summary>
		private double[] GetArrayCopy()
		{
			var tmp = new double[Position.Length];
			Position.CopyTo(tmp, 0);

			return tmp;
		}
		#endregion
		//---------------------------------------------------------------------
		#region IComparable<Particle> Member
		/// <summary>
		/// Compares to particles. Used for sorting.
		/// </summary>
		public int CompareTo(Particle other)
		{
			if (other == null)
				throw new ArgumentNullException("other");
			//-----------------------------------------------------------------
			if (this == other || Cost == other.Cost)
				return 0;

		    return Cost > other.Cost ? 1 : -1;
		}
		#endregion
	}
}