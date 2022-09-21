using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OGUBumper.Helpers
{
    /// <summary>
    /// Helper of the files used by the application
    /// </summary>
    internal class FileHelper
    {
        /// <summary>
        /// Cookie used for each request
        /// </summary>
        /// <returns>Cookie</returns>
        internal static string GetCookie()
        {
            return File.ReadAllText(Path.Combine("Files", "Cookies.txt"));
        }

        /// <summary>
        /// Urls to bump
        /// </summary>
        /// <returns>URls to reply bumping post</returns>
        internal static IEnumerable<string> GetUrls()
        {
            return File.ReadAllLines(Path.Combine("Files", "Urls.txt"));
        }

        /// <summary>
        /// Messages for posts
        /// </summary>
        /// <seealso cref="GetRandomReplyMessage"/>
        private static IEnumerable<string> Messages = new List<string>();

        /// <summary>
        /// Random object used for get random line from message list
        /// </summary>
        /// <seealso cref="GetRandomReplyMessage"/>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Messages for posts
        /// </summary>
        /// <returns>Random reply message</returns>
        internal static string GetRandomReplyMessage()
        {
            if (!Messages.Any())
                Messages = File.ReadAllLines(Path.Combine("Files", "Messages.txt"));

            return Messages.OrderBy(p => Random.Next()).First();
        }
    }
}