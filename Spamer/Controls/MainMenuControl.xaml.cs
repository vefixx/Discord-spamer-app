using Spamer.Helper;
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

namespace Spamer.Controls
{
    /// <summary>
    /// Логика взаимодействия для MainMenuControl.xaml
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        CfgHelper cfg_helper;
        AccountsHelper acc_helper;
        ServersHelper servers_helper;
        public MainMenuControl()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void UpdateInfo()
        {

            cfg_helper = new CfgHelper();
            acc_helper = new AccountsHelper();
            servers_helper = new ServersHelper();

            Dictionary<string, object> cfg_info = cfg_helper.GetCFGInfo();

            count_validAccounts_text.Text = acc_helper.GetCountAccounts().ToString();
            count_servers_text.Text = servers_helper.ServersList().Length.ToString();
            config_name_text.Text = cfg_info["config_use"].ToString();

            string images = cfg_info["images"].ToString();
            if (images.Length > 0)
                images_textbox.Text = images;

            bool script_active = bool.Parse(cfg_info["script_active"].ToString());
            string text;
            if (script_active)
                text = "Работает";
            else
                text = "Выключен";

        }

        private void Update()
        {
            UpdateInfo();
        }

        private void btn_StartOrStopScripts_Click(object sender, RoutedEventArgs e)
        {
            cfg_helper.UpdateScriptStatus();

            Update();
            
        }

        private void btn_ApplyImages_Click(object sender, RoutedEventArgs e)
        {
            cfg_helper.UpdateImages(images_textbox.Text);

            Update();
        }
    }
}
