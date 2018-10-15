using System;

namespace MailClient
{
    class PopConnectionSettings
    {
        public string Hostname
        {
            get;
            set;
        }

        public int Port
        {
            get { return Port; }
            set { if (value < 65536) Port = value; }
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

        public double RefreshRateSeconds
        {
            get { return RefreshRateSeconds; }
            set { if (value > 1.0) RefreshRateSeconds = value; }
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
