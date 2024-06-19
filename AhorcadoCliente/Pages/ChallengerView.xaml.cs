using AhorcadoCliente.GameServices;
using AhorcadoCliente.WordServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows;
using System;

namespace AhorcadoCliente.Pages
{
    /// <summary>
    /// Lógica de interacción para ChallengerView.xaml
    /// </summary>
    public partial class ChallengerView : Page
    {
        private DispatcherTimer dispatcherTimer;
        private List<TextBlock> textBlocks = new List<TextBlock>();
        GameServices.GameServicesClient gameServicesClient = new GameServicesClient();
        WordServiceClient wordServiceClient = new WordServiceClient();
        string word;
        string clue;
        char? guestSelectLetter;
        int remainingAttempts;
        private int initialAttempts = 6;
        MatchGame matchGame;
        private bool matchFinished = false;  // Variable de control para evitar mensajes duplicados

        public ChallengerView(MatchGame matchGame)
        {
            InitializeComponent();
            LoadMatchWord(matchGame);
            loadMatchClue(matchGame);
            this.matchGame = matchGame;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!matchFinished)  // Verificar si la partida ya ha terminado
            {
                //checkMatchStatus();
                getGuestLetter(matchGame);
                updateImage();
                checkGuestLeave();
                checkMatchFinish();
            }
        }

        private void getGuestLetter(MatchGame matchID)
        {
            char? guestSelectLetter = gameServicesClient.getGuestLetter(matchGame.MatchID);

            if (guestSelectLetter.HasValue)
            {
                char selectLetter = guestSelectLetter.Value;
                updateWordLines(selectLetter);
            }
        }

        private int getRemainingAttempts(MatchGame matchGame)
        {
            remainingAttempts = gameServicesClient.getRemainingAttempts(matchGame.MatchID);
            return remainingAttempts;
        }

        private void updateImage()
        {
            int newRemainingAttempts = gameServicesClient.getRemainingAttempts(matchGame.MatchID);
            if (newRemainingAttempts != remainingAttempts)
            {
                remainingAttempts = newRemainingAttempts;
                int errorCounter = 6 - remainingAttempts;
                changeImage(errorCounter);
            }
            else if (remainingAttempts == 0)
            {
                matchFinished = true;  // Indicar que la partida ha terminado
                dispatcherTimer.Stop();
                gameServicesClient.finishMatch(matchGame.MatchID);
                string message = Properties.Resources.WinnerMatchMessageChallenger;
                MessageBox.Show(message);
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new Lobby());
                }
                else
                {
                    MessageBox.Show("NavigationService is not available.");
                }
            }
        }

        private void changeImage(int errorCounter)
        {
            string imagePath = $"pack://application:,,,/Images/{errorCounter}.jpg";
            imageControl.Source = new BitmapImage(new Uri(imagePath));
        }

        private void waitGuest(bool thereIsGuest)
        {
            while (!thereIsGuest)
            {
                string message = Properties.Resources.WaitGuestMessage;
                MessageBox.Show(message);
            }
        }

        private void updateWordLines(char Letter)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Letter)
                {
                    textBlocks[i].Text = Letter.ToString();
                }
            }
        }

        private void generateWordLines(string word)
        {
            WordPanel.Children.Clear();
            textBlocks.Clear();

            foreach (char letter in word)
            {
                TextBlock texBlock = new TextBlock
                {
                    Text = "_",
                    FontSize = 60,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5)
                };

                textBlocks.Add(texBlock);
                WordPanel.Children.Add(texBlock);
            }
        }

        private async void loadMatchClue(MatchGame matchGame)
        {
            try
            {
                clue = await getMatchClueAsync(matchGame);
                txtBockClue.Text = clue;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading match clue: {e.Message}");
            }
        }

        private async void LoadMatchWord(MatchGame matchGame)
        {
            try
            {
                word = await getMatchWordAsync(matchGame);
                generateWordLines(word);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading match word: {e.Message}");
            }
        }

        private async Task<string> getMatchClueAsync(MatchGame match)
        {
            string clueMatch = null;
            try
            {
                if (match != null && match.MatchLanguage == 1)
                {
                    clueMatch = await wordServiceClient.getClueSpanishAsync(match.WordID);
                }
                else if (match != null && match.MatchLanguage == 2)
                {
                    clueMatch = await wordServiceClient.getClueEnglishAsync(match.WordID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return clueMatch;
        }

        private async Task<string> getMatchWordAsync(MatchGame match)
        {
            string wordMatch = null;
            try
            {
                if (match != null && match.MatchLanguage == 1)
                {
                    wordMatch = await wordServiceClient.getWordSpanishAsync(match.WordID);
                }
                else if (match != null && match.MatchLanguage == 2)
                {
                    wordMatch = await wordServiceClient.getWordEnglishAsync(match.WordID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return wordMatch;
        }

        private void LeaveMatch_Click(object sender, RoutedEventArgs e)
        {
            string message = Properties.Resources.LeaveMatchConfirmation;
            string confirmation = Properties.Resources.ConfirmationMessage;
            MessageBoxResult result = MessageBox.Show(message, confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LeaveMatch();
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

        private void checkMatchFinish()
        {
            if (matchFinished) return;  // Salir si la partida ya ha terminado

            int matchStatus = gameServicesClient.getMatchStatus(matchGame.MatchID);
            if (matchStatus == 3)
            {
                matchFinished = true;  // Indicar que la partida ha terminado
                dispatcherTimer.Stop();
                string messagge = Properties.Resources.LosserChallengerMessage;
                MessageBox.Show(messagge);

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new Lobby());
                }
                else
                {
                    MessageBox.Show("NavigationService is not available.");
                }
            }
        }

        private void checkGuestLeave()
        {
            int matchStatus = gameServicesClient.getMatchStatus(matchGame.MatchID);
            if (matchStatus == 2)
            {
                dispatcherTimer.Stop();
                string message = Properties.Resources.GuestLeaveMatchMessage;
                MessageBox.Show(message);
                NavigationService.Navigate(new Lobby());
            }
        }
    }
}
