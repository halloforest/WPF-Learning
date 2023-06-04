using DesktopContactsApp.Classes;
using SQLite;
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

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();

            newContactButton.Click += NewContactButton_Click;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;

            ReadDatabase();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Filter the name
            var filteredList = contacts.Where(c => c.Name.Contains(searchTextBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            // Alternativ - how to convert every name string, costs overhead
            // var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            contactsListView.ItemsSource = filteredList;
        }

        private void NewContactButton_Click(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog(); // Back to the previous windows only after this window is closed

            ReadDatabase();
        }

        void ReadDatabase()
        {

            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist
                contacts = connection.Table<Contact>().ToList();
            }


            if (contacts != null)
            {
                /*
                 * Add a new Item
                foreach (var contact in contacts) 
                {
                    contactsListView.Items.Add(new ListViewItem() { Content = contact.Id + ": " + contact.Name + " " + contact.Email + " " + contact.Phone});
                }
                */

                // Assign the item source
                contactsListView.ItemsSource = contacts;
            }

        }
    }
}
