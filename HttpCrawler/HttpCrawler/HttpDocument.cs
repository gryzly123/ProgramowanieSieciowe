using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace HttpCrawler
{
    public delegate void DocumentParsed(HttpDocument Doc);

    public class HttpDocument
    {
        public enum Status
        {
            ScanPending,
            ScanInProgress,
            ScanFinished,
            ScanFailed,
            DontScan
        };

        //wejścia:
        public string Href;
        public int Depth;

        //status:
        public string Errmsg;
        public Status CrawlerStatus = Status.ScanPending;

        //wyjścia:
        public List<HttpDocument> Subdocuments = new List<HttpDocument>();
        public List<string> MailAddresses = new List<string>();
        public List<string> ImageAddresses = new List<string>();

        public DocumentParsed OnDocumentParsed;

        private void BuildHostname(out string Hostname, out bool UseSsl)
        {
            UseSsl = false;
            if (Href.Equals(string.Empty)) throw new Exception("invalid_href");
            bool HasProtocol = Href.StartsWith("http") || Href.StartsWith("https");
            if (HasProtocol) UseSsl = Href.StartsWith("https");

            string[] Split = Href.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            Hostname = (HasProtocol || Href.StartsWith("/")) ? Split[1] : Split[0];
        }

        private string AbsoluteLink(string Relative)
        {
            if (Relative.StartsWith("http")) return Relative; //wlicza także https

            return string.Format("{0}/{1}",
                Href.Substring(0, Href.LastIndexOf('/')),
                Relative);
        }

        public HttpDocument(string TargetHref, int CurrentDepth)
        {
            Href = TargetHref;
            Depth = CurrentDepth;
        }

        public void DownloadDocument(CrawlerSession Session)
        {
            if (CrawlerStatus != Status.ScanPending) return;

            try
            {
                CrawlerStatus = Status.ScanInProgress;

                string Hostname; bool UseSsl;
                BuildHostname(out Hostname, out UseSsl);
                string RelativePath = Href.Split(new string[] { Hostname }, StringSplitOptions.None)[1];

                HttpConnection TmpConnection = new HttpConnection();
                TmpConnection.StartConnection(Hostname, UseSsl);

                List<byte> Data;
                TmpConnection.ExecuteRequest(RelativePath, out Data);
                TmpConnection.CloseConnection();

                CrawlerStatus = ParseDocument(Data) ? Status.ScanFinished : Status.ScanFailed;
            }
            catch { Errmsg = "tcp error"; CrawlerStatus = Status.ScanFailed; }
            OnDocumentParsed(this);
        }

        public bool ParseDocument(List<byte> Data)
        {
            string Response = Encoding.ASCII.GetString(Data.ToArray());
            if (!Response.StartsWith("HTTP/1.1 200 OK"))
            {
                Errmsg = Response.Substring(0, Response.IndexOf("\r\n"));
                return false;
            }

            //Oddzielam odpowiedź od nagłówka
            Response = Response.Substring(Response.IndexOf("\r\n\r\n") + 4);

            //Znajduję obrazki po <img [...] src="link" [...]>
            Regex ImgMatcher = new Regex("<img(.+?)src=\"(?<image_link>(.+?))\"(.+?)>");
            foreach (Match M in ImgMatcher.Matches(Response))
                ImageAddresses.Add(M.Groups["image_link"].Value);


            //Znajduję maile po [...]@[...]
            //  regex uwzględnia maile w <a>, ponieważ nie
            //  interesują go znaki poprzedzające i następujące wokół adresu
            Regex MailMatcher = new Regex("(?<left>([a-zA-Z0-9._-]+))@(?<right>([a-zA-Z0-9._-]+))");
            MatchCollection Col = MailMatcher.Matches(Response);
            foreach (Match M in Col)
                MailAddresses.Add((M.Groups["left"] + "@" + M.Groups["right"]).Trim());


            //Znajduję linki po <a [...] href="[...]" [...]>
            Regex HtmMatcher = new Regex("<a(.+?)href=\"(?<url>(.+?))\"(.+?)>");
            foreach (Match M in HtmMatcher.Matches(Response))
            {
                //...ale interesują mnie tylko pliki HTML i HTM zgodnie z poleceniem
                //(przy okazji wycina to duplikaty z adresów email w <a>)
                string Url = M.Groups["url"].Value;
                if(Url.EndsWith(".html") || Url.EndsWith(".htm"))
                    Subdocuments.Add(new HttpDocument(AbsoluteLink(Url), Depth + 1));
            }

            return true;
        }

        internal void PrintToXmlRecursive(XmlWriter Xml)
        {
            foreach (string Img in ImageAddresses)
            {
                Xml.WriteStartElement("IMAGE");
                  Xml.WriteString(Img);
                Xml.WriteEndElement();
            }
            foreach (string Mail in MailAddresses)
            {
                Xml.WriteStartElement("EMAIL");
                  Xml.WriteString(Mail);
                Xml.WriteEndElement();
            }
            foreach (HttpDocument Subdoc in Subdocuments)
            {
                Xml.WriteStartElement("FILE");
                  Xml.WriteAttributeString("href", Subdoc.Href);
                  Xml.WriteAttributeString("state", Subdoc.StatusString());
                  Subdoc.PrintToXmlRecursive(Xml);
                Xml.WriteEndElement();
            }
        }

        private string StatusString()
        {
            switch (CrawlerStatus)
            {
                case Status.ScanFinished: return "parsed";
                case Status.ScanFailed:   return string.Format("failed[{0}]", Errmsg);
                case Status.DontScan:     return "duplicate";
                case Status.ScanPending:  return "max depth";

                case Status.ScanInProgress:
                default:
                    throw new Exception("undefined behaviour");
            }
        }
    }
}
