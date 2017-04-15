using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VamkEmail
{
    public partial class Form1 : Form
    {
        private string username, password, recipient,subject, message, footer;
        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);


        private void button1_Click(object sender, EventArgs e)
        {
            GetInfo();
            Connection();
            try
            {
                smtp.Send(mail);
                MessageBox.Show("Email sent!", "Success");
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public Form1()
        {
            InitializeComponent();

            
        }

        private void GetInfo()
        {
            username = txtSNumber.Text + "@edu.vamk.fi";
            password = txtSPassword.Text;
            recipient = txtReMail.Text;
            message = txtMessage.Text;
            subject = txtSubject.Text;
            footer = "<address><ul><li>Student at Vaasa University of Applied Sciences</li><li>"+username+"</li><ul></address>";
        }

        private void Connection()
        {
            
            mail.From = new MailAddress(username);
            mail.To.Add(recipient);
            mail.Subject = subject;
            mail.Body = message + footer;
            mail.IsBodyHtml = true;

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(username, password);
            

           
        }

        private void sendEmail()
        {

        }
    }
}
