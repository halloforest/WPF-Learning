﻿using DesktopContactsApp.Classes;
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

            // Put this window in the central of the main window
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (contact == null)    // Via add button
            {
                this.contact = new Contact();
            } 
            else
            {
                this.contact = contact;
                nameTextBox.Text = contact.Name;
                emailTextBox.Text = contact.Email;
                phoneTextBox.Text = contact.Phone;
            }

            updateButton.Click += UpdateButton_Click;
            deleteButton.Click += DeleteButton_Click;
            cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

                // Contact exists in the table -> update, otherwise -> insert
                if(connection.Table<Contact>().Any(c => c.Id == contact.Id)) connection.Update(contact);
                else connection.Insert(contact);                     
            }

            Close();
        }
    }
}
