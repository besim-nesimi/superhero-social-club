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
using System.Windows.Shapes;
using superhero_social_club.Models;

namespace superhero_social_club
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        // Denna privata fältvari kommer uppdateras.
        private Superhero superhero = new();
        public DetailsWindow(int superHeroId)
        {
            InitializeComponent();

            GetSuperHero(superHeroId);
        }

        private void GetSuperHero(int superHeroId)
        {
            using (SuperheroDbContext context = new())
            {
                this.superhero = context.Superheroes.Where(s => s.SuperheroId == superHeroId).FirstOrDefault();

                if(this.superhero == null)
                {
                    MessageBox.Show("Something went wrong... Supe not found");
                }
                else
                {
                    txtSuperheroName.Text = this.superhero.SuperheroName;
                    txtSuperpower.Text = this.superhero.Superpower;
                    txtSecretIdentity.Text = this.superhero.SecretIdenty;



                    // Skriver nedan kod för att få in själva bilden till att synas.
                    imgProfile.Source = new BitmapImage(new Uri($@"/Images/{this.superhero.ImageReference}.png", UriKind.Relative));
                }
            }
        }

        private void btnUpdateSuperhero_Click(object sender, RoutedEventArgs e)
        {
            string newSuperheroName = txtSuperheroName.Text.Trim();
            string newSuperpower = txtSuperpower.Text.Trim();
            string newSecretIdentity = txtSecretIdentity.Text.Trim();

            if(this.superhero != null)
            {
                if (newSuperheroName == this.superhero.SuperheroName && newSuperpower == this.superhero.Superpower && newSecretIdentity == this.superhero.SecretIdenty)
                {
                    MessageBox.Show("No changes found. Change something.");
                }
                else
                {
                    // Uppdatera supe
                    using(SuperheroDbContext context = new())
                    {
                        // Först hämtar vi superhjälten, därefter matchar vi superhjälten som vi har klickat på i main window och matchar den med superheroId i databasen.
                        Superhero superheroToUpdate = context.Superheroes.Where(s => s.SuperheroId == this.superhero.SuperheroId).FirstOrDefault();

                        if (superheroToUpdate != null)
                        {
                            // Ändrar supens properties
                            superheroToUpdate.SuperheroName = newSuperheroName;
                            superheroToUpdate.Superpower = newSuperpower;
                            superheroToUpdate.SecretIdenty = newSecretIdentity;

                            context.Superheroes.Update(superheroToUpdate);
                            context.SaveChanges();

                        }

                    }
                    MainWindow mainWindow = new();
                    mainWindow.Show();
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
