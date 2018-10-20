using System;

namespace MailClient
{
    public class PopConnectionSettings
    {
        public string Hostname
        {
            get;
            set;
        }

        private int ActualPort;
        public int Port
        {
            get { return ActualPort; }
            set { if (value < 65536) ActualPort = value; }
        }

        public string UserLogin
        {
            get;
            set;
        }

        public string UserPassword
        {
            get;
            set;
        }

        private double ActualRefreshRateSeconds;
        public double RefreshRateSeconds
        {
            get { return ActualRefreshRateSeconds; }
            set { if (value > 1.0) ActualRefreshRateSeconds = value; }
        }

        //init ustawień z domyślnymi ustawieniami
        public PopConnectionSettings()
        {
            Hostname = "";
            Port = 110;
            UserLogin = "";
            UserPassword = "";
            RefreshRateSeconds = 15.0;
        }

        //parsowanie ustawień z pliku (App.config)
        public PopConnectionSettings(String Cfg)
        {
            throw new NotImplementedException();
        }

        //tworzenie pliku App.config
        public String SaveConfig()
        {
            throw new NotImplementedException();
        }
    }
}
