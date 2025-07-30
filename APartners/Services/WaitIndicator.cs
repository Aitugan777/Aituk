using APartners.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace APartners.Services
{
    public class WaitIndicator : IDisposable
    {
        private readonly Window _indicatorWindow;

        public WaitIndicator(string message)
        {
            _indicatorWindow = new WaitIndicatorWindow(message)
            {
                Owner = Application.Current?.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            _indicatorWindow.Show();
            DoEvents(); // Чтобы окно показалось до выполнения длительной операции
        }

        public void Dispose()
        {
            _indicatorWindow.Close();
        }

        // Позволяет "продавить" показ окна перед выполнением долгого действия
        private static void DoEvents()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(_ => {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }
    }

}
