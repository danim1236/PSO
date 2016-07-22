using System;
using System.Windows.Forms;
using MyPSO.Engine;

namespace MyPSO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var swarm = new ParticleSwarm(20, new double[] { -200, -200 }, new double[] { 200, 200 });
            var engine = new BananaEngine();
            engine.InitializeSwarm(swarm);
            //engine.Iterate(swarm, 200, 10);
            var form = new Form1();
            engine.IterationStepEvent += form.IterationStepEvent;
            Application.Run(form);
        }
    }
}
