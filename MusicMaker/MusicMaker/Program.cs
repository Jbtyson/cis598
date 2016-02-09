﻿using System;
using System.Windows.Forms;
using MusicGenerator;

namespace MusicMaker
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

            var musicController = new MusicController();
            musicController.Init();
            musicController.Play();

            Application.Run(new Form1());
        }
    }
}
