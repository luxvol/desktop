using System.Collections.ObjectModel;
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
using LifePlanner.Windows;

namespace LifePlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Dates { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Dates = new ObservableCollection<string>();
            DataContext = this;
            GenerateWeekDays();
        }

        public void GenerateWeekDays()
        {
            DateTime endDate = DateTime.Now.AddYears(3);
            for (DateTime date = DateTime.Now; date <= endDate; date = date.AddDays(7))
            {
                DateTime mondayOfWeek = date.AddDays(-(int)date.DayOfWeek + 1);
                for (int i = 0; i < 7; i++)
                {
                    Dates.Add(mondayOfWeek.AddDays(i).ToString("dd.MM.yyyy"));
                }
            }
        }

        private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is string date)
            {
                var itemsControl = sender as ItemsControl;
                if (itemsControl != null)
                {
                    ClearHighlight(itemsControl);
                    int index = Dates.IndexOf(date);
                    int startIndex = (index / 7) * 7;
                    int endIndex = startIndex + 7;
                    for (int i = startIndex; i < endIndex && i < Dates.Count; i++)
                    {
                        var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                        if (container != null)
                        {
                            var border = VisualTreeHelper.GetChild(container, 0) as Border;
                            if (border != null)
                            {
                                border.Tag = "Highlight";
                            }
                        }
                    }
                }
            }
        }

        private void ClearHighlight(ItemsControl itemsControl)
        {
            foreach (var item in itemsControl.Items)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container != null)
                {
                    var border = VisualTreeHelper.GetChild(container, 0) as Border;
                    if (border != null)
                    {
                        border.Tag = null;
                    }
                }
            }
        }

        public void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
            {
                border.Tag = null;

            }
        }
        private void ItemsControl_MouseLeave(object sender, MouseEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            if (itemsControl != null)
            {
                ClearHighlight(itemsControl);
            }
        }

        private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is FrameworkElement element && element.DataContext is string date)
            {
                var itemsControl = sender as ItemsControl;
                if (itemsControl != null)
                {
                    int index = Dates.IndexOf(date);
                    int startIndex = (index / 7) * 7;
                    ObservableCollection<string> weekDates = new ObservableCollection<string>();
                    for (int i = startIndex; i < startIndex + 7 && i < Dates.Count; i++)
                    {
                        weekDates.Add(Dates[i]);
                    }
                    WeekPlanner weekPlanner = new WeekPlanner(weekDates);
                    weekPlanner.Show();
                }
            }
        }
    }
}