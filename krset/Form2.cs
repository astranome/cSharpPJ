using System;

using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace krset
{
    
    public partial class Form2 : Form
    {
        string email, pass, protocol;
        public Form2(string _email, string _pass, string _protocol)
        {
            InitializeComponent();
            email = _email;
            pass = _pass;
            protocol = _protocol;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(string[]), new Type[1] { typeof(string) });
            StreamReader sr = new StreamReader("mail.xml");
            string[] str = (string[])(xs.Deserialize(sr));
            foreach (string i in str)
            {
                checkedListBox1.Items.Add(i);
            }
            sr.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MailMessage msg = new MailMessage();

            System.Net.NetworkCredential smtpCreds = new System.Net.NetworkCredential(email, pass);




            //MailAddress To = new MailAddress(textBox1.Text);

            MailAddress From = new MailAddress(email);

            msg.Subject = richTextBox2.Text;
            msg.Body = richTextBox1.Text;
            msg.From = From;



            foreach (string str in checkedListBox1.CheckedItems)
            {
                MailAddress To = new MailAddress(str);
                msg.To.Add(To);
            }

            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = protocol;
                client.Port = 25;
                client.UseDefaultCredentials = false;
                client.Credentials = smtpCreds;
                client.EnableSsl = true;

                client.SendCompleted += new SendCompletedEventHandler(MailSendCallback);

                client.SendAsync(msg, null);
            }
            catch
            {
                MessageBox.Show("неверные данные");
            }
            richTextBox2.Text = "";
            richTextBox1.Text = "";
        }

        private static void MailSendCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;
            if (e.Cancelled)
                MessageBox.Show("письмо отменено");
            if (e.Error != null)
                MessageBox.Show("Письмо не отправлено. Ошибка");
            else
                MessageBox.Show("Письма отправлены");
            //mailSendSemaphore.Release();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isdog = false;
            bool isdot = false;
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '@')
                    if (isdot) break;
                    else isdog = true;
                if (textBox2.Text[i] == '.') isdot = true;
            }
            if (isdot && isdog)
                checkedListBox1.Items.Add(textBox2.Text);
            else MessageBox.Show("неккоректный email");

            textBox2.Text = "";



        }

        private void button3_Click(object sender, EventArgs e)
        {

            string[] str = new string[checkedListBox1.Items.Count];
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                str[i] = checkedListBox1.Items[i].ToString();
            }
            XmlSerializer xs = new XmlSerializer(typeof(string[]), new Type[1] { typeof(string) });
            StreamWriter sw = new StreamWriter("mail.xml");
            xs.Serialize(sw, str);
            sw.Flush();
            sw.Close();
            MessageBox.Show("Сохранено");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            if (dg.ShowDialog() == DialogResult.OK)
            {
                checkedListBox1.Items.Clear();
                XmlSerializer xs = new XmlSerializer(typeof(string[]), new Type[1] { typeof(string) });
                StreamReader sr = new StreamReader(dg.FileName);
                string[] str = (string[])(xs.Deserialize(sr));
                foreach (string i in str)
                {
                    checkedListBox1.Items.Add(i);
                }
                sr.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            if (dg.ShowDialog() == DialogResult.OK)
            {
                string[] str = new string[checkedListBox1.Items.Count];
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    str[i] = checkedListBox1.Items[i].ToString();
                }
                XmlSerializer xs = new XmlSerializer(typeof(string[]), new Type[1] { typeof(string) });
                StreamWriter sw = new StreamWriter(dg.FileName);
                xs.Serialize(sw, str);
                sw.Flush();
                sw.Close();
                MessageBox.Show("Сохранено");

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int del = this.checkedListBox1.SelectedIndex;
            if (del < 0)
            {
                MessageBox.Show("Не выделен ни один адрес!");
                return;
            }
            checkedListBox1.Items.RemoveAt(del);
        }


       
    }

}



