using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PopConnectionSettings Config = new PopConnectionSettings();
            //Config.Hostname = "pop3.wp.pl";
            //Config.Port = 995;
            //Config.UserLogin = "ps45575@wp.pl";
            Config.Hostname = "127.0.0.1";
            Config.Port = 110;
            Config.UserLogin = "newuser";

            PopConnection Con = new PopConnection();
            string Response = "";
            Con.StartConnection(Config, ref Response);
            MessageBox.Show(Response);

        }
    }
}
