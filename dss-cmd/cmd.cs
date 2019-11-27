using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSSIO;
namespace dss_cmd
{
    public class CMD
    {
        DSSReader reader;

        public CMD()
        {

        }

        public CMD(string fileName)
        {
            Open(fileName);
        }

        private void Open(string fileName)
        {
            if (reader != null)
            {
                reader.Dispose();
            }
            reader = new DSSReader(fileName);
        }

        public void Run()
        {
            Console.WriteLine("exit");
            Console.WriteLine("Open 'filename.dss'");
            Console.WriteLine("Catalog  -- list all paths");
            Console.WriteLine("print index  -- print item at index number (from catalog output)");
            Console.WriteLine("print path -- print item by path name");

            while (true)
            {
                var line = Console.ReadLine().ToLower();
                if (line == "ex")
                    break;
                var tokens = line.Split(' ');

                if (line.StartsWith("open") && tokens.Length == 2)
                    Open(tokens[1]);

                if (reader == null)
                {
                    Console.WriteLine("please open a file");
                    continue;

                }
                if (line.StartsWith("catalog"))
                {
                    Catalog();
                }

                if( line.StartsWith("print") && tokens.Length == 2)
                {
                   if( Regex.IsMatch(tokens[1],"[0-9]$"))
                    {// print item at index

                    }
                    else
                    {// print by path name

                    }

                }

            }
        }
            void Catalog()
            {
            var paths = reader.GetCatalog();
                for (int i = 0; i < paths.Count; i++)
                {
                    Console.WriteLine("["+i+"]"+  paths[i].FullPath);
                }
            }
        }
    }
