using System;
using System.IO;

namespace LibAPNG.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if DEBUG
            var workspace = @"C:\APNG.NET\LibAPNG.Test";
#else
            var commandLineArgs = Environment.GetCommandLineArgs();
            var workspace = commandLineArgs.Length > 1 ? commandLineArgs[1] : Path.GetDirectoryName(commandLineArgs[0]);
#endif

            var files = Directory.GetFiles(workspace, "*.png", SearchOption.TopDirectoryOnly);

            foreach(var path in files)
            {
                var apng = new APNG(path);

                if (!apng.DefaultImageIsAnimated)
                    continue;

                var directoryName = workspace + "/" + Path.GetFileNameWithoutExtension(path);

                Console.WriteLine(directoryName);

                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);

                for (var i = 0; i < apng.Frames.Length; i++)
                {
                    var frame = apng.Frames[i];
                    //var fileName = directoryName + "/" + frame.fcTLChunk.SequenceNumber + ".png";
                    var fileName = directoryName + "/" + i + ".png";
                    File.WriteAllBytes(fileName, frame.GetStream().ToArray());
                }
            }
        }
    }
}