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
using superhero_social_club.Models;

namespace superhero_social_club
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateUi();
        }

        private void UpdateUi()
        {
            // Rensar Ui
            lvlSuperheroes.Items.Clear();
            txtSuperheroName.Clear();
            txtSuperpower.Clear();
            txtSecretIdentity.Clear();

            // Hämta data från databasen
            using (SuperheroDbContext context = new())
            {
                List<Superhero> superheroes = context.Superheroes.ToList();

                // Populera listview:en med den datan
                foreach (Superhero superhero in superheroes)
                {
                    ListViewItem item = new();
                    item.Content = superhero.SuperheroName;
                    item.Tag = superhero;
                    lvlSuperheroes.Items.Add(item);
                }
            }

            
            
        }

        private void btnAddSuperhero_Click(object sender, RoutedEventArgs e)
        {
            string superHeroName = txtSuperheroName.Text.Trim();
            string superPower = txtSuperpower.Text.Trim();
            string secretIdentity = txtSecretIdentity.Text.Trim();


            
            // Om input parametrar är null eller empty :
            if(string.IsNullOrEmpty(superHeroName) || string.IsNullOrEmpty(superPower) || string.IsNullOrEmpty(secretIdentity))
            {
                MessageBox.Show("Please fill in the boxes!");
            }
            else
            {
                // Om input parametrar är ifyllda, skapa ny superhero.
                using(SuperheroDbContext context = new())
                {
                    Superhero newSuperhero = new();

                    // Sätt properties
                    newSuperhero.SuperheroName = superHeroName;
                    newSuperhero.Superpower = superPower;
                    newSuperhero.SecretIdenty = secretIdentity;
                    newSuperhero.ImageReference = Guid.NewGuid().ToString();

                    // Lägg till heron i databasen.
                    context.Superheroes.Add(newSuperhero);
                    context.SaveChanges();
                }

                UpdateUi();
            }
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if(lvlSuperheroes.SelectedItem != null)
            {
                 // Väljer en superhero i listviewet
                ListViewItem selectedItem = lvlSuperheroes.SelectedItem as ListViewItem;

                // Tillgång till Superhero objekt, connectar selectedItem med objektets TAG.
                Superhero selectedSuperhero = selectedItem.Tag as Superhero;
                
                // Skickar superHeroID till DetailsWindow. Bra att skicka Id't - det är sidans jobb att då fatta vad it
                DetailsWindow detailsWindow = new(selectedSuperhero.SuperheroId);

                detailsWindow.Show();

                Close();

            }
            else
            {
                MessageBox.Show("You have to click on a hero to show details");
            }



            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvlSuperheroes.SelectedItem as ListViewItem;
            Superhero selectedSuperhero = selectedItem.Tag as Superhero;

            using (SuperheroDbContext context = new())
            {

                context.Superheroes.Remove(selectedSuperhero);
                context.SaveChanges();
            }

            UpdateUi();
        }
    }
}
