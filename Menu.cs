using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Form1 abc = new Form1();
            abc.Show();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
       

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
       

    }
}
