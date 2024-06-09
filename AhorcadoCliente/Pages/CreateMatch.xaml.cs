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

namespace AhorcadoCliente.Pages
{
    /// <summary>
    /// Lógica de interacción para CreateMatch.xaml
    /// </summary>
    public partial class CreateMatch : Page
    {
        List<Category> categories;
        List<Word> words;
        WordServiceClient wordServiceClient = new WordServiceClient();
        GameServicesClient gameServicesClient = new GameServicesClient();
        private int matchLanguage;

        public CreateMatch()
        {
            InitializeComponent();
            chargeCategories();
            cbSelectWord.IsEnabled = false;
            cbWordCategories.IsEnabled = false;
        }

        private async void CreateMatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MatchGame newMatch = createNewMatch();
                MatchGame confirmation = await gameServicesClient.createMatchAsync(newMatch);
                if (confirmation != null)
                {
                    string message = Properties.Resources.MatchCreatedMessage;
                    MessageBox.Show(message); 
                    NavigationService.Navigate(new WaitingGuest(confirmation));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancelMatchCreation();
        }

        private async void chargeCategories()
        {
            try
            {
                Category[] aux = await wordServiceClient.GetCategoriesAsync();
                categories = aux.ToList();
                cbWordCategories.ItemsSource = categories;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task chargeWordsPerCategory(int category)
        {
            try
            {
                Word[] aux = await wordServiceClient.GetWordsPerCategoryAsync(category);
                words = aux.ToList();
                cbSelectWord.ItemsSource = words;
                cbSelectWord.IsEnabled = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las palabras: {ex.Message}");
            }
        }

        private void SpanishSelection_Click(object sender, RoutedEventArgs e)
        {
            cbWordCategories.DisplayMemberPath = "SpanishCategory";
            cbSelectWord.DisplayMemberPath = "SpanishWord";
            cbWordCategories.IsEnabled = true;
            cbSelectWord.IsEnabled = true;
            matchLanguage = 1;

        }

        private void EnglishSelection_Click(object sender, RoutedEventArgs e)
        {
            cbWordCategories.DisplayMemberPath = "EnglishCategory";
            cbSelectWord.DisplayMemberPath = "EnglishWord";
            cbWordCategories.IsEnabled = true;
            cbSelectWord.IsEnabled = true;
            matchLanguage = 2;
        }

        private async void cbWordCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWordCategories.SelectedItem != null)
            {
                Category selectedCategory = (Category)cbWordCategories.SelectedItem;
                int categoryId = selectedCategory.CategoryID;

                await chargeWordsPerCategory(categoryId);
            }
        }

        private void cancelMatchCreation()
        {
            string message = Properties.Resources.CancelCreateMatchMessage;
            string messageConfirm = Properties.Resources.ConfirmationMessage;
            MessageBoxResult result = MessageBox.Show(message, messageConfirm, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new Lobby());
            }
        }
        private MatchGame createNewMatch()
        {
            try
            {
                Player player = SessionManager.Instance.LoggedInPlayer;
                Word selectedWord = (Word)cbSelectWord.SelectedItem;

                MatchGame newMatch = new MatchGame();
                newMatch.WordID = selectedWord.WordID;
                newMatch.ChallengerID = player.PlayerID;
                newMatch.StatusMatchID = 1;
                newMatch.GuestID = null;
                newMatch.WinnerID = null;
                newMatch.DateMatch = DateTime.Now.ToString(("dd/MM/yyyy"));
                newMatch.SelectLetter = null;
                newMatch.RemainingAttempts = 6;
                newMatch.NickNameChallenger = player.NickName;
                newMatch.EmailChallenger = player.Email;
                newMatch.MatchLanguage = matchLanguage;

                return newMatch;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
