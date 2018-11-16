using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace HttpCrawler
{
    public delegate void CrawlerFinished(CrawlerSession Session);
    public delegate void CrawlerProgress(CrawlerSession Session, int DocumentsParsed, int DocumentsLeft);

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
        int DocumentsParsed = 0;
        Random WaitPeriod = new Random();

        HttpDocument RootDocument;
        internal CrawlerConfig Config;

        public CrawlerFinished OnCrawlerFinished;
        public CrawlerProgress OnCrawlerProgress;

        public void BeginSession(CrawlerConfig InCfg)
        {
            Config = InCfg;
            RootDocument = new HttpDocument(Config.RemotePath, 0);
            ScanTasks.Enqueue(RootDocument);
            BeginNextDocument();
        }

        private void BeginNextDocument()
        {
            HttpDocument CurrentDoc;
            //szukam kolejnego unikatowego dokumentu
            do
            {
                //pobieram nowy dokument ze stosu
                if(ScanTasks.Count > 0) CurrentDoc = ScanTasks.Dequeue();
                //jeśli nie ma już co pobrać, kończę proces
                else
                {
                    OnCrawlerFinished(this);
                    return;
                }

                //jeśli podany URL był już zeskanowany gdzie indziej, flaguję go jako pominięty
                if (TotalScannedUrls.Contains(CurrentDoc.Href))
                    CurrentDoc.CrawlerStatus = HttpDocument.Status.DontScan;
            }
            while (CurrentDoc.CrawlerStatus != HttpDocument.Status.ScanPending);

            //znalazłem element, dodaję go do listy już zeskanowanych
            TotalScannedUrls.Add(CurrentDoc.Href);

            //"human element" - do i tak już zmiennego czasu parsowania
            //poprzedniego dokumentu dodaję jeszcze losowy delay pomiędzy zapytaniami
            Thread.Sleep(WaitPeriod.Next(0, 500));

            //rozpoczynam pobieranie i analizę
            CurrentDoc.OnDocumentParsed += OnDocumentFinished;
            new Thread(new ThreadStart(() => { CurrentDoc.DownloadDocument(this); })).Start();
        }

        internal HttpDocument GetRootDocument()
        {
            return RootDocument;
        }

        private void OnDocumentFinished(HttpDocument Doc)
        {
            ++DocumentsParsed;

            if (Doc.CrawlerStatus == HttpDocument.Status.ScanFinished //jeśli skanowanie dokumentu się powiodło...
                && Doc.Depth < Config.CrawlerMaxDepth - 1) //...i nie jest on na maksymalnej głębokości,
                foreach (HttpDocument NextDoc in Doc.Subdocuments)
                    ScanTasks.Enqueue(NextDoc); //to dodaję poddokumenty do kolejki

            //rozpoczynam kolejny dokument
            OnCrawlerProgress(this, DocumentsParsed, ScanTasks.Count);
            BeginNextDocument();
        }

        public bool PrintCrawlSessionToXml()
        {
            XmlWriter Xml;
            try { Xml = XmlWriter.Create(Config.OutputPath); }
            catch { return false; }

            try
            {
                Xml.WriteStartDocument();
                Xml.WriteStartElement("SITE");
                  Xml.WriteAttributeString("url", Config.RemotePath);
                  Xml.WriteAttributeString("depth", Config.CrawlerMaxDepth.ToString());
                  RootDocument.PrintToXmlRecursive(Xml);
                Xml.WriteEndElement();
                Xml.WriteEndDocument();
                Xml.Close();
                return true;
            }
            catch
            {
                Xml.Close();
                return false;
            }
        }
    }
}
