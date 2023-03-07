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
        private int seconds = 0;
        private string time = string.Format("{0:00}:{1:00}", 25, 00);

        private string workStatus = "Work";
        private string breakStatus = "Break";

        private int pomodoroTime = 25;
        private int longBreakTime = 15;
        private int shortBreakTime = 5;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set The Labels
            TimerLabel.Content = time;
            StatusLabel.Content = workStatus;

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
                if (StatusLabel.Content.ToString() == workStatus)
                {
                    // Handle the end of the Pomodoro session here
                    // Start the break timer
                    minutes = shortBreakTime;
                    seconds = 0;
                    StatusLabel.Content = breakStatus;
                    timer.Start();
                }
                else
                {
                    // Handle the end of the break here
                    // Start the Pomodoro timer
                    minutes = pomodoroTime;
                    seconds = 0;
                    StatusLabel.Content = workStatus;
                    timer.Start();
                }
            }
            else
            {
                string time = string.Format("{0:00}:{1:00}", minutes, seconds);
                TimerLabel.Content = time;
            }
        }




        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                ToggleButton.Content = "Start";
            }
            else
            {
                timer.Start();
                ToggleButton.Content = "Stop";
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {   
            // Stop the Timer
            timer.Stop();

            // Reset Minutes & Seconds
            minutes = pomodoroTime;
            seconds = 0;

            // Update Label
            string time = string.Format("{0:00}:{1:00}", minutes, seconds);
            TimerLabel.Content = time;
        }
    }
}
