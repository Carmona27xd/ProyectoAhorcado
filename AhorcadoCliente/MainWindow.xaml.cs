using AhorcadoCliente.Pages;
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

namespace AhorcadoCliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer;
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");
            mediaPlayer = new MediaPlayer();
            playBackGroundMusic();
            goToLoggin();
        }

        private void goToLoggin()
        {
            this.frame.Navigate(new LogIn(this));
        }

        public void changeLanguage(string cultureCode)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);
            this.frame.Navigate(new LogIn(this));
        }

        private void playBackGroundMusic()
        {
            try
            {
                // Ruta relativa al directorio de salida (bin\Debug o bin\Release)
                string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Music", "Music.mp3");

                if (!System.IO.File.Exists(musicPath))
                {
                    MessageBox.Show("Archivo de música no encontrado en la ruta especificada: " + musicPath);
                    return;
                }

                mediaPlayer.Open(new Uri(musicPath, UriKind.Absolute));
                mediaPlayer.Volume = 0.5; // Ajusta el volumen si es necesario
                mediaPlayer.MediaEnded += new EventHandler(MediaPlayer_MediaEnded);
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reproducir la música: {ex.Message}");
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
