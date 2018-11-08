using System;
using System.Xml;

namespace FtpClient
{
    public class FtpConnectionSettings : NetConnectionSettings
    {
        public override Int16 GetDefaultPort() { return 21; }
        public override void ReadAdditionalVerbs(string XmlName, string XmlValue) { }
        public override void WriteAdditionalVerbs(XmlTextWriter Writer) { }
    }
}
