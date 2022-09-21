using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OGUBumper.Helpers
{
    internal class FileHelper
    {
        internal static string GetCookie()
        {
            return File.ReadAllText(Path.Combine("Files", "Cookies.txt"));
        }

        internal static IEnumerable<string> GetUrls()
        {
            return File.ReadAllLines(Path.Combine("Files", "Urls.txt"));
        }

        private static IEnumerable<string> Messages = new List<string>();

        private static readonly Random Random = new Random();

        internal static string GetRandomReplyMessage()
        {
            if (!Messages.Any())
                Messages = File.ReadAllLines(Path.Combine("Files", "Messages.txt"));

            return Messages.OrderBy(p => Random.Next()).First();
        }
    }
}
