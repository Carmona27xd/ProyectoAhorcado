using AhorcadoCliente.ServiceReference1;
using AhorcadoCliente.ServiceReference2;
using AhorcadoCliente.ServiceReference3;
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
                Match newMatch = createNewMatch();
                bool confirmation = await gameServicesClient.createMatchAsync(newMatch);
                if (confirmation)
                {
                    MessageBox.Show("Partida creada con exito");
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
            cbWordCategories.DisplayMemberPath = "categoria_espanol";
            cbSelectWord.DisplayMemberPath = "palabra_espanol";
            cbWordCategories.IsEnabled = true;
            cbSelectWord.IsEnabled = true;

        }

        private void EnglishSelection_Click(object sender, RoutedEventArgs e)
        {
            cbWordCategories.DisplayMemberPath = "categoria_ingles";
            cbSelectWord.DisplayMemberPath = "palabra_ingles";
            cbWordCategories.IsEnabled = true;
            cbSelectWord.IsEnabled = true;
        }

        private async void cbWordCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWordCategories.SelectedItem != null)
            {
                Category selectedCategory = (Category)cbWordCategories.SelectedItem;
                int categoryId = selectedCategory.id_categoria;

                await chargeWordsPerCategory(categoryId);
            }
        }

        private void cancelMatchCreation()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        private Match createNewMatch()
        {
            try
            {
                User user = SessionManager.Instance.LoggedInUser;
                Word selectedWord = (Word)cbSelectWord.SelectedItem;

                Match newMatch = new Match();
                newMatch.id_palabra = selectedWord.id_palabra;
                newMatch.id_creador = user.id_cuenta;
                newMatch.estado_partida = 1;
                newMatch.id_retador = null;
                newMatch.id_ganador = null;
                newMatch.fecha_partida = DateTime.Now.ToString(("dd/MM/yyyy"));
                newMatch.letra_seleccionada = null;
                newMatch.intentos_restantes = 6;
                newMatch.nickname_creador = user.nickname;
                newMatch.correo_creador = user.correo;

                return newMatch;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
