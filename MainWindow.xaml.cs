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
    public partial class MainWindow : Window
    {
        // Create the Timer
        private DispatcherTimer timer = new DispatcherTimer();

        // Store the time remaining for the current session
        private int minutes = 25;
        private int seconds = 0;

        // Store the statuses for the sessions
        private string workStatus = "Work";
        private string longBreakStatus = "Long Break";
        private string shortBreakStatus = "Short Break";

        // Store the duration of each session
        private int pomodoroTime = 25;
        private int longBreakTime = 15;
        private int shortBreakTime = 5;

        // Store the current session number and maximum number of sessions
        private int curSession = 1;
        private int maxSessions = 4;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the initial values for the timer label and status label
            UpdateTimerLabel();
            StatusLabel.Content = workStatus;

            // Set the interval of the timer to 1 second
            timer.Interval = TimeSpan.FromSeconds(1);

            // Attach the Tick event handler to the timer
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Decrement a second per tick
            seconds--;
            
            // If the number of seconds is less than 0, decrement the minutes and reset the number of seconds
            if (seconds < 0)
            {
                minutes--;
                seconds = 59;
            }

            // If the number of minutes and seconds is 0, move to the next stage.
            if (minutes == 0 && seconds == 0)
            {
                NextStage();
                timer.Start();
            }
            else
            {
                // Update Timer Label
                UpdateTimerLabel();
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // If the timer is currently running stop it
            if (timer.IsEnabled)
            {
                timer.Stop();
                ToggleButton.Content = "Start";
            }
            else
            {
                // If the timer is not currently running, start it
                timer.Start();
                ToggleButton.Content = "Stop";
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            // Move to the next stage
            NextStage();

            // Update timer label
            UpdateTimerLabel();
        }

        private void Reset()
        {
            // Stop the timer
            timer.Stop();

            // Reset the number of minutes and seconds
            minutes = pomodoroTime;
            seconds = 0;

            // Update label
            UpdateTimerLabel();

            // Update Status Label
            StatusLabel.Content = workStatus;

            // Reset Current Session and session label
            curSession = 1;
            UpdateSessionLabel();
        }

        private void NextStage()
        {
            // Stop the timer
            timer.Stop();
            
            // Reset the number of seconds
            seconds = 0;

            // Update the toggle button
            ToggleButton.Content = "Start";

            // Check the status of the timer and swithc to the next stage accordingly
            if (StatusLabel.Content.ToString() == workStatus)
            {
                // If max sessions have been completed, start long break
                if (curSession >= maxSessions)
                {
                    minutes = longBreakTime;
                    StatusLabel.Content = longBreakStatus;
                }
                else
                {
                    minutes = shortBreakTime;
                    StatusLabel.Content = shortBreakStatus;
                } 
            }
            else if (StatusLabel.Content.ToString() == longBreakStatus)
            {
                Reset();
            }
            else
            {
                minutes = pomodoroTime;
                StatusLabel.Content = workStatus;

                // One cycle has been completed
                curSession++;
            }

            // Update the session label
            UpdateSessionLabel();
        }

        private void UpdateTimerLabel()
        {
            TimerLabel.Content = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void UpdateSessionLabel()
        {
            SessionLabel.Content = string.Format("{0}/{1}", curSession, maxSessions);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
