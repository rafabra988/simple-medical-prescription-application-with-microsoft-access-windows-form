using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BDados
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
            Application.Run(new Form1());
        }
    }
}
public class Aluno
{
    private int idade;
    private String Nome;
    public Aluno()
    {

    }
}