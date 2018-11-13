using System;
using System.Xml;

namespace FtpClient
{
    public class FtpConnectionSettings : NetConnectionSettings
    {
        public bool UnixListing = false;

        public override void CloneChildData(NetConnectionSettings InSettings)
        {
            UnixListing = ((FtpConnectionSettings)InSettings).UnixListing;
        }

        public override Int16 GetDefaultPort() { return 21; }
        public override void ReadAdditionalVerbs(string XmlName, string XmlValue) { if (XmlName.Equals("unixlisting") && XmlValue.Equals("True")) UnixListing = true; }
        public override void WriteAdditionalVerbs(XmlTextWriter Writer)
        {
            Writer.WriteStartElement("unixlisting");
            Writer.WriteAttributeString("value", UnixListing.ToString());
            Writer.WriteEndElement();
        }
    }
}
