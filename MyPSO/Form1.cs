using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyPSO.Engine;

namespace MyPSO
{
    public partial class Form1 : Form
    {
        private readonly ParticleSwarm _swarm;
        private readonly BananaEngine _engine;
        private readonly Bitmap _bitmap;

        public Form1()
        {
            InitializeComponent();
            _swarm = new ParticleSwarm(20, new double[] { -200, -200 }, new double[] { 200, 200 });
            _engine = new BananaEngine();
            _engine.InitializeSwarm(_swarm);
            _bitmap = CreateBitmap();
        }

        private Bitmap CreateBitmap()
        {
            var width = _swarm.MaxPosition[0] - _swarm.MinPosition[0];
            var height = _swarm.MaxPosition[1] - _swarm.MinPosition[1];

            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            var bWidth = bitmap.Width;
            var bHeight = bitmap.Height;

            var rWidth = bWidth/width;
            var rHeight = bHeight/height;

            var vals = new List<List<double>>(bHeight);
            for (int i = 0; i < bHeight; i++)
            {
                var row = new List<double>(bWidth);
                for (int j = 0; j < bWidth; j++)
                {
                    double y = i * rHeight + _swarm.MinPosition[0];
                    double x = j * rWidth + _swarm.MinPosition[1];
                    row.Add(_engine.BananaFunction(new[] { x, y }));
                }
                vals.Add(row);
            }
            var min = vals.Min(row => row.Min());
            var max = vals.Max(row => row.Max());
            var delta = max - min;
            var r = 255/delta;

            for (int i = 0; i < bHeight; i++)
            {
                for (int j = 0; j < bWidth; j++)
                {
                    var c = (int)Math.Min(Math.Max(vals[i][j]*r + min, 0), 255);
                    var cor = Color.FromArgb(c, c, c);
                    bitmap.SetPixel(j, i, cor);
                }
            }
            return bitmap;
        }

        public void IterationStepEvent(object sender, IterationStepHandlerArgs args)
        {
            Invoke((MethodInvoker) (() => PaintParticleSwarm(args.Swarm)));
        }

        public void PaintParticleSwarm(ParticleSwarm swarm)
        {
            throw new NotImplementedException();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            pictureBox1.Image = _bitmap;
        }
    }
}
