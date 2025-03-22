using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0] != "generate-ts")
        {
            Console.WriteLine("Usage: dotnet run -- generate-ts");
            return;
        }

        string swaggerUrl = @"C:\Users\Pawel\source\repos\Labirynth\web-labyrinth\Server\LabyrinthApi\swagger\v1\swagger.json";
        string outputPath = @"C:\Users\Pawel\source\repos\Labirynth\web-labyrinth\Client\maze-client\src\app\services\api\services.generated.ts";

        var arguments = $"nswag run /runtime:net9.0 /input:{swaggerUrl} /output:{outputPath} /template:angular";
        Console.WriteLine(arguments);
        Console.WriteLine("Generating TypeScript client...");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.StartInfo.RedirectStandardError = true;
        process.ErrorDataReceived += (sender, e) => Console.WriteLine("Error: " + e.Data);
        process.Start();
        process.BeginErrorReadLine();
        process.WaitForExit();

        Console.WriteLine("Finish!");
    }
}
