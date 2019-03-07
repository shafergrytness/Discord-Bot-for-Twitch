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
using System.IO;
using Discord;
using Discord.Commands;


namespace Bot441.Modules
{
    public class Upcoming : ModuleBase<SocketCommandContext>
    {
        public WebClient webPage = new WebClient();
        public string apiUrl = "https://api.pandascore.co/";
        public string tokenSuf = ".php/?token=";

        [Command("upcoming")] //Checks Pandascore api for upcoming games
        public async Task UpcomingMatches([Remainder]string game)
        {
            if (game.Equals("lol", StringComparison.OrdinalIgnoreCase) ||
                game.Equals("league", StringComparison.OrdinalIgnoreCase))
            {
                await Context.Channel.SendMessageAsync("Upcoming Matches"+Environment.NewLine);
                Stream stream = webPage.OpenRead(apiUrl + "lol/matches/upcoming" + tokenSuf + Config.bot.pandaToken);
                StreamReader reader = new StreamReader(stream);

                JArray jObject = JArray.Parse(reader.ReadLine()) as JArray;
                dynamic objs = jObject;
                int count = 0;
                foreach (dynamic obj in objs)
                {
                    DateTime gameTime = obj.begin_at;
                    await Context.Channel.SendMessageAsync("Match: " + obj.name + Environment.NewLine +
                                                           "Event: " + obj.league.name + Environment.NewLine + 
                                                           "Time: "  + gameTime.ToLocalTime() + Environment.NewLine);
                    if (count > 2)
                        return;
                    count++;
                }
                stream.Close();
            }
            else if (game.Equals("ow", StringComparison.OrdinalIgnoreCase) ||
                     game.Equals("overwatch", StringComparison.OrdinalIgnoreCase))
            {
                await Context.Channel.SendMessageAsync("Upcoming Matches" + Environment.NewLine);
                Stream stream = webPage.OpenRead(apiUrl + "ow/matches/upcoming" + tokenSuf + Config.bot.pandaToken);
                StreamReader reader = new StreamReader(stream);

                JArray jObject = JArray.Parse(reader.ReadLine()) as JArray;
                dynamic objs = jObject;
                int count = 0;
                foreach (dynamic obj in objs)
                {
                    DateTime gameTime = obj.begin_at;
                    await Context.Channel.SendMessageAsync("Match: " + obj.name + Environment.NewLine +
                                                           "Event: " + obj.league.slug + Environment.NewLine +
                                                           "Time: " + gameTime.ToLocalTime() + Environment.NewLine);
                    if (count > 2)
                        return;
                    count++;
                }
                stream.Close();
            }
            else if (game.Equals("dota", StringComparison.OrdinalIgnoreCase) ||
                     game.Equals("dota2", StringComparison.OrdinalIgnoreCase))
            {
                await Context.Channel.SendMessageAsync("Upcoming Matches" + Environment.NewLine);
                Stream stream = webPage.OpenRead(apiUrl + "dota2/matches/upcoming" + tokenSuf + Config.bot.pandaToken);
                StreamReader reader = new StreamReader(stream);

                JArray jObject = JArray.Parse(reader.ReadLine()) as JArray;
                dynamic objs = jObject;
                int count = 0;
                foreach (dynamic obj in objs)
                {
                    DateTime gameTime = obj.begin_at;
                    await Context.Channel.SendMessageAsync("Match: " + obj.name + Environment.NewLine +
                                                           "Event: " + obj.serie.name + Environment.NewLine +
                                                           "Time: " + gameTime.ToLocalTime() + Environment.NewLine);
                    if (count > 2)
                        return;
                    count++;
                }
                stream.Close();
            }
            else
            {
                await Context.Channel.SendMessageAsync("Invalid! !upcoming lol, ow, dota");
            }
        }
    }
}
