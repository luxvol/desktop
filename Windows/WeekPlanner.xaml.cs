using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WeekPlanner.xaml
    /// </summary>
    public partial class WeekPlanner : Window
    {
        public ObservableCollection<WeekDayRow> WeekRows { get; set; }
        public ObservableCollection<TaskRow> Tasks { get; set; }

        public WeekPlanner()
        {
            InitializeComponent();
            WeekRows = new ObservableCollection<WeekDayRow>()
            {
                new WeekDayRow{
                    Monday = "Monday",
                    Tuesday = "Tuesday",
                    Wednesday = "Wednesday",
                    Thursday = "Thursday",
                    Friday = "Friday",
                    Saturday = "Saturday",
                    Sunday = "Sunday"
                }
            };
            Tasks = new ObservableCollection<TaskRow>();
            WeekListView.ItemsSource = WeekRows;
            WeeklyTasks.ItemsSource = Tasks;
            //AddTasks();
        }

        public WeekPlanner(ObservableCollection<string> weekDays)
        {
            InitializeComponent();
            WeekRows = new ObservableCollection<WeekDayRow>();
            Tasks = new ObservableCollection<TaskRow>();

            for (int i = 0; i < weekDays.Count; i += 7)
            {
                WeekRows.Add(new WeekDayRow
                {
                    Monday = weekDays[i],
                    Tuesday = i + 1 < weekDays.Count ? weekDays[i + 1] : string.Empty,
                    Wednesday = i + 2 < weekDays.Count ? weekDays[i + 2] : string.Empty,
                    Thursday = i + 3 < weekDays.Count ? weekDays[i + 3] : string.Empty,
                    Friday = i + 4 < weekDays.Count ? weekDays[i + 4] : string.Empty,
                    Saturday = i + 5 < weekDays.Count ? weekDays[i + 5] : string.Empty,
                    Sunday = i + 6 < weekDays.Count ? weekDays[i + 6] : string.Empty,
                });
            }

            WeekListView.ItemsSource = WeekRows;
            WeeklyTasks.ItemsSource = Tasks;
            //AddTasks();

            string filePath = "../../../DataBase/tasks.txt";
            LoadTasksFromFile(filePath);
        }

        private void AddTasks()
        {
            Tasks.Add(new TaskRow
            {
                Task1 = "Task for Monday",
                Task2 = "Task for Tuesday",
                Task3 = "Task for Wednesday",
                Task4 = "Task for Thursday",
                Task5 = "Task for Friday",
                Task6 = "Task for Saturday",
                Task7 = "Task for Sunday",
            });
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string day = button.Name switch
                {
                    "Task1AddBtn" => WeekRows[0].Monday,
                    "Task2AddBtn" => WeekRows[0].Tuesday,
                    "Task3AddBtn" => WeekRows[0].Wednesday,
                    "Task4AddBtn" => WeekRows[0].Thursday,
                    "Task5AddBtn" => WeekRows[0].Friday,
                    "Task6AddBtn" => WeekRows[0].Saturday,
                    "Task7AddBtn" => WeekRows[0].Sunday,
                    _ => throw new InvalidOperationException("Unknown button"),
                };

                AddTask addTaskWindow = new AddTask(day, this);
                addTaskWindow.ShowDialog();

            }
        }
        private void LoadTasksFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    // Rozdziel linię na datę i zadania
                    var parts = line.Split(';');
                    if (parts.Length >= 2)
                    {
                        string date = parts[0].Trim();
                        var tasks = parts.Skip(1).Select(task => task.Trim());

                        // Znajdź odpowiedni wiersz tygodnia i dodaj każde zadanie osobno do kolekcji Tasks
                        var weekDayRow = WeekRows.FirstOrDefault(row => row.Monday == date || row.Tuesday == date || row.Wednesday == date ||
                                                                         row.Thursday == date || row.Friday == date || row.Saturday == date || row.Sunday == date);
                        if (weekDayRow != null)
                        {
                            foreach (var task in tasks)
                            {
                                Tasks.Add(new TaskRow
                                {
                                    Task1 = weekDayRow.Monday == date ? task : string.Empty,
                                    Task2 = weekDayRow.Tuesday == date ? task : string.Empty,
                                    Task3 = weekDayRow.Wednesday == date ? task : string.Empty,
                                    Task4 = weekDayRow.Thursday == date ? task : string.Empty,
                                    Task5 = weekDayRow.Friday == date ? task : string.Empty,
                                    Task6 = weekDayRow.Saturday == date ? task : string.Empty,
                                    Task7 = weekDayRow.Sunday == date ? task : string.Empty,
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found: " + filePath);
            }
        }
        public void RefreshTasks()
        {
            Tasks.Clear();
            string filePath = "../../../DataBase/tasks.txt";
            LoadTasksFromFile(filePath);
        }




        public class TaskRow
        {
            public string Task1 { get; set; }
            public string Task2 { get; set; }
            public string Task3 { get; set; }
            public string Task4 { get; set; }
            public string Task5 { get; set; }
            public string Task6 { get; set; }
            public string Task7 { get; set; }
        }

        public class WeekDayRow
        {
            public string Monday { get; set; }
            public string Tuesday { get; set; }
            public string Wednesday { get; set; }
            public string Thursday { get; set; }
            public string Friday { get; set; }
            public string Saturday { get; set; }
            public string Sunday { get; set; }
        }

        private void WeeklyTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WeeklyTasks.SelectedItem is TaskRow selectedTask)
            {
                string selectedDate = null;
                string selectedTaskValue = null;
                int columnIndex = 0;

                foreach (var column in WeekRows[0].GetType().GetProperties())
                {
                    if (column.GetValue(WeekRows[0]).ToString() == selectedTask.GetType().GetProperties()[columnIndex].GetValue(selectedTask).ToString())
                    {
                        selectedDate = column.GetValue(WeekRows[0]).ToString();
                        selectedTaskValue = selectedTask.GetType().GetProperties()[columnIndex].GetValue(selectedTask).ToString();
                        break;
                    }
                    columnIndex++;
                }

                if (selectedDate != null && selectedTaskValue != null)
                {
                    EditTask editTaskWindow = new EditTask(selectedDate, selectedTaskValue, this);
                    editTaskWindow.ShowDialog();
                }
            }
        }
    }
}