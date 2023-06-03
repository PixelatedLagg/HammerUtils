public class Program
{
    public static bool Setup = false;
    public static StreamWriter Writer = new StreamWriter(Stream.Null);
    public static void Main()
    {
        AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnClose);
        if (!File.Exists("data.txt"))
        {
            Setup = true;
            using (Writer = new StreamWriter("data.txt"))
            {
                Console.Write("Welcome to HammerUtils!\nAre you using the Steam version of Counter-Strike? (y/n) -> ");
                char input = Console.ReadKey().KeyChar;
                while (input != 'y' && input != 'n')
                {
                    Console.Clear();
                    Console.Write("Are you using the Steam version of Counter-Strike? (y/n) -> ");
                    input = Console.ReadKey().KeyChar;
                }
                if (input == 'y')
                {
                    Writer.WriteLine(1);
                    Console.Write("\n\nFor each of the following, provide the absolute path. If there are any spaces, be sure to use quotes.\nhlcsg.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlbsp.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlvis.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlrad.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("\\cstrike\\maps -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("steam.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hammer.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                }
                else
                {
                    Writer.WriteLine(0);
                    Console.Write("\n\nFor each of the following, provide the absolute path. If there are any spaces, be sure to use quotes.\nhlcsg.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlbsp.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlvis.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hlrad.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("\\cstrike\\maps -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("\\half-life -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                    Console.Write("hammer.exe -> ");
                    Writer.WriteLine(Console.ReadLine() ?? "");
                }
                Writer.Close();
            }
            Setup = false;
            Console.WriteLine();
        }
        Console.Write("Project Name: ");
        string name = Console.ReadLine() ?? "";
        string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{name}-Map";
        Directory.CreateDirectory(folder);
        File.Create($"{folder}\\{name}.map");
        IEnumerable<string> data = File.ReadLines("data.txt");
        if (data.ElementAt(0) == "1")
        {
            using (StreamWriter Writer = new StreamWriter($"{folder}\\{name}.bat"))
            {
                Writer.WriteLine("@echo off");
                Writer.WriteLine($"{data.ElementAt(1)} -nowadtextures {name}");
                Writer.WriteLine($"{data.ElementAt(2)} {name}");
                Writer.WriteLine($"{data.ElementAt(3)} {name}");
                Writer.WriteLine($"{data.ElementAt(4)} {name}");
                Writer.WriteLine($"copy {name}.bsp {data.ElementAt(5)}");
                Writer.WriteLine("pause");
                Writer.WriteLine($"{data.ElementAt(6)} -applaunch 10 -dev +sv_cheats 1 +map {name}");
            }
        }
        else
        {
            using (StreamWriter Writer = new StreamWriter($"{folder}\\{name}.bat"))
            {
                Writer.WriteLine("@echo off");
                Writer.WriteLine($"{data.ElementAt(1)} -nowadtextures {name}");
                Writer.WriteLine($"{data.ElementAt(2)} {name}");
                Writer.WriteLine($"{data.ElementAt(3)} {name}");
                Writer.WriteLine($"{data.ElementAt(4)} {name}");
                Writer.WriteLine($"copy {name}.bsp {data.ElementAt(5)}");
                Writer.WriteLine($"cd {data.ElementAt(6)}");
                Writer.WriteLine("pause");
                Writer.WriteLine($"hl -dev -console -game cstrike +sv_cheats 1 +map {name}");
            }
        }
        System.Diagnostics.Process.Start(data.ElementAt(7), $"-open {folder}\\{name}.map");
    }

    public static void OnClose(object? sender, EventArgs e)
    {
        if (Setup)
        {
            Writer.Close();
            File.Delete("data.txt");
        }
    }
}