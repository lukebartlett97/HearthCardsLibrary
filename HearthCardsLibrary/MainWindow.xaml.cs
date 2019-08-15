using HearthCardsLibrary.Network;
using HearthCardsLibrary.Storage;
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
using Newtonsoft.Json;
using HearthCardsLibrary.Enums;

namespace HearthCardsLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string cardTypeSelected = string.Empty;
        private string cardRaritySelected = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            JSONService.GetInstance().InitialiseFolder();
            ImageService.GetInstance().InitialiseFolder();
            radTypeMinion.IsChecked = true;
            radRarityNone.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(((Button) sender).Name == SubmitButton.Name)
            {
                string postResponse = HearthCardsAPI.getInstance().PostCardData(GetFormData());
                HearthCardsPostResponse res = JsonConvert.DeserializeObject<HearthCardsPostResponse>(postResponse);
                string savePath = ImageService.GetInstance().GetImagePath(res.Cardid);
                CardImage.Source = HearthCardsAPI.getInstance().GetCardImage(res.Url, savePath);
            }
        }

        private CardData GetFormData()
        {
            CardData cardData = new CardData
            {
                Text = txtCardName.Text,
                CardText = txtCardText.Text,
                Mana = txtMana.Text,
                Attack = txtAttack.Text,
                Health = txtHealth.Text,
                Race = txtTribe.Text,
                CardType = cardTypeSelected,
                Gem = cardRaritySelected
            };
            return cardData;
        }

        private void TypeButton_Checked(object sender, RoutedEventArgs e)
        {
            CardType cardType = (CardType)((RadioButton)sender).CommandParameter;
            cardTypeSelected = cardType.GetAPIString();
            EnableAllFields();
            switch (cardType)
            {
                case CardType.Spell:
                    txtHealth.IsEnabled = false;
                    lblHealth.IsEnabled = false;
                    txtAttack.IsEnabled = false;
                    lblAttack.IsEnabled = false;
                    break;
                case CardType.Weapon:
                    txtTribe.IsEnabled = false;
                    lblTribe.IsEnabled = false;
                    break;
                case CardType.Hero:
                    txtTribe.IsEnabled = false;
                    lblTribe.IsEnabled = false;
                    txtAttack.IsEnabled = false;
                    lblAttack.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        private void EnableAllFields()
        {
            txtCardName.IsEnabled = true;
            txtCardText.IsEnabled = true;
            txtMana.IsEnabled = true;
            txtAttack.IsEnabled = true;
            txtHealth.IsEnabled = true;
            txtTribe.IsEnabled = true;
            lblCardName.IsEnabled = true;
            lblCardText.IsEnabled = true;
            lblMana.IsEnabled = true;
            lblAttack.IsEnabled = true;
            lblHealth.IsEnabled = true;
            lblTribe.IsEnabled = true;
        }

        private void RarityButton_Checked(object sender, RoutedEventArgs e)
        {
            cardRaritySelected = ((CardRarity)((RadioButton)sender).CommandParameter).GetAPIString();
        }
    }
}
