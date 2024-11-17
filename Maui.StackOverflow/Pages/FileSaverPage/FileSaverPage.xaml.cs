using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Text;
#if ANDROID
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Webkit;
using AndroidUri = Android.Net.Uri;
#endif

namespace Maui.StackOverflow;

/*
 * Android `Intent.ActionCreateDocument` is used to implement FileSaver which grants permissions to use that content URI in subsequent ContentResolver or ContentProvider operations. These classes should be used to upload content to Google Drive not the File class. It would be academic exercise to create a File-like wrapper for open(), write() and close() methods that would use ContentResolver or ContentProvider classes file Content URIs otherwise use File methods for standalone paths.

References:
 - https://developer.android.com/training/data-storage/shared/documents-files
 - https://learn.microsoft.com/en-us/dotnet/api/android.content.contentresolver.openfiledescriptor?view=net-android-34.0#android-content-contentresolver-openfiledescriptor(android-net-uri-system-string)
 - https://learn.microsoft.com/en-us/dotnet/api/android.content.contentprovider.openfile?view=net-android-34.0
*/

public partial class FileSaverPage : ContentPage
{
	public FileSaverPage()
	{
		InitializeComponent();
	}

	async void OnSave(object sender, EventArgs e)
	{
		await Task.Delay(5);
		//var cts = new CancellationTokenSource();
		//var stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
		//var fileSaverResult = await FileSaver.Default.SaveAsync("test.txt", stream, cts.Token);
		//if (fileSaverResult.IsSuccessful)
		//{
		//	await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cts.Token);
		//}
		//else
		//{
		//	await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cts.Token);
		//}
#if ANDROID
		var intent = new Intent(Intent.ActionCreateDocument);
		//var intent = new Intent(Intent.ActionSend);
		//var intent = new Intent(Intent.ActionPick);
		//intent.AddCategory(Intent.CategoryOpenable);
		//intent.SetType("text/plain");
		//intent.SetType("application/vnd.google-apps.document");
		intent.PutExtra(Intent.ExtraTitle, "test.txt");
		var activity = Platform.CurrentActivity;
		activity?.StartActivity(intent);
#endif
	}
}
