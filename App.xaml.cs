namespace MAUICatchMe;

public partial class App : Application
{
	// Az ablak kezdeti mérete asztali platformon (Windows, Mac Catalyst),
	// eszközfüggetlen egységben. Mobilon nincs hatása.
	const int WindowWidth = 500;
	const int WindowHeight = 900;

	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell())
		{
			Width = WindowWidth,
			Height = WindowHeight
		};
	}
}
