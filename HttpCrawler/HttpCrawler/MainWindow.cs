using System;
using System.Windows.Forms;

namespace HttpCrawler
{
    public partial class MainWindow : Form
    {
        CrawlerConfig Cfg = new CrawlerConfig();
        CrawlerSession Session;

        public MainWindow()
        {
            InitializeComponent();
            SetupConfig();
        }
        private void SetupConfig()
        {
            Cfg.RemotePath = TextboxPath.Text;
            Cfg.OutputPath = TextboxOutXml.Text;
            Cfg.DataValid = int.TryParse(TextboxDepth.Text, out Cfg.CrawlerMaxDepth) && (Cfg.CrawlerMaxDepth > 0);
            ButtonBeginCrawling.Enabled = Cfg.DataValid && Session == null;
        }

        private void RequestUpdateConfig(object sender, EventArgs e)
        {
            SetupConfig();
        }

        private void ButtonChooseXmlPath_Click(object sender, EventArgs e)
        {
            DialogSaveXml.ShowDialog();
        }

        private void DialogSaveXml_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextboxOutXml.Text = DialogSaveXml.FileName;
        }

        private void ButtonBeginCrawling_Click(object sender, EventArgs e)
        {
            ButtonBeginCrawling.Enabled = false;
            ListboxLog.Items.Clear();

            Session = new CrawlerSession();
            Session.OnCrawlerProgress += HandleOnCrawlerProgress;
            Session.OnCrawlerFinished += HandleOnCrawlerFinished;
            Session.BeginSession(Cfg);
        }

        private void HandleOnCrawlerProgress(CrawlerSession Session, int DocumentsParsed, int DocumentsLeft)
        {
            Invoke(new Action(() =>
            {
                ProgressbarCrawler.Maximum = DocumentsParsed + DocumentsLeft;
                ProgressbarCrawler.Value = Math.Min(ProgressbarCrawler.Maximum, DocumentsParsed + 1);
                ProgressbarCrawler.Value = Math.Max(DocumentsParsed, 0);
                ListboxLog.Items.Add(
                    String.Format("{0}/{1} documents parsed",
                    DocumentsParsed.ToString(),
                    (DocumentsParsed + DocumentsLeft).ToString()));
            }));
        }

        private void HandleOnCrawlerFinished(CrawlerSession Session)
        {
            HttpDocument Doc = Session.GetRootDocument();
            Invoke(new Action(() =>
            {
                ListboxLog.Items.Clear();
                PrintDoc(Doc);
                Session = null;
                ButtonBeginCrawling.Enabled = true;
            }));
        }

        private void PrintDoc(HttpDocument Doc)
        {
            string Separator = "";
            for (int i = 0; i < Doc.Depth; ++i) Separator += "    ";

            ListboxLog.Items.Add(Separator + " " + Doc.Href);
            foreach (string Img in Doc.ImageAddresses)
                ListboxLog.Items.Add(Separator + "  <img> " + Img);
            foreach (string Mail in Doc.MailAddresses)
                ListboxLog.Items.Add(Separator + "<mail> " + Mail);
            foreach (HttpDocument Subdoc in Doc.Subdocuments)
                PrintDoc(Subdoc);
        }
    }
}
