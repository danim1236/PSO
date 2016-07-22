﻿using System;
using System.Windows.Forms;
using ParticleSwarmDemo.FunctionMinimizing;

namespace ParticleSwarm.FunctionMinimizing
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
