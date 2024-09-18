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
    /// Logika interakcji dla klasy AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private string _date;
        private WeekPlanner _weekPlanner;

        public AddTask(string date, WeekPlanner weekPlanner)
        {
            InitializeComponent();
            _date = date;
            _weekPlanner = weekPlanner;
            DateTextBlock.Text = date;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(task))
            {
                string filePath = "../../../DataBase/tasks.txt";
                string newEntry = $"{_date} ; {task}";

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath).ToList();
                    var existingLine = lines.FirstOrDefault(line => line.StartsWith(_date));

                    if (existingLine != null)
                    {
                        lines[lines.IndexOf(existingLine)] = $"{existingLine} ; {task}";
                    }
                    else
                    {
                        lines.Add(newEntry);
                    }

                    File.WriteAllLines(filePath, lines);
                }
                else
                {
                    File.WriteAllText(filePath, newEntry);
                }

                // Odśwież `ListView` w `WeekPlanner`
                _weekPlanner.RefreshTasks();
            }

            this.Close();
        }
    }


}
