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
using System.Windows.Threading;

namespace AhorcadoCliente.Pages
{
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Page
    {
        List<MatchGame> matchesAvaliables;
        GameServicesClient gameServicesClient = new GameServicesClient();
        private DispatcherTimer dispatcherTimer;
        public Lobby()
        {
            InitializeComponent();
            LoadUserDetails();
            SetupTimer();
            getAvaliableMatches();
        }

        private void SetupTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            getAvaliableMatches();

        }

        private void joinMatch_Click(object sender, RoutedEventArgs e)
        {
            if (MatchesDataGrid.SelectedItem is MatchGame selectedMatch)
            {
                try
                {
                    Player user = SessionManager.Instance.LoggedInPlayer;
                    gameServicesClient.initMatchGame(user.PlayerID, selectedMatch.MatchID);
                    string message = Properties.Resources.JoinGameMessage;
                    MessageBox.Show(message);
                    dispatcherTimer.Stop();
                    NavigationService.Navigate(new InGame(selectedMatch));
                }
                catch (Exception ex)
                {
                    string message = Properties.Resources.JoinMatchErrorMessage;
                    MessageBox.Show(message);
                }
            }
        }

        private async void getAvaliableMatches()
        {
            try
            {
                Player player = SessionManager.Instance.LoggedInPlayer;
                MatchGame[] aux = await gameServicesClient.getMatchListAsync(player.PlayerID);
                matchesAvaliables = aux.ToList();
                MatchesDataGrid.ItemsSource = null;

                if (matchesAvaliables.Count > 0)
                {
                    MatchesDataGrid.ItemsSource = matchesAvaliables;
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

        private void LogOut_CLick(object sender, RoutedEventArgs e)
        {
            string message = Properties.Resources.LogOutMessage;
            string messageConfirm = Properties.Resources.ConfirmationMessage;
            MessageBoxResult result = MessageBox.Show(message, messageConfirm, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dispatcherTimer.Stop();
                NavigationService.Navigate(new LogIn(Application.Current.MainWindow as MainWindow));
            }
        }

        private void MatchHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            NavigationService.Navigate(new MatchHistory());
        }

        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            //NavigationService.Navigate(new Profile());
            Player player = SessionManager.Instance.LoggedInPlayer;
            goToProfile(player);
        }

        private void goToProfile(Player player)
        {
            if (player != null)
            {
                NavigationService.Navigate(new ViewProfile(player));
            }
        }
    }
}

