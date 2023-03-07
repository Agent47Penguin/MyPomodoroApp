using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyPomodoroApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Create the Timer
        private DispatcherTimer timer = new DispatcherTimer();
        private int minutes = 25;
        private int seconds = 00;
        private string time = string.Format("{0:00}:{1:00}", 25, 00);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set The Label
            TimerLabel.Content = time;

            // Initialize Timer Interval
            timer.Interval = TimeSpan.FromSeconds(1);

            // Handle the Tick
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds--;
            if (seconds < 0)
            {
                minutes--;
                seconds = 59;
            }

            if (minutes == 0 && seconds == 0)
            {
                timer.Stop();
                // Add code to handle the end of the Pomodoro session here
            }
            else
            {
                string time = string.Format("{0:00}:{1:00}", minutes, seconds);
                TimerLabel.Content = time;
            }
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Start the Tick
            timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop the Tick
            timer.Stop();
        }
    }
}
