using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace LifePlanner.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        private string _date;
        private string _taskValue;
        private WeekPlanner _weekPlanner;

        public EditTask(string date, string taskValue, WeekPlanner weekPlanner)
        {
            InitializeComponent();
            _date = date;
            _taskValue = taskValue;
            _weekPlanner = weekPlanner;
            DateTextBlock.Text = date;
            TaskTextBox.Text = taskValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newTask = TaskTextBox.Text.Trim();
            string filePath = "../../../DataBase/tasks.txt";

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(_date))
                    {
                        var parts = lines[i].Split(';').Select(part => part.Trim()).ToList();
                        if (newTask == string.Empty)
                        {
                            parts.Remove(_taskValue);
                        }
                        else
                        {
                            int index = parts.IndexOf(_taskValue);
                            if (index != -1)
                            {
                                parts[index] = newTask;
                            }
                        }
                        lines[i] = string.Join(" ; ", parts);
                        break;
                    }
                }
                File.WriteAllLines(filePath, lines);
            }

            _weekPlanner.RefreshTasks();
            this.Close();
        }
    }

}