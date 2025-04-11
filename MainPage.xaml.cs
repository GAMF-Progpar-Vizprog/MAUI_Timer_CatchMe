using Microsoft.Maui.Graphics;
namespace MAUICatchMe;

public partial class MainPage : ContentPage
{
	int Hits
	{
		get { return int.Parse(llHits.Text); }
		set { llHits.Text = value.ToString(); }
	}
	int Misses
	{
		get { return int.Parse(llMisses.Text); }
		set { llMisses.Text = value.ToString(); }
	}
	Random rnd = new Random();
	IDispatcherTimer timer;
	public MainPage()
	{
		InitializeComponent();
		timer = Dispatcher.CreateTimer();
		timer.Tick += Timer_Tick;
		timer.Interval = TimeSpan.FromMilliseconds(2000);
	}
	private void Timer_Tick(object sender, EventArgs e)
	{
		double x = rnd.NextDouble() *
			(alPlayground.Width - ibCasper.Width);
		double y = rnd.NextDouble() *
			(alPlayground.Height - ibCasper.Height);
		AbsoluteLayout.SetLayoutBounds(ibCasper,
			new Rect(x, y, ibCasper.Width, ibCasper.Height));
	}
	private void btStart_Clicked(object sender, EventArgs e)
	{
		if (timer.IsRunning) return;
		llCaption.Text = "Game started!";
		timer.Start();

	}
	private void btReset_Clicked(object sender, EventArgs e)
	{
		Hits = 0;
		Misses = 0;
	}
	private void btStop_Clicked(object sender, EventArgs e)
	{
		llCaption.Text = "Game stopped!";
		timer.Stop();
	}
	private void ibCasper_Clicked(object sender, EventArgs e)
	{
		if (!timer.IsRunning) return;
		Hits++;
	}
	private void alPlayground_Tapped(object sender, EventArgs e)
	{
		if (!timer.IsRunning) return;
		Misses++;
	}

	private void slTime_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		bool IsRunning = timer.IsRunning;
		if (IsRunning)
		{
			timer.Stop();
		}
		timer.Tick -= Timer_Tick;
		timer= Dispatcher.CreateTimer();
		timer.Tick += Timer_Tick;
		timer.Interval = TimeSpan.FromMilliseconds(slTime.Value);
		if (IsRunning)
		{
			timer.Start();
		}
	}
}

