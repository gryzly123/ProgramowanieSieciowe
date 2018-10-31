using System;
using System.Xml;

namespace MailClient
{
    public class PopConnectionSettings : NetConnectionSettings
    {
        public double ActualRefreshRateSeconds;
        public double RefreshRateSeconds
        {
            get { return ActualRefreshRateSeconds; }
            set { if (value >= 1.0) ActualRefreshRateSeconds = value; else ActualRefreshRateSeconds = 15.0; }
        }

        public PopConnectionSettings()
        {
            RefreshRateSeconds = 5.0;
        }

        public void PopCloneFrom(PopConnectionSettings In)
        {
            CloneFrom(In);
            RefreshRateSeconds = In.RefreshRateSeconds;
        }

                            
        public override Int16 GetDefaultPort()
        {
            return 110;
        }

        public override void ReadAdditionalVerbs(string XmlName, string XmlValue)
        {
            if (XmlName != "refrate") return;

            double Refrate = 0;
            double.TryParse(XmlValue, out Refrate);
            RefreshRateSeconds = Refrate; //jeśli parse nie powiedzie się, Refrate = 15
        }

        public override void WriteAdditionalVerbs(XmlTextWriter Writer)
        {
            Writer.WriteStartElement("refrate");
            Writer.WriteAttributeString("value", RefreshRateSeconds.ToString());
            Writer.WriteEndElement();
        }
    }
}
