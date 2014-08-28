using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using LCData;

namespace LocalCoder
{
    public class Engine
    {
        private Process Apache;
        private LCForm LC;

        public Engine(LCForm _LC)
        {
            LC = _LC;
            MySystem.SetAssociation(".lcproject", "LOCAL_CODER", Application.ExecutablePath, "Local Coder Project");
        }

        public void Start()
        {
            Settings.PrepareHTTPDConf();
            RunApache();
            LC.webResultBox.Navigate("http://localhost:10960/index.php");
            Update();
        }

        public void Close()
        {
            CloseApache();
        }

        public void Update()
        {
            LC.webResultBox.Refresh(WebBrowserRefreshOption.Completely);
        }

        private void WriteNewFile(string content)
        {
            File.WriteAllText(Settings.HtdocsFolder + "index.php", content);
        }

        private void RunApache()
        {
            ProcessStartInfo psi = new ProcessStartInfo(Settings.ApacheFileName, Settings.ApacheProcessArguments)
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Apache = Process.Start(psi);
        }

        private void CloseApache()
        {
            try
            {
                Apache.Kill();
            }
            catch { }
        }
    }
}

public class Settings
{
    public static string EngineWorkPath = Application.StartupPath;
    public static string ConfFileName = EngineWorkPath + @"\web\conf\conf.conf";
    public static string HttpdConfFileName = EngineWorkPath + @"\web\conf\httpd.conf";
    public static string ApacheFileName = EngineWorkPath + @"\web\bin\httpd.exe";
    public static string ApacheProcessArguments = "";
    public static string HtdocsFolder = EngineWorkPath + @"\web\htdocs\";

    public static void PrepareHTTPDConf()
    {
        string conf = File.ReadAllText(ConfFileName);
        conf = conf.Replace("{ENGINE_ROOT}", EngineWorkPath);
        File.WriteAllText(HttpdConfFileName, conf);
    }
}