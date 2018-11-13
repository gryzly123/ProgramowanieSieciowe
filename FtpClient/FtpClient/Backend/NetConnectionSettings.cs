using System;
using System.Xml;

namespace FtpClient
{
    public abstract class NetConnectionSettings
    {
        public string Hostname;
        public UInt16 Port;
        public string UserLogin;
        public string UserPassword;
        public bool UseSsl;

        //init ustawień z domyślnymi ustawieniami
        public NetConnectionSettings()
        {
            Hostname = "127.0.0.1";
            Port = GetDefaultPort();
            UserLogin = "";
            UserPassword = "";
            UseSsl = false;
        }

        public abstract UInt16 GetDefaultPort();
        public abstract void CloneChildData(NetConnectionSettings InSettings);
        public abstract void ReadAdditionalVerbs(string XmlName, string XmlValue);
        public abstract void WriteAdditionalVerbs(XmlTextWriter Writer);

        public void CloneFrom(NetConnectionSettings In)
        {
            Hostname           = In.Hostname;
            Port               = In.Port;
            UserLogin          = In.UserLogin;
            UserPassword       = In.UserPassword;
            UseSsl             = In.UseSsl;
            CloneChildData(In);
        }

        //parsowanie ustawień z pliku (App.config)
        public bool TryReadFromFile(String ConfigPath)
        {
            XmlTextReader Reader = null;

            try
            {
                Reader = new XmlTextReader(ConfigPath);
                bool hasHostname = false, hasUsername = false, hasPassword = false, hasPort = false, hasSsl = false;
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
                            UInt16.TryParse(Reader.GetAttribute(0), out Port);
                            hasPort = true;
                            break;

                        case "ssl":
                            bool Ssl = false, Parse = false;
                            Parse = bool.TryParse(Reader.GetAttribute(0), out Ssl);
                            UseSsl = Ssl || !Parse; //jeśli parse nie powiedzie się, ustawiam true
                            hasSsl = true;
                            break;

                        default:
                            if(Reader.AttributeCount > 0)
                                ReadAdditionalVerbs(Reader.Name, Reader.GetAttribute(0));
                            break;
                    }

                Reader.Close();
                return hasHostname && hasUsername && hasPassword && hasPort && hasSsl;
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
                  Writer.WriteStartElement("ssl");
                    Writer.WriteAttributeString("value", UseSsl.ToString());
                  Writer.WriteEndElement();
                  WriteAdditionalVerbs(Writer);
                Writer.WriteEndElement();
              Writer.WriteEndDocument();
              Writer.Close();
            }
            catch { return false; }
            return true;
        }
    }
}
