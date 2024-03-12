using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Assi3_PRN221
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int countdownTime;
        private Thread countdownThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartCountdown_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCountdownTime.Text, out int time))
            {
                countdownTime = time;
                countdownThread = new Thread(new ThreadStart(Countdown));
                countdownThread.Start();
            }
            else
            {
                MessageBox.Show("Please enter a valid integer for countdown time.");
            }
        }

        private void Countdown()
        {
            for (int i = countdownTime; i >= 0; i--)
            {
                UpdateCountdownDisplay(i);
                Thread.Sleep(1000); // Sleep for 1 second
            }
        }

        private void UpdateCountdownDisplay(int secondsRemaining)
        {
            if (txtCountdownDisplay.Dispatcher.CheckAccess())
            {
                txtCountdownDisplay.Text = TimeSpan.FromSeconds(secondsRemaining).ToString(@"hh\:mm\:ss");
            }
            else
            {
                txtCountdownDisplay.Dispatcher.Invoke(() => UpdateCountdownDisplay(secondsRemaining));
            }
        }
    }
}