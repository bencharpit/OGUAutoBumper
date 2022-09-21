using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OGUBumper.Helpers;
using OGUBumper.Models;
using OGUBumper.Services;

namespace OGUBumper
{
    /// <summary>
    /// Auto bumper for the posts you want from any account in OGU.GG
    /// </summary>
    /// <seealso cref="FileHelper"/>
    internal class Program : FileHelper
    {
        /// <summary>
        /// <see cref="OGUService"/>
        /// </summary>
        private static OGUService oguService;

        /// <summary>
        /// URLs & WebForms details for post bumps
        /// </summary>
        /// <seealso cref="WebForm"/>
        private static List<KeyValuePair<string, WebForm>> allPostWebForm;

        /// <summary>
        /// Delay between bumps
        /// </summary>
        private static TimeSpan delayReplies;

        static async Task Main()
        {
            allPostWebForm = new List<KeyValuePair<string, WebForm>>();

            oguService = new OGUService(GetCookie());

            delayReplies = TimeSpan.FromMinutes(30);

            foreach (var Url in GetUrls())
            {
                var formInfo = await oguService.GetPostInfoAsync(Url);

                allPostWebForm.Add(new KeyValuePair<string, WebForm>(Url, formInfo));
            }

            await StartAsync();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Start bumping
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/></returns>
        protected static async Task StartAsync()
        {
            while (true)
            {
                Console.WriteLine("Bumping ..." + Environment.NewLine);

                foreach (var WebForm in allPostWebForm)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    await oguService.PostReplyAsync(WebForm.Value, GetRandomReplyMessage());

                    Console.WriteLine($"{WebForm.Key} - has been bumped [{DateTime.Now}]");
                }

                Console.WriteLine($"{Environment.NewLine}Starting {delayReplies.Minutes} minute(s) delay...");

                await Task.Delay(delayReplies);

                Console.WriteLine("Delay finished - Bumping again");
            }
        }
    }
}
