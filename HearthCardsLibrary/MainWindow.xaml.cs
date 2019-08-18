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
using System.IO;

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
            JSONService.Instance.InitialiseFolder();
            ImageService.Instance.InitialiseFolder();
            radTypeMinion.IsChecked = true;
            radRarityNone.IsChecked = true;
            cmbCardClass.ItemsSource = Enum.GetValues(typeof(CardClass)).Cast<CardClass>();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CardData formData = GetFormData();
            string postResponse = HearthCardsAPI.Instance.PostCardData(formData);
            HearthCardsPostResponse res = JsonConvert.DeserializeObject<HearthCardsPostResponse>(postResponse);
            string savePath = ImageService.Instance.GetImagePath(res.Cardid);
            formData.GeneratedImage = HearthCardsAPI.Instance.GetCardImage(res.Url, savePath);
            JSONService.Instance.SaveCard(formData);
            CardImage.Source = formData.GeneratedImage == null ? null : ByteToImageSource(formData.GeneratedImage);
        }
        private static ImageSource ByteToImageSource(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }

        private CardData GetFormData()
        {
            CardData cardData = new CardData
            {
                UniqueName = txtUniqueName.Text,
                Text = txtCardName.Text,
                CardText = txtCardText.Text,
                Mana = txtMana.Text,
                Attack = txtAttack.Text,
                Health = txtHealth.Text,
                Race = txtTribe.Text,
                CardType = cardTypeSelected,
                Gem = cardRaritySelected,
                CardClass = ((CardClass) cmbCardClass.SelectedValue).GetAPIString(),
                UserImage = ImageService.Instance.GetImage(txtImageFile.Text)
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
            cmbCardClass.IsEnabled = true;
            txtCardText.IsEnabled = true;
            txtMana.IsEnabled = true;
            txtAttack.IsEnabled = true;
            txtHealth.IsEnabled = true;
            txtTribe.IsEnabled = true;
            lblCardName.IsEnabled = true;
            lblCardClass.IsEnabled = true;
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

        private void PickFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = "CardImages";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                txtImageFile.Text = filename;
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ImageService.Instance.ExportImage(ImageSourceToBytes(new PngBitmapEncoder(), CardImage.Source));
        }
    }
}
