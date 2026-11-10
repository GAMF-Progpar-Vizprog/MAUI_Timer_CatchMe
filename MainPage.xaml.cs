namespace MAUICatchMe;

public partial class MainPage : ContentPage
{
	int Hits
	{
		get => int.Parse(llHits.Text);
		set => llHits.Text = value.ToString();
	}
	int Misses
	{
		get => int.Parse(llMisses.Text);
		set => llMisses.Text = value.ToString();
	}
	readonly IDispatcherTimer timer;

	public MainPage()
	{
		InitializeComponent();
		timer = Dispatcher.CreateTimer();
		timer.Interval = TimeSpan.FromMilliseconds(slTime.Value);
		timer.Tick += Timer_Tick;
	}

	private void Timer_Tick(object? sender, EventArgs e)
		=> MoveCasperToRandomPosition();

	/// <summary>
	/// A figurát véletlenszerű helyre teszi úgy, hogy teljes egészében
	/// a játéktéren belül maradjon.
	/// </summary>
	private void MoveCasperToRandomPosition()
	{
		double x = Random.Shared.NextDouble() *
			(alPlayground.Width - ibCasper.Width);
		double y = Random.Shared.NextDouble() *
			(alPlayground.Height - ibCasper.Height);
		AbsoluteLayout.SetLayoutBounds(ibCasper,
			new Rect(x, y, ibCasper.Width, ibCasper.Height));
	}

	private void btStart_Clicked(object? sender, EventArgs e)
	{
		if (timer.IsRunning) return;
		llCaption.Text = "Game started!";
		timer.Start();
	}

	private void btReset_Clicked(object? sender, EventArgs e)
	{
		Hits = 0;
		Misses = 0;
	}

	private void btStop_Clicked(object? sender, EventArgs e)
	{
		llCaption.Text = "Game stopped!";
		timer.Stop();
	}

	private void ibCasper_Clicked(object? sender, EventArgs e)
	{
		if (!timer.IsRunning) return;
		Hits++;
	}

	private void alPlayground_Tapped(object? sender, TappedEventArgs e)
	{
		if (!timer.IsRunning) return;
		Misses++;
	}

	private void slTime_ValueChanged(object? sender, ValueChangedEventArgs e)
	{
		// Az időzítőt nem kell újra létrehozni: elég leállítani,
		// beállítani az új intervallumot, és ha futott, újraindítani.
		if (timer is null) return;
		bool wasRunning = timer.IsRunning;
		timer.Stop();
		timer.Interval = TimeSpan.FromMilliseconds(e.NewValue);
		if (wasRunning) timer.Start();
	}
}
