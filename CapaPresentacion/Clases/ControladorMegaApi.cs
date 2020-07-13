using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Clases
{
    public class ControladorMegaApi
    {

        public void UploadFileToMega()
        {
            var client = new MegaApiClient();
            client.Login("sfacturacionj@gmail.com", "Jony*1995");
            IEnumerable<INode> nodes = client.GetNodes();
            Uri uri = new Uri("https://mega.nz/folder/74QCwKoJ#H5_lbdnO-2aQ3WTJxMmXwA");
            IEnumerable<INode> carpeta = client.GetNodesFromLink(uri);
            foreach (INode child in carpeta)
            {
                if (child.Name == "BackUpBaseDatos" + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip")
                {
                    client.Delete(child);
                }
            }
            INode myFile = client.UploadFile("C:/SFacturacion/BD/BackUpBaseDatos" + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip", nodes.Single(x => x.Id == "zswWCIDA"));
            client.Logout();


        }

    }
}
