using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TicTacToe_AI_DataMining
{
    public class FileReader
    {
        public List<string[]> ReadFile(string url)
        {
            StreamReader reader = new StreamReader(@url);

            string line = null;
            List<string[]> values = new List<string[]>();

            while (null != (line = reader.ReadLine()))
            {
                values.Add(line.Split(','));
            }

            return values;
        }
    }
}