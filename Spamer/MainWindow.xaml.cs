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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spamer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FoldingWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Controls.MainMenuControl();
        }

        private void btn_Accounts_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Controls.AccountsControl();
        }

        private void btn_LogsMenu_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Controls.LogsMenuControl();
        }

        private void btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Controls.SettingsMenuControl();
        }
    }
}
