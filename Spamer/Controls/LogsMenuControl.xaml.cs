using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Spamer.Controls
{
    /// <summary>
    /// Логика взаимодействия для LogsMenuControl.xaml
    /// </summary>
    
    public partial class LogsMenuControl : UserControl
    {
        string cfg_path = "conf/conf.cfg";

        string logs_path;
        string old_logs;

        bool logs_work;

        SolidColorBrush RED_HEX = (SolidColorBrush)new BrushConverter().ConvertFromString("#CC0033");

        DispatcherTimer dt;
        public LogsMenuControl()
        {
            InitializeComponent();
            this.Unloaded += (s, e) => { Console.WriteLine("close"); };

            GetCFG();
            CheckLogPath();
            
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Tick += dt_Tick;
            dt.Start();
            
        }

        public void GetCFG()
        {

            string json = File.ReadAllText(cfg_path);
            Roots.Root_cfg cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);

            logs_path = cfg.log_path;
        }

        public void CheckLogPath()
        {
            if (File.Exists(logs_path))
                logs_work = true;
            else
                logs_work = false;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            CheckLogPath();

            if (!logs_work)
            {
                ListBoxItem lb_item = new ListBoxItem();
                lb_item.Content = "Ошибка! Не получилось загрузить файл с логами. Убедитесь в правильности пути.";
                lb_item.Foreground = RED_HEX;
                LogsBox.Items.Add(lb_item);

                return;
            }

            string[] logs_lines = File.ReadAllLines(logs_path);
            Array.Reverse(logs_lines);
            if (string.Join("", logs_lines) != old_logs)
            {
                LogsBox.Items.Clear();
                
                foreach (string line in logs_lines)
                {

                    ListBoxItem lb_item = new ListBoxItem();
                    lb_item.Content = line;

                    LogsBox.Items.Add(lb_item);
                }

                old_logs = string.Join("", logs_lines);
            }
        }
    }
}
