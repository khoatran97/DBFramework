using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DBFramework;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Connector connector = null;
        private Dictionary<string, dynamic> listTable = null;

        public MainWindow()
        {
            InitializeComponent();

            listTable = new Dictionary<string, dynamic>();
        }

        private void LoadListTable()
        {
            listTable = DBContext.instance.getListTable();

            TableCombobox.ItemsSource = listTable.Keys;
        }

        private void ShowData(string tableName)
        {
            Type tableType = Entity.instance.getTypeByName(tableName);

            foreach (PropertyInfo property in tableType.GetProperties())
            {
                var column = new DataGridTextColumn();
                column.Header = property.Name;
                column.Binding = new Binding(property.Name);
                MainDataGrid.Columns.Add(column);
            }

            dynamic context = listTable[tableName];
            var data = context.getAll();

            foreach (object item in data)
            {
                MainDataGrid.Items.Add(item);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string serverName = ServerTextbox.Text;
            string database = DatabaseTextbox.Text;

            bool isWindowsAuthentication = !(NormalAuthenCheckbox.IsChecked.HasValue && NormalAuthenCheckbox.IsChecked.Value);
            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Text;

            connector = new Connector(serverName, database, isWindowsAuthentication, username, password);

            DBContext.setConnector(connector);
            LoadListTable();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadListTable();
        }

        private void TableCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowData(((ComboBox)sender).SelectedValue.ToString());
        }
    }
}
