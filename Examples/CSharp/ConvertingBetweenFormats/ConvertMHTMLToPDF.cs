﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspose.Html.Examples.CSharp.ConvertingBetweenFormats
{
    static class ConvertMHTMLToPDF
    {
        public static void WithASingleLine()
        {
            //ExStart: WithASingleLine
            string dataDir = RunExamples.GetDataDir_Data();
            // Open an existing MHTML file for reading.
            using (var stream = System.IO.File.OpenRead(dataDir + "sample.mht"))
            {
                // Call the ConvertMHTML method to convert the MHTML to PDF.
                Aspose.Html.Converters.Converter.ConvertMHTML(stream, new Aspose.Html.Saving.PdfSaveOptions(), "output.pdf");
            }
            //ExEnd: WithASingleLine
        }

        public static void ConvertMHTMLFileToPDF()
        {
            //ExStart: ConvertMHTMLFileToPDF
            string dataDir = RunExamples.GetDataDir_Data();
            // Open an existing MHTML file for reading.
            using (var stream = System.IO.File.OpenRead(dataDir + "sample.mht"))
            {
                // Create an instance of PdfSaveOptions.
                var options = new Aspose.Html.Saving.PdfSaveOptions();

                // Call the ConvertMHTML method to convert the MHTML to PDF.
                Aspose.Html.Converters.Converter.ConvertMHTML(stream, options, "output.pdf");
            }
            //ExEnd: ConvertMHTMLFileToPDF
        }

        public static void SpecifyPdfSaveOptions()
        {
            //ExStart: SpecifyPdfSaveOptions
            string dataDir = RunExamples.GetDataDir_Data();
            // Open an existing MHTML file for reading.
            using (var stream = System.IO.File.OpenRead(dataDir + "sample.mht"))
            {
                // Create an instance of the PdfSaveOptions with a custom page-size and a background-color.
                var options = new Aspose.Html.Saving.PdfSaveOptions()
                {
                    PageSetup =
                    {
                        AnyPage = new Aspose.Html.Drawing.Page()
                        {
                            Size = new Aspose.Html.Drawing.Size(Aspose.Html.Drawing.Length.FromPixels(3000), Aspose.Html.Drawing.Length.FromPixels(1000))
                        }
                    },
                    BackgroundColor = System.Drawing.Color.AliceBlue,
                };

                // Call the ConvertMHTML method to convert the MHTML to PDF.
                Aspose.Html.Converters.Converter.ConvertMHTML(stream, options, "output.pdf");
            }
            //ExEnd: SpecifyPdfSaveOptions
        }

        public static void SpecifyCustomStreamProvider()
        {
            //ExStart: SpecifyCustomStreamProvider
            string dataDir = RunExamples.GetDataDir_Data();
            // Open an existing MHTML file for reading.
            using (var stream = System.IO.File.OpenRead(dataDir + "sample.mht"))
            {
                // Create an instance of MemoryStreamProvider
                using (var streamProvider = new MemoryStreamProvider())
                {
                    // Convert MHTML to PDF by using the MemoryStreamProvider
                    Aspose.Html.Converters.Converter.ConvertMHTML(stream, new Aspose.Html.Saving.PdfSaveOptions(), streamProvider);

                    // Get access to the memory stream that contains the resulted data
                    var memory = streamProvider.Streams.First();
                    memory.Seek(0, System.IO.SeekOrigin.Begin);

                    // Flush the result data to the output file
                    using (System.IO.FileStream fs = System.IO.File.Create("output.pdf"))
                    {
                        memory.CopyTo(fs);
                    }
                }
            }
            //ExEnd: SpecifyCustomStreamProvider
        }

        class MemoryStreamProvider : Aspose.Html.IO.ICreateStreamProvider
        {
            // List of MemoryStream objects created during the document rendering
            public List<System.IO.MemoryStream> Streams { get; } = new List<System.IO.MemoryStream>();

            public System.IO.Stream GetStream(string name, string extension)
            {
                // This method is called when the only one output stream is required, for instance for XPS, PDF or TIFF formats.
                System.IO.MemoryStream result = new System.IO.MemoryStream();
                Streams.Add(result);
                return result;
            }

            public System.IO.Stream GetStream(string name, string extension, int page)
            {
                // This method is called when the creation of multiple output streams are required. For instance during the rendering HTML to list of the image files (JPG, PNG, etc.)
                System.IO.MemoryStream result = new System.IO.MemoryStream();
                Streams.Add(result);
                return result;
            }

            public void ReleaseStream(System.IO.Stream stream)
            {
                //  Here You can release the stream filled with data and, for instance, flush it to the hard-drive
            }

            public void Dispose()
            {
                // Releasing resources
                foreach (var stream in Streams)
                    stream.Dispose();
            }
        }
    }
}
