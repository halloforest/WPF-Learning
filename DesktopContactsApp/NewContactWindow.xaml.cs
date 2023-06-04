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
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();

            saveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contact();
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;



            /*
             * Variantion 1
            var connection = new SQLiteConnection(databasePath);
            connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist
            connection.Insert(contact);
            connection.Close(); // Close the connection to the database
            */

            // Variantion 2
            // The connection is only valid within the scope
            // As a result, the disposed function will be invoked automatically at the end
            using (var connection = new SQLiteConnection(App.databasePath))
            { 
                connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist
                connection.Insert(contact);
            }

            Close();
        }
    }
}
