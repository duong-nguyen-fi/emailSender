using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace MailSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            cbFrom.SelectedIndex = 1;
            body.Font = new Font(body.Font, FontStyle.Italic);
        }
        static string pass, server,signature,head,content;

        private void label7_Click(object sender, EventArgs e)
        {


        } // nothing

        private void btnSend_Click(object sender, EventArgs e)
        {
            content = "";           
            getPassWordandServer(); //generate server and password
            // head of message ex: "Dear Maria"
            head = "<h3>Dear " +Dear.Text + ",</h3>";
            signature = "<address><ul><li>Duong Nguyen </li><li>Student at Vaasa University of Applied Sciences</li><li>www.cc.puv.fi/~e1500948</li><ul></address>";
            #region try send mail
            try
            {
            MailMessage mail = new MailMessage();
            SmtpClient Client = new SmtpClient(server,587);
            mail.From = new MailAddress(cbFrom.Text);
            mail.To.Add(cbto.Text);
            mail.Subject = subject.Text;
            // Seperate body line in line
            foreach (string line in body.Lines)
            {
                if (line != "")
                {
                    content += "<h3>" + line.Trim() + "</h3>";
                }
            }
            mail.Body =head + content.TrimEnd() + signature;
            mail.IsBodyHtml = true;
            
            
            Client.EnableSsl = true;
            Client.Credentials = new System.Net.NetworkCredential(cbFrom.Text, pass);
            
                Client.Send(mail);
                MessageBox.Show("Email sent!","Success");
            }
            #endregion try

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //  get password and server for SMTP
        private void getPassWordandServer()
        {
            int id = (cbFrom.SelectedIndex);
            String connString = "Server=localhost;database=e1500948_email;uid=root;pwd=;";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command;
            command = conn.CreateCommand();
            switch (id)
            {
                case 0:
                    command.CommandText = "SELECT * FROM `emailSMTP` WHERE `id`='1'";
                    break;
                case 1:
                    command.CommandText = "SELECT * FROM `emailSMTP` WHERE `id`='2'";
                    break;
                case 3:
                    command.CommandText = "SELECT * FROM `emailSMTP` WHERE `id`='4'";
                
                    break;
            }                   
            try
            {
                conn.Open();
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pass = reader["password"].ToString();
                    server = reader["server"].ToString();
                }
            }
          //  catch (MySql.Data.MySqlClient.MySqlException ex )
          catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                conn.Close(); //close connection
                conn.Dispose(); // free resource
                conn = null;    //cancle object, empty object
                //MessageBox.Show("Close connection");
            }
            
        } // end of getPassword

        

    }
}
