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
            ButtonBeginCrawling.Enabled = Cfg.DataValid;
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
            Session = new CrawlerSession();
            Session.OnCrawlerFinished += HandleOnCrawlerFinished;
            Session.BeginSession(Cfg);
        }

        private void HandleOnCrawlerFinished(CrawlerSession Session)
        {
            HttpDocument Doc = Session.GetRootDocument();
            Invoke(new Action(() =>
            {
                ListboxLog.Items.Clear();
                PrintDoc(Doc);
            }));
        }

        private void PrintDoc(HttpDocument Doc)
        {
            string Separator = "";
            for (int i = 0; i < Doc.Depth; ++i) Separator += "    ";

            ListboxLog.Items.Add(Separator + " " + Doc.Href);
            foreach (string Img in Doc.ImageAddresses)
                ListboxLog.Items.Add(Separator + "<img> " + Img);
            foreach (string Mail in Doc.MailAddresses)
                ListboxLog.Items.Add(Separator + "<img> " + Mail);
            foreach (HttpDocument Subdoc in Doc.Subdocuments)
                PrintDoc(Subdoc);
        }
    }
}
