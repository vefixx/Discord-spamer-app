using Spamer.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;
using Spamer.Roots;
using System.Data;
using System.Xml;

namespace Spamer.Controls
{
    /// <summary>
    /// Логика взаимодействия для AccountsControl.xaml
    /// </summary>
    public partial class AccountsControl : UserControl
    {
        AccountsHelper acc_helper;
        LogsHelper logs_helper;

        public AccountsControl()
        {
            InitializeComponent();
            
            acc_helper = new AccountsHelper();
            logs_helper = new LogsHelper();

            UpdateAccounts();
        }

        private void UpdateAccounts()
        {
            ListBox_Accounts.Items.Clear();
            List<Dictionary<string, string>> all_accounts = acc_helper.GetAllAccounts();
            foreach (var account in all_accounts)
            {
                ListBoxItem lb_item = new ListBoxItem();
                lb_item.Content = account["username"];
                ListBox_Accounts.Items.Add(lb_item);
            }


        }

        private async void AccountsList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ListBox_Accounts.Items.Clear();

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string[] file_accounts = File.ReadAllLines(files[0]);

                List<Task> tasks = new List<Task>();
                foreach ( string account in file_accounts )
                {
                    tasks.Add(MultiThread_AddAccount(account));
                }
                await Task.WhenAll(tasks);

                UpdateAccounts();
            }
        }

        private async Task MultiThread_AddAccount(string account_token)
        {
            bool isValid = await CheckoutValidAccount(account_token);
            if (isValid)
            {
                string username = await GetUsernameAsync(account_token);

                acc_helper.AddAccount(username, account_token);

                logs_helper.AddLog("ADD", $"Добавлен новый аккаунт ({username})", "200");
            }
        }

        public async Task<bool> CheckoutValidAccount(string token)
        {
            if (Regex.Match(token, "[А-Яа-яЁё]").Success)
            {
                return false;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://discord.com/api/v9/");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                try
                {
                    var response = await httpClient.GetAsync("users/@me");
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<string> GetUsernameAsync(string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);


                HttpResponseMessage response = await httpClient.GetAsync("https://discord.com/api/v9/users/@me");
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Root_JsonUserData root = JsonConvert.DeserializeObject<Root_JsonUserData>(jsonResponse);

                    string username = root.global_name;

                    return username;
                }
                else
                {
                    return null;
                }

            }
        }

        private void btn_UpdateAccounts_Click(object sender, RoutedEventArgs e)
        {
            UpdateAccounts();
        }

        private void listbox_Account_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_DeleteAccounts_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)ListBox_Accounts.SelectedItem;

            if (item == null)
                return;

            acc_helper.DeleteAccount(item.Content.ToString());

            UpdateAccounts();

            logs_helper.AddLog("DELETE", $"Удален аккаунт ({item.Content})", "GOOD");
        }
    }
}
