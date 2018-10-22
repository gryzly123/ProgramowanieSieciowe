using System;
using System.Xml;

namespace MailClient
{
    public class PopConnectionSettings
    {
        public string Hostname;
        public Int16 Port;
        public string UserLogin;
        public string UserPassword;

        private double ActualRefreshRateSeconds;
        public double RefreshRateSeconds
        {
            get { return ActualRefreshRateSeconds; }
            set { if (value > 1.0) ActualRefreshRateSeconds = value; else ActualRefreshRateSeconds = 15.0; }
        }

        //init ustawień z domyślnymi ustawieniami
        public PopConnectionSettings()
        {
            Hostname = "";
            Port = 110;
            UserLogin = "";
            UserPassword = "";
            RefreshRateSeconds = 15.0;
        }

        //parsowanie ustawień z pliku (App.config)
        public PopConnectionSettings(String ConfigPath)
        {
            XmlTextReader Reader = new XmlTextReader(ConfigPath);
            bool hasHostname = false, hasUsername = false, hasPassword = false, hasPort = false, hasRefrate = false;
            while (Reader.Read())
            {
                if(Reader.HasAttributes)
                    switch (Reader.Name)
                    {
                        case "hostname":
                            Hostname = Reader.GetAttribute(0);
                            hasHostname = true;
                            break;

                        case "username":
                            UserLogin = Reader.GetAttribute(0);
                            hasUsername = true;
                            break;

                        case "password":
                            UserPassword = Reader.GetAttribute(0);
                            hasPassword = true;
                            break;

                        case "port":
                            Int16.TryParse(Reader.GetAttribute(0), out Port);
                            hasPort = true;
                            break;

                        case "refrate":
                            double Refrate = 0;
                            double.TryParse(Reader.GetAttribute(0), out Refrate);
                            RefreshRateSeconds = Refrate; //jeśli parse nie powiedzie się, Refrate=15
                            hasRefrate = true;
                            break;
                    }
            }

            if (hasHostname && hasUsername && hasPassword && hasPort && hasRefrate) return;
            System.Windows.Forms.MessageBox.Show("Warning", "Not all fields could be serialized from config file");
        }

        //tworzenie pliku App.config
        public String SaveConfig(string TargetCfg)
        {
            XmlTextWriter Writer = new XmlTextWriter(TargetCfg, System.Text.Encoding.ASCII);
            throw new NotImplementedException();
        }
    }
}
