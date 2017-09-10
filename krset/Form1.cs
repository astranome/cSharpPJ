
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

      
        private void button1_Click(object sender, EventArgs e)
        {

            Form2 fr = new Form2(textBox1.Text, textBox2.Text, comboBox1.Text);
            fr.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
    }
   
}
