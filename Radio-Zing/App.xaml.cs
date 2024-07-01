using Model;
using StubLib;

namespace Radio_Zing
{
    public partial class App : Application
    {
        public Manager MyManager { get; } = new(new Stub());
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
