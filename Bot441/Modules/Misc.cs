using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Discord;
using Discord.Commands;
using System.IO;

namespace Bot441.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")] //Bot repeats the user input
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed Message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("live")] //Checks to see if user is currently live on Twitch.tv
        public async Task Live([Remainder]string username)
        {
            if (username == "help"|| string.IsNullOrEmpty(username))
            {
                await Context.Channel.SendMessageAsync(Context.User.Mention + " !live Username");
            }
            else
            {
                var client = new HttpClient();
                string message = "https://www.twitch.tv/";
                client.DefaultRequestHeaders.Add("Client-ID", Config.bot.clientID);

                HttpResponseMessage response = await
                client.GetAsync("https://api.twitch.tv/helix/streams?user_login=" + username);

                HttpContent responseContent = response.Content;
                string responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Length > 30)
                {
                    await Context.Channel.SendMessageAsync(username +" is online! "+Environment.NewLine+message+username);
                }
                else
                {
                    //Console.WriteLine("OFFLINE");
                    var embed = new EmbedBuilder();
                    embed.WithTitle(username + " is Offline :(");
                    embed.WithColor(new Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed);
                }
            }
            //Console.WriteLine(responseBody);
        }
        
    }
}
