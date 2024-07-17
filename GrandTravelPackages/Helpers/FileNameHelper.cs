using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.Helpers
{
    public class FileNameHelper
    {
        public static string GetNameFormated(string name)
        {
            var tokens = name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }
            return string.Join("", tokens);
        }
    }
}
