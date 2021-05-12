using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnrealToSource
{
    class Program
    {
        // Constants with paths to tools
        public const string TOOL_UASSET_TO_RAW = @".\Third-Party\UModelWin32\umodel.exe";
        public const string TOOL_BLENDER = @"D:\Software\Blender Foundation\Blender\blender.exe"; // TODO: Temporary, find the Blender install location in the registry

        // Constants with paths to scripts
        public const string SCRIPT_ENTRYPOINT = @".\PyScripts\__init__.py"; // TODO: Figure out how to load a package into Blender (or combine everything into a single script <- feels like cheating)

        // Dependency names (for reference):
        // Import for PSK: io_import_scene_unreal_psa_psk_280 default:True loaded:True
        // Blender Source Tools: io_scene_valvesource default:True loaded:True

        public const string PREFIX_DEPENDENCY_ERROR = "!Dependency ";
        public const string SUFFIX_DEPENDENCY_ERROR = " missing!";

        static void Main(string[] arguments)
        {
            string exeParentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string outputPath = Path.Combine(exeParentDirectory, "exports");
            /*
            if (arguments.Length == 0)
            {
                Console.WriteLine("You need to drop a .uasset file onto this program to convert it to Source Engine");
                Console.ReadKey();// TODO: Remove this input (debugging only)
                return;
            }

            // Sanity checks if our third-party tool is unavailable
            string rawToolPath = Path.Combine(exeParentDirectory, TOOL_UASSET_TO_RAW);

            if (!File.Exists(rawToolPath))
            {
                Console.WriteLine("Could not find UModel at " + rawToolPath);
                Console.WriteLine("This tool requires umodel.exe which can be found @ https://www.gildor.org/en/projects/umodel#files (Date last checked: 13-01-2020)");
                Console.ReadKey();// TODO: Remove this input (debugging only)
                return;
            }

            string path = arguments[0];

            if (Path.GetExtension(path) != ".uasset")
            {
                Console.WriteLine(path + " is not a .uasset file!");
                Console.WriteLine("The file you drop on this program needs to be a .uasset file!");
                Console.ReadKey();// TODO: Remove this input (debugging only)
                return;
            }

            // Use UModel to convert the dropped .uasset file to raw files (pskx/uncompressed tga)
            Process rawToolProcess = new Process();
            rawToolProcess.StartInfo.UseShellExecute = false;
            rawToolProcess.StartInfo.RedirectStandardOutput = true;
            rawToolProcess.StartInfo.FileName = Path.Combine(exeParentDirectory, TOOL_UASSET_TO_RAW);
            rawToolProcess.StartInfo.Arguments = string.Format("-export -notgacomp -out={1} {0} ", path, outputPath);
            rawToolProcess.Start();

            string output = rawToolProcess.StandardOutput.ReadToEnd();
            rawToolProcess.WaitForExit();

            Console.WriteLine(output);
            */
            // Import the pskx into blender
            Process blenderProcess = new Process();
            blenderProcess.StartInfo.UseShellExecute = false;
            blenderProcess.StartInfo.RedirectStandardOutput = true;
            blenderProcess.StartInfo.FileName = Path.Combine(TOOL_BLENDER);
            // -b = no gui
            // -P <python script> = execute python script
            blenderProcess.StartInfo.Arguments = string.Format("-b -P {0} ", Path.Combine(exeParentDirectory, SCRIPT_ENTRYPOINT));
            blenderProcess.Start();

            string blenderOutput;
            bool dependencyErrors = false;

            while((blenderOutput = blenderProcess.StandardOutput.ReadLine()) != null)
            {
                Console.WriteLine(blenderOutput);

                if (blenderOutput.StartsWith(PREFIX_DEPENDENCY_ERROR))
                {
                    string dependency = blenderOutput.Substring(PREFIX_DEPENDENCY_ERROR.Length, blenderOutput.Length - SUFFIX_DEPENDENCY_ERROR.Length - PREFIX_DEPENDENCY_ERROR.Length);

                    Console.WriteLine("Can't find " + dependency);

                    dependencyErrors = true;
                }
            }

            if (dependencyErrors)
            {
                Console.WriteLine("The above dependency/dependencies weren't found! This tool can't run without these two dependencies installed and loaded into Blender:");
                Console.WriteLine("1. https://github.com/Befzz/blender3d_import_psk_psa");
                Console.WriteLine("2. https://github.com/Artfunkel/BlenderSourceTools");

                Console.ReadKey();
                return;
            }

            blenderProcess.WaitForExit();

            Console.ReadKey();

            // Goal etc: See notes and progress down the list in chunks
        }
    }
}
