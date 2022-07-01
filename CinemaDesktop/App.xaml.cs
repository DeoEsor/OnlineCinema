using System.Windows;
using DryIoc;

namespace CinemaDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
			
            Container = new Container();
			
            // Grpc client
            //Views
            Container.Register<Registration>(Reuse.Transient);
            //ViewModels
			
            var reg = Container.Resolve<Registration>();
            reg.Show();
        }
    }
}