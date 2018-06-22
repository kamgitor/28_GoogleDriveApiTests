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
// using System.Windows.Shapes;


// usingi
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


/*

	NUGET
	Install-Package Google.Apis.Drive.v3

 */


namespace GoogleDriveApiTests
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		// If modifying these scopes, delete your previously saved credentials
		// at ~/.credentials/drive-dotnet-quickstart.json
		static string[] Scopes = { DriveService.Scope.Drive,
					   DriveService.Scope.DriveAppdata,
					   DriveService.Scope.DriveFile,
					   DriveService.Scope.DriveMetadataReadonly,
					   DriveService.Scope.DriveReadonly,
					   DriveService.Scope.DriveScripts};

		static string ApplicationName = "Drive API .NET Quickstart";



		public MainWindow()
		{
			InitializeComponent();
		}

		private void Start_Button_Click(object sender, RoutedEventArgs e)
		{

			UserCredential credential;


			using (var stream =
			new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
			{
				string credPath = System.Environment.GetFolderPath(
					System.Environment.SpecialFolder.Personal);
				credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"kammiltor",			// user
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}


			// Create Drive API service.
			var service = new DriveService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});


			// Define parameters of request.
			FilesResource.ListRequest listRequest = service.Files.List();
			listRequest.PageSize = 10;
			listRequest.Fields = "nextPageToken, files(id, name)";


			// List files.
			IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
				.Files;
			Console.WriteLine("Files:");
			Console.WriteLine("Hello World");
			if (files != null && files.Count > 0)
			{
				foreach (var filee in files)
				{
					Console.WriteLine("{0} ({1})", filee.Name, filee.Id);
				}
			}
			else
			{
				Console.WriteLine("No files found.");
			}


		}
	}
}
