using System;
using System.Xml;

namespace MailClient
{
    public class SmtpConnectionSettings : NetConnectionSettings
    {
        public override UInt16 GetDefaultPort() { return 25; }
        public override void CloneChildData(NetConnectionSettings InSettings) { }
        public override void ReadAdditionalVerbs(string XmlName, string XmlValue) { }
        public override void WriteAdditionalVerbs(XmlTextWriter Writer) { }
    }
}
