using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PDF_ultramerger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rikovmike PDF Ultra Merger");

            if (args.Length > 0)
            {
                Console.WriteLine(args[0]);

                FileAttributes attr = File.GetAttributes(args[0]);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string[] files = Directory.GetFiles(args[0]);
                    processFiles(files);

                }

                else
                {
                    processFiles(args);
                }



            }



        }


        static void processFiles(string[] files)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            string exportname = Path.Combine(Path.GetDirectoryName(files[0]), $"{Path.GetFileNameWithoutExtension(files[0])}-{Path.GetFileNameWithoutExtension(files[files.Length - 1])}.pdf");
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();


            // Iterate files
            foreach (string file in files)
            {

                if (Path.GetExtension(file).ToLower() == ".pdf")
                {

                    Console.WriteLine($"++ {Path.GetFileName(file)}");

                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }


                }

            }

            outputDocument.Save(exportname);

        }


    }
}
