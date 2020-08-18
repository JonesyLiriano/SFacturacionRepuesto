using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Clases
{
    class ControladorImpresoraPapelA4 : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        // Stream que nos ayudara a contener el Report.rdlc 
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        // exportacion del archivo-reporte en formato EMF (Enhanced Metafile).
        private void Export(LocalReport report)
        {
            //las siguientes lineas definen el tamaño de la hoja, en mi caso es de tamaño ticket
            //los tamaños pueden ser en pulgadas(in) o en centimetros(cm), quiza aceptan mas formatos pero no los probé.
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0.078in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //renderizamos el reporte
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
            { stream.Position = 0; }
        }
        // Impresora Para Etiquetas
        private void ExportTermica(LocalReport report)
        {
            //las siguientes lineas definen el tamaño de la hoja, en mi caso es de tamaño ticket
            //los tamaños pueden ser en pulgadas(in) o en centimetros(cm), quiza aceptan mas formatos pero no los probé.
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.5in</PageWidth>
                <PageHeight>6.9in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //renderizamos el reporte
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
            { stream.Position = 0; }
        }
        private void ExportLabel(LocalReport report)
        {
            //las siguientes lineas definen el tamaño de la hoja, en mi caso es de tamaño ticket
            //los tamaños pueden ser en pulgadas(in) o en centimetros(cm), quiza aceptan mas formatos pero no los probé.
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>1.5in</PageWidth>
                <PageHeight>1in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //renderizamos el reporte
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
            { stream.Position = 0; }
        }

        // Handler para los eventos PrintPageEvents
        private void PrintPageTermica(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // ajusta el area rectangular con margenes.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Dibuja un fondo blanco para el reporte
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Dibuja el contenido del reporte
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // pasa a la siguiente pagina y comprueba que no se haya terminado el contenido
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // ajusta el area rectangular con margenes.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Dibuja un fondo blanco para el reporte
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Dibuja el contenido del reporte
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // pasa a la siguiente pagina y comprueba que no se haya terminado el contenido
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }

        private void PrintPageLabel(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // ajusta el area rectangular con margenes.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Dibuja un fondo blanco para el reporte
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Dibuja el contenido del reporte
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // pasa a la siguiente pagina y comprueba que no se haya terminado el contenido
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }

        private void Print()
        {
            PrintDocument printDoc;
            //busca el nombre de la impresora predeterminada
            String printerName = Properties.Settings.Default.Impresora;

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: No hay datos que imprimir.");

            printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
           
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show(String.Format("No se pudo encontrar la impresora \"{0}\".", printerName));
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        //Impresora de etiquetas
        private void PrintTermica()
        {
            PrintDocument printDoc;
            //busca el nombre de la impresora predeterminada
            String printerName = Properties.Settings.Default.Impresora;

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: No hay datos que imprimir.");

            printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show(String.Format("No se pudo encontrar la impresora \"{0}\".", printerName));
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPageTermica);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void PrintLabel()
        {
            PrintDocument printDoc;
            //busca el nombre de la impresora predeterminada
            String printerName = Properties.Settings.Default.ImpresoraTermica;

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: No hay datos que imprimir.");

            printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show(String.Format("No se pudo encontrar la impresora \"{0}\".", printerName));
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPageLabel);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        //Busca Impresora Predeterminada
        //private string ImpresoraPredeterminada()
        //{
        //    for (Int32 i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        //    {
        //        PrinterSettings a = new PrinterSettings();
        //        a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
        //        if (a.IsDefaultPrinter)
        //        { return PrinterSettings.InstalledPrinters[i].ToString(); }
        //    }
        //    return "";
        //}

        // Exporta el reporte a un archivo .emf y lo imprime
        public void Imprime(LocalReport rdlc)
        {
            try
            {
                Export(rdlc);
                Print();
            }
            catch (Exception exc)
            {
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        // Impresora de etiquetas
        public void ImprimeTermica(LocalReport rdlc)
        {
            try
            {
                ExportTermica(rdlc);
                PrintTermica();
            }
            catch (Exception exc)
            {
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        public void ImprimeLabel(LocalReport rdlc)
        {
            try
            {
                ExportLabel(rdlc);
                PrintLabel();
            }
            catch (Exception exc)
            {
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }


        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }

        }
    }
}
