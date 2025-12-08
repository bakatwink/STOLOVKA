namespace MauiApp1;


public partial class EmployeePage : ContentPage
{
    public EmployeePage()
    {
        InitializeComponent();
    }

    private void QrReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var code = e.Results[0].Value;
        
        QrReader.IsDetecting = false;
        QrReader.IsVisible = false;
        ScanButton.IsVisible = true;
    }

    private void ScanButton_Clicked(object sender, EventArgs e)
    {
        QrReader.IsDetecting = true;
        QrReader.IsVisible = true;
        ScanButton.IsVisible = false;
    }
}
