using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneClickHandler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Prequisite.Prequisite pre = new Prequisite.Prequisite();
            Task checkPre = new Task(pre.PreWorker);
            checkPre.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!clonepath.Text.Contains(".git"))
            {
                MessageBox.Show("Not a valid Git Repo!");
                return;
            }



            try
            {


                string gitAddArgument = @"clone " + clonepath.Text;
                Process pro = new Process();
                pro.StartInfo.FileName = "git";
                pro.StartInfo.UseShellExecute = true;
                pro.StartInfo.CreateNoWindow = false;
                pro.StartInfo.Arguments = gitAddArgument;

                pro.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error");
                Console.WriteLine(ex.Message);
                return;
            }
           
          
        }

        private void clonepath_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cmakeCommand = "cmake";
                string cmakeArgument = @" .\TrinityCore\ -DCMAKE_INSTALL_PREFIX=" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\\cmake ";

                Process.Start(cmakeCommand, cmakeArgument);
            }

            catch
            {
                MessageBox.Show("Error executing cMake!");
            }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
