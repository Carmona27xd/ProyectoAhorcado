using AhorcadoCliente.GameServices;
using AhorcadoCliente.UserServices;
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
    /// Lógica de interacción para InGame.xaml
    /// </summary>
    public partial class InGame : Page
    {
        private DispatcherTimer dispatcherTimer;
        private List<TextBlock> textBlocks = new List<TextBlock>();
        GameServices.GameServicesClient gameServicesClient = new GameServicesClient();
        WordServiceClient wordServiceClient = new WordServiceClient();
        MatchGame matchGame;
        private List<char> charListWord;
        private HashSet<char> guessedLetters = new HashSet<char>();
        string word;
        string clue;
        char selectedLetter;
        int errorCounter = 0;
        int remainingAttempts = 6;

        public InGame(MatchGame matchGame)
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
        }

        private void updateCharSelectDB(char  selectedLetter)
        {
            this.selectedLetter = selectedLetter;
            
            try
            {
                bool confirmation = gameServicesClient.updateCharBD(selectedLetter, matchGame.MatchID);
                if (!confirmation)
                {
                    MessageBox.Show("Error al actualizar la letra en la base de datos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateRemainingAttempts(int remainingAttempts)
        {
            this.remainingAttempts = remainingAttempts;

            try
            {
                bool confirmation = gameServicesClient.updateRemainingAttempts(remainingAttempts, matchGame.MatchID);
                if (!confirmation)
                {
                    MessageBox.Show("Error al actualizar los intentos restantes");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                charListWord = new List<char>(word.ToCharArray()); 
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

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                char letter = button.Content.ToString()[0];
                updateCharSelectDB(letter);

                if (isLetterInWord(letter, word))
                {
                    updateWordLines(letter);
                    guessedLetters.Add(letter); 

                    if (wordIsComplete())
                    {
                        Player player = SessionManager.Instance.LoggedInPlayer;
                        gameServicesClient.updateWinner(player.PlayerID, matchGame.MatchID);
                        gameServicesClient.updateNameWinner(matchGame.MatchID,matchGame.NickNameChallenger);
                        gameServicesClient.finishMatch(matchGame.MatchID);
                        gameServicesClient.updatePointsEarned(player.PlayerID);
                        string message = Properties.Resources.WinnerMatchMessageGuest;
                        MessageBox.Show(message);
                        NavigationService.Navigate(new Lobby());
                    }
                }
                else
                {
                    remainingAttempts--;
                    updateRemainingAttempts(remainingAttempts);
                    errorCounter++;
                    UpdateHangmanImage(errorCounter);

                    if (remainingAttempts == 0)
                    {
                        Player player = SessionManager.Instance.LoggedInPlayer;
                        string message = Properties.Resources.LosserMatchMessage;
                        MessageBox.Show(message);
                        gameServicesClient.updateWinner(matchGame.ChallengerID, matchGame.MatchID);
                        gameServicesClient.updateNameWinner(matchGame.MatchID, player.NickName);
                        gameServicesClient.updatePointsEarned(matchGame.ChallengerID);
                        gameServicesClient.finishMatch(matchGame.MatchID);
                        NavigationService.Navigate(new Lobby());
                    }
                }
                button.IsEnabled = false;
            }
        }


        private bool wordIsComplete()
        {
            return !charListWord.Except(guessedLetters).Any();
        }

        private void UpdateHangmanImage(int errorCounter)
        {
            string imagePath = $"pack://application:,,,/Images/{errorCounter}.jpg"; 
            imageControl.Source = new BitmapImage(new Uri(imagePath));
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

        private void checkMatchStatus()
        {
            int matchStatus = gameServicesClient.getMatchStatus(matchGame.MatchID);
            if (matchStatus == 2)
            {
                string message = Properties.Resources.ChallengerLeaveMatchMessage;  
                MessageBox.Show(message);
                NavigationService.Navigate(new Lobby());
                dispatcherTimer.Stop();
            }
        }

        private bool isLetterInWord(char letter, string word)
        {
            return word.Contains(letter);
        }

        private void LeaveMatch_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("Deseas abandonar la partida?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
    }
}

