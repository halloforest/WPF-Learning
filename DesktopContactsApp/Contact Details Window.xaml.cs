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
    /// Interaction logic for Contact_Details_Window.xaml
    /// </summary>
    public partial class Contact_Details_Window : Window
    {
        private Contact contact;
        public Contact_Details_Window(Contact contact)
        {
            InitializeComponent();

            this.contact = contact;

            updateButton.Click += UpdateButton_Click;
            deleteButton.Click += DeleteButton_Click;

            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist
                connection.Delete(contact);
            }

            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;
            
            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); // This operation will be ignored if this table already exist
                connection.Update(contact);
            }

            Close();
        }
    }
}
