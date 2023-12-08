using System.IO.Compression;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// string inputFileName = "4947.json";
// string outputFileName = "4947.json.gz";

// CompressJson(inputFileName, outputFileName);

string compressedFileName = "4946.json.gz";
string decompressedFileName = "4946.json";

DecompressJson(compressedFileName, decompressedFileName);

Console.WriteLine("File compressed successfully.");

static void CompressJson(string inputFileName, string outputFileName)
{
    // Read JSON data from the input file
    string jsonData;
    using (StreamReader reader = new StreamReader(inputFileName))
    {
        jsonData = reader.ReadToEnd();
    }

    // Compress and write to the Gzip file
    using (FileStream outputFileStream = File.Create(outputFileName))
    {
        using (GZipStream gzipStream = new GZipStream(outputFileStream, CompressionMode.Compress))
        {
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            gzipStream.Write(data, 0, data.Length);
        }
    }
}

static void DecompressJson(string compressedFileName, string decompressedFileName)
{
    // Read compressed data from the Gzip file
    byte[] compressedData;
    using (FileStream compressedFileStream = File.OpenRead(compressedFileName))
    {
        using (MemoryStream decompressedMemoryStream = new MemoryStream())
        {
            using (GZipStream gzipStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(decompressedMemoryStream);
            }

            compressedData = decompressedMemoryStream.ToArray();
        }
    }

    // Write decompressed data to the output file
    using (StreamWriter writer = new StreamWriter(decompressedFileName))
    {
        writer.Write(Encoding.UTF8.GetString(compressedData));
    }
}



app.Run();