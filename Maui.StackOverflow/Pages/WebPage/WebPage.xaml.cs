using System.Diagnostics;

namespace Maui.StackOverflow;

public partial class WebPage : ContentPage
{
	public WebPage()
	{
		InitializeComponent();
	}

	async void Button_Clicked(object sender, EventArgs e)
	{
		await Task.Delay(1);

		try
		{
			string result = await webView.EvaluateJavaScriptAsync("1+2+3+4");
			Debug.WriteLine($"result: {result}"); // 10

			result = await webView.EvaluateJavaScriptAsync("( () => 1 + 2 + 3 )()");
			Debug.WriteLine($"result: {result}"); // 6

			result = await webView.EvaluateJavaScriptAsync("document.title = 'blank'");
			Debug.WriteLine($"result: {result}"); // 55

			//webView.Eval("Promise.resolve(55).then(x => { document.title = JSON.stringify(x) } )");

			result = await webView.EvaluateJavaScriptAsync("Promise.resolve(55).then(x => { document.title = JSON.stringify(x) } )");
			//Debug.WriteLine($"result: {result}"); // 55

			await Task.Delay(1000);
			result = await webView.EvaluateJavaScriptAsync("document.title");
			Debug.WriteLine($"result: {result}"); // 55
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Exception: {ex.Message}");
		}
	}
}