using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WebAppView
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitBrowser();
        }

        public ChromiumWebBrowser browser;

        public void InitBrowser()
        {
            Cef.EnableHighDPISupport();
            Cef.Initialize(new CefSettings());
            // Web control URL display
            browser = new ChromiumWebBrowser("https://docs.google.com/spreadsheets/d/e/2PACX-1vRQSVrWJ7iPjRwQdXjL-UQvUpZmFJ4MuMd4-QaQd6PFC4pdnugQBnbEF_DM-Fplc6y2tJ6rRwPTL-5r/pubhtml?gid=6&single=true");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            // Browser loaded event
            browser.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {
            if (args.Frame.IsMain)
            {
                args
                    .Browser
                    .MainFrame
                    // Execute JavaScript here
                    .ExecuteJavaScriptAsync(
                    "document.body.style.overflow = 'hidden'; " +
                    "document.getElementById('footer').style.display = 'none'; " +
                    "document.getElementById('doc-title').style.display = 'none'; " +
                    "document.getElementById('top-bar').style.display = 'none'");
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close application
            Cef.Shutdown();
            Environment.Exit(0);
        }
    }
}
