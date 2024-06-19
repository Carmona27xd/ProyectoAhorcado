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
    /// Lógica de interacción para ViewProfile.xaml
    /// </summary>
    public partial class ViewProfile : Page
    {
        UserServicesClient userServicesClient = new UserServicesClient();
        GameServicesClient gameServicesClient = new GameServicesClient();
        Player player;
        public ViewProfile(Player player)
        {
            InitializeComponent();
            this.player = player;
            initializeInformation();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Lobby());
        }

        private void ModifyProfile_Click(object sender, RoutedEventArgs e)
        {
            Player player = SessionManager.Instance.LoggedInPlayer;
            NavigationService.Navigate(new ModifyProfile(player));
        }

        private void initializeInformation()
        {

            int pointsEarned = userServicesClient.getPointsEarned(player.PlayerID);
            string labelEmailString = Properties.Resources.EmailRegister;
            string nickNameString = Properties.Resources.NickNameRegister;
            string namesString = Properties.Resources.NamesRegister;
            string firstSurnameString = Properties.Resources.FirstNameRegister;
            string secondSurnameString = Properties.Resources.SecondNameRegister;
            string birthDateString = Properties.Resources.BirthDateRegister;
            string telephoneString = Properties.Resources.TelephoneRegister;
            string pointsEarnedString = Properties.Resources.LabelPointsEarned;

            labelEmail.Content = labelEmailString + ": " + player.Email;
            labelNickname.Content = nickNameString + ": " + player.NickName;
            labelNames.Content = namesString + ": " + player.Names;
            labelFirstSurname.Content = firstSurnameString + ": " + player.FirstSurname;
            labelSecondSurname.Content = secondSurnameString + ": " + player.SecondSurname;
            labelPointsEarned.Content = pointsEarnedString + ": " + pointsEarned;
            labelBirthDay.Content = birthDateString + ": " + player.BirthDate;
            labelTelephone.Content = telephoneString + ": " + player.PhoneNumber;
        }

    }
}
