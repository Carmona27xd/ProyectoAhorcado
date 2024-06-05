using AhorcadoCliente.GameServices;
using AhorcadoCliente.UserServices;
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
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Page
    {
        List<MatchGame> matchesAvaliables;
        GameServicesClient gameServicesClient = new GameServicesClient();
        public Lobby()
        {
            InitializeComponent();
            LoadUserDetails();
            getAvaliableMatches();
        }

        private void joinMatch_Click(object sender, RoutedEventArgs e)
        {
            if (MatchesDataGrid.SelectedItem is MatchGame selectedMatch)
            {
                MessageBox.Show("Te has unido a la partida!");
                NavigationService.Navigate(new InGame(selectedMatch));
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una partida.");
            }
        }

        private async void getAvaliableMatches()
        {
            try
            {
                MatchGame[] aux = await gameServicesClient.getMatchListAsync();
                matchesAvaliables = aux.ToList();
                if (matchesAvaliables.Count > 0)
                {
                    MatchesDataGrid.ItemsSource = matchesAvaliables;
                }
                else
                {
                    MessageBox.Show("El matchesAvaliables esta vacio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUserDetails()
        {
            Player user = SessionManager.Instance.LoggedInPlayer;
            if (user != null)
            {
                
            }
            
        }

        private void CreateMatch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateMatch());
        }
    }
}

