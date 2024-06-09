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
    /// Lógica de interacción para MatchHistory.xaml
    /// </summary>
    public partial class MatchHistory : Page
    {
        List<MatchGame> matchesPlayed;
        GameServicesClient gameServicesClient = new GameServicesClient();
        public MatchHistory()
        {
            InitializeComponent();
            getMatchesPlayed();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Lobby());
        }

        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void getMatchesPlayed()
        {
            try
            {
                Player player = SessionManager.Instance.LoggedInPlayer;
                MatchGame[] aux = await gameServicesClient.getMatchesPlayedAsync(player.PlayerID);
                matchesPlayed = aux.ToList();
                if (matchesPlayed.Count > 0)
                {
                    HistoryDataGrid.ItemsSource = matchesPlayed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
