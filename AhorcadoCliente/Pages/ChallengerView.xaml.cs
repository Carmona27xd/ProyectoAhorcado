using AhorcadoCliente.GameServices;
using AhorcadoCliente.WordServices;
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
            checkMatchStatus();
            getGuestLetter(matchGame);
            updateImage();
            
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
                gameServicesClient.finishMatch(matchGame.MatchID);
                string message = Properties.Resources.WinnerMatchMessageChallenger;
                dispatcherTimer.Stop();
                MessageBox.Show(message);
                NavigationService.Navigate(new Lobby());
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

        private void checkMatchStatus()
        {
            int matchStatus = gameServicesClient.getMatchStatus(matchGame.MatchID);
            if (matchStatus == 2)
            {
                string message = Properties.Resources.ChallengerLeaveMatchMessage;
                MessageBox.Show(message);
                dispatcherTimer.Stop();
                NavigationService.Navigate(new Lobby());
            }
            
            if (matchStatus == 3)
            {
                dispatcherTimer.Stop();

                DispatcherTimer delayTimer = new DispatcherTimer();
                delayTimer.Interval = TimeSpan.FromSeconds(1);
                delayTimer.Tick += (sender, e) =>
                {
                    delayTimer.Stop();
                    string messagge = Properties.Resources.LosserChallengerMessage;
                    MessageBox.Show(messagge);
                    NavigationService.Navigate(new Lobby());
                };
                delayTimer.Start();

            }
        }
    }
}
