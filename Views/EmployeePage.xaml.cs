using MauiApp1.Properties;
using ZXing.Net.Maui.Controls;

namespace MauiApp1;
public partial class EmployeePage : ContentPage
{
    private CameraBarcodeReaderView QrReader = new CameraBarcodeReaderView();
    public EmployeePage()
    {
        InitializeComponent();
        this.Title = Users.loged.Item1;
        
        QrReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions{ Formats = ZXing.Net.Maui.BarcodeFormat.QrCode };
        QrReader.IsTorchOn = false;
        QrReader.WidthRequest = 200;
        QrReader.HeightRequest = 200;
        QrReader.BarcodesDetected += QrReader_BarcodesDetected!;
    }

    private void QrReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var code = e.Results[0].Value;
        List<string> ate = new List<string>();
        if (code == Users.studentCrypt["Student1"])
        {
            if (ate.Contains(code)) { Dispatcher.Dispatch(() => { DisplayAlert("Повторное использование QR-кода", $"Иванов Иван: БУ-23", "OK"); }); }
            else
            {
                Dispatcher.Dispatch(() => { DisplayAlert("Успешно", $"Иванов Иван: БУ-23", "OK"); });
                ate.Add(code);
            }
            
        }
        else if(code == Users.studentCrypt["Student2"])
        {
            if (ate.Contains(code)) { Dispatcher.Dispatch(() => { DisplayAlert("Повторное использование QR-кода", $"Пирожков Олег: ИСП-24", "OK"); }); }
            else
            {
                Dispatcher.Dispatch(() => { DisplayAlert("Успешно", $"Пирожков Олег: ИСП-24", "OK"); });
                ate.Add(code);
            }
        }
        else
        {
            Dispatcher.Dispatch(() => { DisplayAlert("Ошибка", $"Неизвесный QR-код", "OK"); });
        }
        QrReader.IsDetecting = false;
        ScanLabel.IsVisible = false;
        EmployeeGrid.Remove(QrReader);
        StopScanButton.IsVisible = false;
        ScanButton.IsVisible = true;
    }

    private void ScanButton_Clicked(object sender, EventArgs e)
    {
        EmployeeGrid.Add(QrReader, 0);
        QrReader.IsDetecting = true;
        StopScanButton.IsVisible = true;
        ScanLabel.IsVisible = true;
        ScanButton.IsVisible = false;
    }

    private void StopScanButton_Clicked(object sender, EventArgs e)
    {
        EmployeeGrid.Remove(QrReader);
        ScanLabel.IsVisible = false;
        StopScanButton.IsVisible = false;
        ScanButton.IsVisible = true;
    }
}
