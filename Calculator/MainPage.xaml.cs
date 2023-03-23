namespace Calculator;

public partial class MainPage : ContentPage
{
    private CancellationTokenSource source;
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("Calculator");
	}
	private async void OnStartClicked(object sender, EventArgs e)
	{
        source = new CancellationTokenSource();
        Button button = sender as Button;
        button.IsEnabled = false;
        LoadText.Text = "Computing...";
        ProgressResult.Text = "0";
        await Task.Run(() => CalculateIntegral(), source.Token);
    }
    private void OnCancelClicked(object sender, EventArgs e)
    {
        if (source != null && !source.IsCancellationRequested)
        {
            source.Cancel();
        }
    }
    private void CalculateIntegral()
    {
        double step = 0.00001, integral = 0, progress = 0;
        for (double x = 0; x <= 1; x += step)
        {
            if (source.IsCancellationRequested)
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    ProgressBarIndicator.Progress = 0;
                    ProgressResult.Text = "0%";
                    StartButton.IsEnabled = true;
                    LoadText.Text = "Task cancelled";
                });
                return;
            }
            integral += step * Math.Sin(x);
            for (int i = 0; i < 10000; i++)
            {
                int tmp = i * i;
            }
            progress = Math.Round(x, 2);
            MainThread.BeginInvokeOnMainThread(() => {
                ProgressBarIndicator.Progress = progress;
                ProgressResult.Text = Convert.ToInt32(progress * 100).ToString() + "%";
            });
        }
        MainThread.BeginInvokeOnMainThread(() => {
            ProgressBarIndicator.Progress = 1;
            ProgressResult.Text = "100%";
            StartButton.IsEnabled = true;
            LoadText.Text = "Completed successfully!\nIntegral equals " + integral.ToString();
        });
    }
}

