using MauiApp1.Properties;
using ZXing.Net.Maui;
using ZXing.QrCode;

namespace MauiApp1
{

	public partial class StudentPage : ContentPage
	{
		public StudentPage()
		{
			InitializeComponent();
			GenerateQr();
		}

		private void GenerateQr()
		{
			QrShow.Value = Users.GetCrypted(Users.loged.Item1 + Users.loged.Item2);
        }

        private void HideQrButton_Clicked(object sender, EventArgs e)
        {
			ShowQrButton.IsVisible = true;
			QrShow.Value = null;
			QrShow.IsVisible = false;
			HideQrButton.IsVisible = false;
        }

        private void ShowQrButton_Clicked(object sender, EventArgs e)
        {
			GenerateQr();
            ShowQrButton.IsVisible = false;
            QrShow.IsVisible = true;
            HideQrButton.IsVisible = true;
        }
    }
}