﻿using AhorcadoCliente.ServiceReference1;
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

namespace AhorcadoCliente.Pages
{
    /// <summary>
    /// Lógica de interacción para Loggin.xaml
    /// </summary>
    public partial class Loggin : Page
    {
        UserServicesClient userServices = new UserServicesClient();
        private MainWindow mainWindow;

        public Loggin(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ponerCredenciales();
        }

        private void ponerCredenciales()
        {
            txtEmail.Text = "user1@example.com";
            txtPassword.Password = "password123";
        }

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            goToUserRegister();
        }

        private void goToUserRegister()
        {
            NavigationService.Navigate(new Uri("/Pages/UserRegister.xaml", UriKind.Relative));
        }

        private void changeEnslish_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.changeLanguage("en");
        }

        private void changeSpanish_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.changeLanguage("es");
        }

        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Password;

                if (!checkCredentials(email, password))
                {
                    string message = Properties.Resources.IncompleteLogginMessage;
                    MessageBox.Show(message);
                }
                else
                {
                    User user = userServices.logIn(email, password);
                    if (user != null)
                    {
                        string message = Properties.Resources.LogginMessage;
                        string messageComplete = message + " " + user.nickname;
                        MessageBox.Show(messageComplete);
                        SessionManager.Instance.Login(user);

                        NavigationService.Navigate(new Lobby());
                    }
                    else
                    {
                        string message = Properties.Resources.ErrorLogginMessage;
                        MessageBox.Show(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkCredentials(string emai, string password)
        {
            if (string.IsNullOrEmpty(emai) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;

        }
    }
}
