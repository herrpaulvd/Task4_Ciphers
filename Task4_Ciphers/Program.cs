using System;
using System.Collections.Generic;

namespace Task4_Ciphers
{
    class Program
    {
        static void Main(string[] args)
        {
            (string, ICipher)[] ciphers =
            {
                ("atbash", new Atbash()),
                ("mark", new Mark())
            };

            List<(string, Func<string, string>)> commands = new();
            foreach(var (name, cipher) in ciphers)
            {
                commands.Add(($">{name}>", cipher.Encode));
                commands.Add(($"<{name}<", cipher.Decode));
            };

            while(true)
            {
                Console.Write("$");
                var line = Console.ReadLine();

                bool found = true;
                foreach(var (cmd, action) in commands)
                    if(line.StartsWith(cmd))
                    {
                        found = true;
                        try
                        {
                            Console.WriteLine(action(line[cmd.Length..]));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("There's an error: " + e.Message);
                        }
                    }
                if (!found)
                {
                    if (line == "q")
                        return;
                    else
                        Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
