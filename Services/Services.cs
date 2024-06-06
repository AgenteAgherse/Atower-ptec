using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO.Compression;

namespace PruebaTecnica.Services
{
    public class Services
    {

        public static byte[] GetPdf(List<string> urls, IConverter _converter)
        {

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { PaperSize = PaperKind.A4 }
            };

            foreach (var url in urls)
            {
                doc.Objects.Add(new ObjectSettings() { Page = url });
            }
            return _converter.Convert(doc);
        }


        public static byte[] CreateZipFromPdf(byte[] pdfData)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var zipEntry = archive.CreateEntry("output.pdf", CompressionLevel.Fastest);
                    using (var zipStream = zipEntry.Open())
                    {
                        zipStream.Write(pdfData, 0, pdfData.Length);
                    }
                }
                return memoryStream.ToArray();
            }
        }
    }
}
