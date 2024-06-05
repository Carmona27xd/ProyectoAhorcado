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

namespace AhorcadoCliente.Pages
{
    /// <summary>
    /// Lógica de interacción para InGame.xaml
    /// </summary>
    public partial class InGame : Page
    {
        private List<TextBlock> textBlocks = new List<TextBlock>();
        GameServices.GameServicesClient gameServicesClient = new GameServicesClient();
        WordServiceClient wordServiceClient = new WordServiceClient();
        string word;

        public InGame(MatchGame matchGame)
        {
            InitializeComponent();
            LoadMatchWord(matchGame);

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
                string letter = button.Content.ToString();
                MessageBox.Show($"Button {letter} clicked");
            }
        }
    }
}

