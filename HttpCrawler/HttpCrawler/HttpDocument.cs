﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace HttpCrawler
{
    public delegate void CrawlerFinished(CrawlerSession Session);

    public struct CrawlerConfig
    {
        public string RemotePath;
        public int CrawlerMaxDepth;
        public string OutputPath;
        public bool DataValid;
    };

    public class CrawlerSession
    {
        List<string> TotalScannedUrls = new List<string>();
        Queue<HttpDocument> ScanTasks = new Queue<HttpDocument>();

        HttpDocument RootDocument;
        internal CrawlerConfig Config;

        public CrawlerFinished OnCrawlerFinished;

        public void BeginSession(CrawlerConfig InCfg)
        {
            Config = InCfg;
            RootDocument = new HttpDocument(Config.RemotePath, 0);
            ScanTasks.Enqueue(RootDocument);
            BeginNextDocument();
        }

        private void BeginNextDocument()
        {
            //szukam kolejnego unikatowego dokumentu
            HttpDocument CurrentDoc;
            do
            {
                if(ScanTasks.Count > 0) CurrentDoc = ScanTasks.Dequeue();
                else
                {
                    OnCrawlerFinished(this);
                    return;
                }
            }
            while (TotalScannedUrls.Contains(CurrentDoc.Href));
            TotalScannedUrls.Add(CurrentDoc.Href);
            OnCrawlerFinished(this);
            //rozpoczynam jego analizę
            CurrentDoc.OnDocumentParsed += OnDocumentFinished;
            CurrentDoc.DownloadDocument(this);
//            new Thread(new ThreadStart(() => { CurrentDoc.DownloadDocument(this); })).Start();
        }

        internal HttpDocument GetRootDocument()
        {
            return RootDocument;
        }

        private void OnDocumentFinished(HttpDocument Doc)
        {
            //jeśli skanowanie dokumentu się nie powiodło, nie dodaję jego dzieci do przeszukania
            if (Doc.CrawlerStatus != HttpDocument.Status.ScanFinished) return;

            //jeśli maksymalna głębokość została osiągnięta, kończę
            if (Doc.Depth == Config.CrawlerMaxDepth - 1) return;

            //dodaję poddokumenty do kolejki
            foreach (HttpDocument NextDoc in Doc.Subdocuments)
                ScanTasks.Enqueue(NextDoc);

            //rozpoczynam kolejny dokument
            BeginNextDocument();
        }

    }
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

        public string Href;
        public string Errmsg;
        public Status CrawlerStatus = Status.ScanPending;
        public int Depth;
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
            if (Relative.StartsWith("http")) return Relative;

            int kek = Href.LastIndexOf('/');
            string Base = Href.Substring(0, kek);
            return Base + "/" + Relative;
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
                BuildHostname(out string Hostname, out bool UseSsl);
                string RelativePath = Href.Split(new string[] { Hostname }, StringSplitOptions.None)[1];

                HttpConnection TmpConnection = new HttpConnection();
                TmpConnection.StartConnection(Hostname, UseSsl);
                TmpConnection.ExecuteRequest(RelativePath, out List<byte> Data);
                TmpConnection.CloseConnection();

                CrawlerStatus = ParseDocument(Data) ? Status.ScanFinished : Status.ScanFailed;
            }
            catch { CrawlerStatus = Status.ScanFailed; }
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
            Response = Response.Substring(Response.IndexOf("\r\n\r\n") + 4);

            Regex ImgMatcher = new Regex("<img(.+?)src=\"(?<image_link>(.+?))\"(.+?)>");
            foreach (Match M in ImgMatcher.Matches(Response))
                ImageAddresses.Add(AbsoluteLink(M.Groups["image_link"].Value));

            Regex HtmMatcher = new Regex("<a(.+?)href=\"(?<url>(.+?))\"(.+?)>");
            foreach (Match M in HtmMatcher.Matches(Response))
            {
                string Url = M.Groups["url"].Value;
                if(Url.EndsWith(".html") || Url.EndsWith(".htm")) Subdocuments.Add(new HttpDocument(AbsoluteLink(Url), Depth + 1));
            }

            return true;
        }
    }
}
