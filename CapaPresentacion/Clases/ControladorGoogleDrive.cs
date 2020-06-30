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

namespace CapaPresentacion.Clases
{
    class ControladorGoogleDrive
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "SFacturacionRepuesto";


        public void UploadZipFile(string path)
        {
            UserCredential credential;

            credential = GetCredentials();

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            FilesResource.ListRequest req = service.Files.List();

            req.Q = "name= 'BackUpBaseDatos" + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip'";
            FileList list = req.Execute();
            if (list.Files.Count > 0)
            {
                FilesResource.DeleteRequest DeleteRequest = service.Files.Delete(list.Files[0].Id);
                DeleteRequest.Execute();
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Path.GetFileName(path);
            fileMetadata.MimeType = "application/zip";
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/zip");
                request.Fields = "id";
                request.Body.Parents = new List<string>() { "15FOKlicJ5AjpOInE7FBUlzexg6vtnzLD" };
                request.Upload();
            }

            var file = request.ResponseBody;

        }


        private static UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                // Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }
    }
}
