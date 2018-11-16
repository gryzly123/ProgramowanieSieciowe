namespace HttpCrawler
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonBeginCrawling = new System.Windows.Forms.Button();
            this.ListboxLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextboxPath = new System.Windows.Forms.TextBox();
            this.TextboxDepth = new System.Windows.Forms.TextBox();
            this.TextboxOutXml = new System.Windows.Forms.TextBox();
            this.ButtonChooseXmlPath = new System.Windows.Forms.Button();
            this.DialogSaveXml = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ProgressbarCrawler = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // ButtonBeginCrawling
            // 
            this.ButtonBeginCrawling.Location = new System.Drawing.Point(282, 90);
            this.ButtonBeginCrawling.Name = "ButtonBeginCrawling";
            this.ButtonBeginCrawling.Size = new System.Drawing.Size(84, 33);
            this.ButtonBeginCrawling.TabIndex = 0;
            this.ButtonBeginCrawling.Text = "Begin";
            this.ButtonBeginCrawling.UseVisualStyleBackColor = true;
            this.ButtonBeginCrawling.Click += new System.EventHandler(this.ButtonBeginCrawling_Click);
            // 
            // ListboxLog
            // 
            this.ListboxLog.FormattingEnabled = true;
            this.ListboxLog.Location = new System.Drawing.Point(12, 147);
            this.ListboxLog.Name = "ListboxLog";
            this.ListboxLog.Size = new System.Drawing.Size(354, 290);
            this.ListboxLog.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Results:";
            // 
            // TextboxPath
            // 
            this.TextboxPath.Location = new System.Drawing.Point(123, 12);
            this.TextboxPath.Name = "TextboxPath";
            this.TextboxPath.Size = new System.Drawing.Size(243, 20);
            this.TextboxPath.TabIndex = 6;
            this.TextboxPath.Text = "http://detox.wi.ps.pl/";
            this.TextboxPath.TextChanged += new System.EventHandler(this.RequestUpdateConfig);
            // 
            // TextboxDepth
            // 
            this.TextboxDepth.Location = new System.Drawing.Point(123, 38);
            this.TextboxDepth.Name = "TextboxDepth";
            this.TextboxDepth.Size = new System.Drawing.Size(56, 20);
            this.TextboxDepth.TabIndex = 7;
            this.TextboxDepth.Text = "3";
            this.TextboxDepth.TextChanged += new System.EventHandler(this.RequestUpdateConfig);
            // 
            // TextboxOutXml
            // 
            this.TextboxOutXml.Location = new System.Drawing.Point(123, 64);
            this.TextboxOutXml.Name = "TextboxOutXml";
            this.TextboxOutXml.Size = new System.Drawing.Size(209, 20);
            this.TextboxOutXml.TabIndex = 8;
            this.TextboxOutXml.Text = "CrawlerResults.xml";
            this.TextboxOutXml.TextChanged += new System.EventHandler(this.RequestUpdateConfig);
            // 
            // ButtonChooseXmlPath
            // 
            this.ButtonChooseXmlPath.Location = new System.Drawing.Point(338, 63);
            this.ButtonChooseXmlPath.Name = "ButtonChooseXmlPath";
            this.ButtonChooseXmlPath.Size = new System.Drawing.Size(28, 21);
            this.ButtonChooseXmlPath.TabIndex = 9;
            this.ButtonChooseXmlPath.Text = "...";
            this.ButtonChooseXmlPath.UseVisualStyleBackColor = true;
            this.ButtonChooseXmlPath.Click += new System.EventHandler(this.ButtonChooseXmlPath_Click);
            // 
            // DialogSaveXml
            // 
            this.DialogSaveXml.DefaultExt = "xml";
            this.DialogSaveXml.Filter = "XML file|*.xml";
            this.DialogSaveXml.FileOk += new System.ComponentModel.CancelEventHandler(this.DialogSaveXml_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Initial remote path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Crawler depth limit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Output stats file:";
            // 
            // ProgressbarCrawler
            // 
            this.ProgressbarCrawler.Location = new System.Drawing.Point(12, 439);
            this.ProgressbarCrawler.MarqueeAnimationSpeed = 0;
            this.ProgressbarCrawler.Name = "ProgressbarCrawler";
            this.ProgressbarCrawler.Size = new System.Drawing.Size(354, 10);
            this.ProgressbarCrawler.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressbarCrawler.TabIndex = 13;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 452);
            this.Controls.Add(this.ProgressbarCrawler);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonChooseXmlPath);
            this.Controls.Add(this.TextboxOutXml);
            this.Controls.Add(this.TextboxDepth);
            this.Controls.Add(this.TextboxPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListboxLog);
            this.Controls.Add(this.ButtonBeginCrawling);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "HttpCrawler (Krzysztof Niedźwiecki)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonBeginCrawling;
        private System.Windows.Forms.ListBox ListboxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextboxPath;
        private System.Windows.Forms.TextBox TextboxDepth;
        private System.Windows.Forms.TextBox TextboxOutXml;
        private System.Windows.Forms.Button ButtonChooseXmlPath;
        private System.Windows.Forms.SaveFileDialog DialogSaveXml;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar ProgressbarCrawler;
    }
}

