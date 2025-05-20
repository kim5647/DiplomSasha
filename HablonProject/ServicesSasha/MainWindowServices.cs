using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha.BaseServise;
using SQLServer.DatabaseContext;
using SQLServer.Repository.RepositorySasha;
using System.Windows.Controls;

namespace HablonProject.ServicesSasha
{
    public class MainWindowServices : BaseServices
    {
        public static Frame MainContentFrame { get; private set; }
        public MainWindowServices(Frame _mainContentFrame)
        {
            MainContentFrame = _mainContentFrame;
        }
    }
}
