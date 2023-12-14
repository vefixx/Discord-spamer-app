using MaterialDesignThemes.Wpf.Converters.CircularProgressBar;
using Newtonsoft.Json;
using Spamer.Roots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Helper
{
    public class AccountsHelper
    {
        CfgHelper cfg_helper = new CfgHelper();
        string accounts_path;

        

        public void Start()
        {
            accounts_path = cfg_helper.GetPaths()["acc_path"];
        }

        public List<Dictionary<string, string>> GetAllAccounts()
        {
            try
            {
                Start();

                List<Dictionary<string, string>> accounts = new List<Dictionary<string, string>>();

                string json = File.ReadAllText(accounts_path);
                Root_accounts root_Accounts = JsonConvert.DeserializeObject<Root_accounts>(json);
            
                foreach (var account in root_Accounts.accounts)
                {
                    Dictionary<string, string> acc = new Dictionary<string, string>
                    {
                        { "username", account.username },
                        { "token", account.token }
                    };

                    accounts.Add(acc);
                }

                return accounts;
            }
            catch
            {
                LogsHelper logs_helper = new LogsHelper();
                logs_helper.AddLog("ERROR", $"Файл с аккаунтами не найден (GetAllAccounts)", "error");

                return null;
            }
            
        }

        public int GetCountAccounts()
        {
            Start();

            List<Dictionary<string, string>> all_acc = GetAllAccounts();
            return all_acc.Count;
        }

        public void AddAccount(string username, string token)
        {
            try
            {
                Start();

                string json = File.ReadAllText(accounts_path);
                Root_accounts root_Accounts = JsonConvert.DeserializeObject<Root_accounts>(json);

                Account new_account = new Account()
                {
                    username = username,
                    token = token
                };

                root_Accounts.accounts.Add(new_account);

                string updatedJson = JsonConvert.SerializeObject(root_Accounts);

                File.WriteAllText(accounts_path, updatedJson);
            }
            catch
            {
                LogsHelper logs_helper = new LogsHelper();
                logs_helper.AddLog("ERROR", $"Файл с аккаунтами не найден (AddAccount)", "error");
            }

        }

        public void DeleteAccount(string username)
        {
            Start();

            string json = File.ReadAllText(accounts_path);
            Root_accounts root_Accounts = JsonConvert.DeserializeObject<Root_accounts>(json);

            Account removeItem = root_Accounts.accounts.Where(d => d.username == username).FirstOrDefault();
            root_Accounts.accounts.Remove(removeItem);
            json = JsonConvert.SerializeObject(root_Accounts, Formatting.Indented);
            File.WriteAllText(accounts_path, json);
        }
    }
}
