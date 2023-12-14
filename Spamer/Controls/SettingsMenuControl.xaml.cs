using Newtonsoft.Json;
using Spamer.Helper;
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

namespace Spamer.Controls
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenuControl.xaml
    /// </summary>
    public partial class SettingsMenuControl : UserControl
    {
        string cfg_path = "conf/conf.cfg";
        LogsHelper logs_helper = new LogsHelper();

        public SettingsMenuControl()
        {
            InitializeComponent();

            LoadSettings();
        }

        private void btn_cfgApply_Click(object sender, RoutedEventArgs e)
        {
            string json = File.ReadAllText(cfg_path);
            

            Roots.Root_cfg old_cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);

            Roots.Root_cfg new_cfg = new Roots.Root_cfg()
            {
                config_name = old_cfg.config_name,
                cfg_path = cfg_path_box.Text,
                acc_path = acc_path_box.Text,
                txt_path = txt_path_box.Text,
                log_path = log_path_box.Text,
                valid_accounts = old_cfg.valid_accounts,
                sent_messages = old_cfg.sent_messages,
                add_servers = old_cfg.add_servers,
                config_use = old_cfg.config_use,
                script_active = old_cfg.script_active,
                images = old_cfg.images,
                delay = old_cfg.delay
                
            };

            old_cfg = new_cfg;

            string updatedJson = JsonConvert.SerializeObject(old_cfg, Formatting.Indented);
            File.WriteAllText(cfg_path, updatedJson);

            logs_helper.AddLog("UPDATE", "Конфигурация приложения была обновлена", "GOOD");
        }

        public void LoadSettings()
        {
            string json = File.ReadAllText(cfg_path);


            Roots.Root_cfg cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);

            cfg_path_box.Text = cfg.cfg_path;
            acc_path_box.Text = cfg.acc_path;
            txt_path_box.Text = cfg.txt_path;
            log_path_box.Text = cfg.log_path;
            
        }
    }
}
