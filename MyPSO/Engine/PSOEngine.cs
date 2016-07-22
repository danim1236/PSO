using System;
using System.Windows.Forms;

namespace MyPSO.Engine
{
    public abstract class PSOEngine
    {
        public event IterationStepHandler IterationStepEvent;

        protected virtual void OnIterationStepEvent(IterationStepHandlerArgs args)
        {
            var handler = IterationStepEvent;
            if (handler != null) handler(this, args);
        }

        public void InitializeSwarm(ParticleSwarm swarm)
        {
            //throw new NotImplementedException();
        }

        public void IterationStep(ParticleSwarm swarm, int numSteps, Func<double[], double> costFunction)
        {
            for (int i = 0; i < numSteps; i++)
            {
                foreach (var particle in swarm.Particles)
                {
                    particle.Cost = costFunction(particle.Position);
                }
            }
        }

        public void IterateFunc(ParticleSwarm swarm, int numIterations, int stepSize, Func<double[], double> costFunction)
        {
            while (numIterations > 0)
            {
                var thisStep = Math.Min(stepSize, numIterations);
                IterationStep(swarm, thisStep, costFunction);
                OnIterationStepEvent(new IterationStepHandlerArgs {Swarm = swarm});
                numIterations -= thisStep;
                Application.DoEvents();
            }
        }

        public abstract void Iterate(ParticleSwarm swarm, int numIterations, int stepSize);
    }
}