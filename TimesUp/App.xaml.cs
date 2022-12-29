using Microsoft.UI.Xaml;
using TimesUp.Database;

namespace TimesUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DataAccess.InitializeDatabase();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
