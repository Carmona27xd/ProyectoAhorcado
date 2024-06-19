using AhorcadoCliente.GameServices;
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
    /// Lógica de interacción para WaitingGuest.xaml
    /// </summary>
    public partial class WaitingGuest : Page
    {
        private DispatcherTimer dispatcherTimer;
        GameServices.GameServicesClient gameServicesClient = new GameServicesClient();
        MatchGame matchGame;
        public WaitingGuest(MatchGame matchGame)
        {
            InitializeComponent();
            this.matchGame = matchGame;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            isThereGuest();
        }

        private void isThereGuest()
        {
            bool isThereGuest = gameServicesClient.isThereGuest(matchGame.MatchID);
            if (isThereGuest)
            {
                dispatcherTimer.Stop();
                string message = Properties.Resources.PlayerJoinedMessage;
                lbWaitingPlayer.Content = message;
                btnLeaveMatch.IsEnabled = false;
                DispatcherTimer delayTimer = new DispatcherTimer();
                delayTimer.Interval = TimeSpan.FromSeconds(1); 
                delayTimer.Tick += (sender, e) =>
                {
                    delayTimer.Stop(); 
                    NavigationService.Navigate(new ChallengerView(matchGame)); 
                };
                delayTimer.Start(); 
            }
        }

        private void LeaveMatch()
        {
            dispatcherTimer.Stop();
            try
            {
                bool exit = gameServicesClient.leaveMatch(matchGame.MatchID);
                if (exit)
                {
                    string message = Properties.Resources.MatchLeaveMessage;
                    MessageBox.Show(message);
                    NavigationService.Navigate(new Lobby());

                }
                else
                {
                    string message = Properties.Resources.MatchLeaveMessageError;
                    MessageBox.Show(message);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void LeaveMatch_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseas abandonar la partida?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                LeaveMatch();
            }
        }
    }
}
