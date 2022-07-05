using System.Windows;
using CinemaDesktop.MVVM.ViewModel;
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
            Container.Register<MainWindow>(Reuse.Singleton);
            Container.Register<MainViewModel>(Reuse.Singleton);
            Container.Register<UserViewModel>(Reuse.Singleton, Made.Of(()=>new UserViewModel()));

            //ViewModels
			
            var reg = Container.Resolve<MainWindow>();
            reg.DataContext = Container.Resolve<MainViewModel>();
            reg.Show();
        }
    }
}