using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string currentTableName;
        private dynamic currentTable;
        private ObservableCollection<dynamic> data;
        private List<DataGridRow> newRows;
        private List<DataGridRow> updateRows;
        private List<dynamic> deleteItems;

        public MainWindow()
        {
            InitializeComponent();

            listTable = new Dictionary<string, dynamic>();
            currentTable = null;
            currentTableName = "";
            newRows = new List<DataGridRow>();
            updateRows = new List<DataGridRow>();
            deleteItems = new List<dynamic>();
        }

        private void LoadListTable()
        {
            listTable = DBContext.instance.getListTable();

            TableCombobox.ItemsSource = listTable.Keys;
        }

        private void ShowData(string tableName)
        {
            Type tableType = Entity.instance.getTypeByName(tableName);
            
            currentTable = listTable[tableName];
            data = new ObservableCollection<dynamic>(currentTable.getAll());

            CollectionViewSource categoryViewSource = ((CollectionViewSource)(this.FindResource("TableData")));

            categoryViewSource.Source = data;
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
            if (currentTableName != "")
            {
                ShowData(currentTableName);
            }
        }

        private void TableCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentTableName = ((ComboBox)sender).SelectedValue.ToString();
            ShowData(currentTableName);
        }

        private void MainDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Row.IsNewItem)
                {
                    newRows.Add(e.Row);
                }
                else
                {
                    updateRows.Add(e.Row);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataGridRow row in newRows)
            {
                dynamic item = row.Item;
                currentTable.add(item);
            }
            foreach (DataGridRow row in updateRows)
            {
                dynamic item = row.Item;
                currentTable.update(item);
            }
            foreach (dynamic item in deleteItems)
            {
                currentTable.delete(item.id);
            }
            newRows = new List<DataGridRow>();
            updateRows = new List<DataGridRow>();
            deleteItems = new List<dynamic>();

            ShowData(currentTableName);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deletingItem = MainDataGrid.CurrentCell.Item;
            data.Remove(deletingItem);
            foreach (DataGridRow row in newRows)
            {
                dynamic item = row.Item;
                if (deletingItem.Equals(item))
                {
                    newRows.Remove(row);
                    return;
                }
            }
            foreach (DataGridRow row in updateRows)
            {
                dynamic item = row.Item;
                if (deletingItem.Equals(item))
                {
                    updateRows.Remove(item);
                    return;
                }
            }

            deleteItems.Add(deletingItem);
        }
    }
}
