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
            Hostname = "127.0.0.1";
            Port = 110;
            UserLogin = "";
            UserPassword = "";
            RefreshRateSeconds = 15.0;
        }

        public void CloneFrom(PopConnectionSettings In)
        {
            Hostname           = In.Hostname;
            Port               = In.Port;
            UserLogin          = In.UserLogin;
            UserPassword       = In.UserPassword;
            RefreshRateSeconds = In.RefreshRateSeconds;
        }

        //parsowanie ustawień z pliku (App.config)
        public bool TryReadFromFile(String ConfigPath)
        {
            XmlTextReader Reader = null;

            try
            {
                Reader = new XmlTextReader(ConfigPath);
                bool hasHostname = false, hasUsername = false, hasPassword = false, hasPort = false, hasRefrate = false;
                while (Reader.Read())
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
                            RefreshRateSeconds = Refrate; //jeśli parse nie powiedzie się, Refrate = 15
                            hasRefrate = true;
                            break;
                    }

                Reader.Close();
                return hasHostname && hasUsername && hasPassword && hasPort && hasRefrate;
            }
            catch { if(Reader != null) Reader.Close(); }
            return false;
        }

        //tworzenie pliku App.config
        public bool SaveConfig(string TargetCfg)
        {
            try
            {
              XmlTextWriter Writer = new XmlTextWriter(TargetCfg, System.Text.Encoding.ASCII);
              Writer.WriteStartDocument();
              Writer.WriteStartElement("appcfg");
                Writer.WriteStartElement("hostname");
                  Writer.WriteAttributeString("value", Hostname);
                Writer.WriteEndElement();
                Writer.WriteStartElement("port");
                  Writer.WriteAttributeString("value", Port.ToString());
                Writer.WriteEndElement();
                Writer.WriteStartElement("username");
                  Writer.WriteAttributeString("value", UserLogin);
                Writer.WriteEndElement();
                Writer.WriteStartElement("password");
                  Writer.WriteAttributeString("value", UserPassword);
                Writer.WriteEndElement();
                Writer.WriteStartElement("refrate");
                  Writer.WriteAttributeString("value", RefreshRateSeconds.ToString());
                Writer.WriteEndElement();
              Writer.WriteEndElement();
              Writer.WriteEndDocument();
              Writer.Close();
            }
            catch { return false; }
            return true;
        }
    }
}
