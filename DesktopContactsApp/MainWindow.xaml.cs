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

            // Add new contact button
            newContactButton.Click += NewContactButton_Click;

            // Search box
            searchTextBox.TextChanged += SearchTextBox_TextChanged;

            // Double click a contact from the list
            contactsListView.MouseDoubleClick += ContactsListView_MouseDoubleClick; ;

            UpdateContacts();
        }

        private void ContactsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedContact = contactsListView.SelectedItem as Contact;

            if (selectedContact != null)
            {
                // Open the Contact Details Window
                var contactDetailsWindow = new Contact_Details_Window(selectedContact);
                contactDetailsWindow.ShowDialog();

                UpdateContacts();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Filter the name
            var filteredList = contacts.Where(c => c.Name.Contains(searchTextBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            // Rewrite "where"
            var filteredList2 = from c in contacts where c.Name.Contains(searchTextBox.Text, StringComparison.OrdinalIgnoreCase) select c; 

            // Alternativ - how to convert every name string, costs overhead
            // var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            contactsListView.ItemsSource = filteredList;
        }

        private void NewContactButton_Click(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog(); // Back to the previous windows only after this window is closed

            UpdateContacts();
        }

        void UpdateContacts()
        {

            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist

                // Order by name
                contacts = (connection.Table<Contact>().ToList()).OrderBy(c => c.Name).ToList();

                // write "c => c.Name" in another way
                // var contact = from c in contacts orderby c.Name select c;
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
