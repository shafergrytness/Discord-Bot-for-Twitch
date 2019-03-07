using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Discord;
using Discord.Commands;


namespace Bot441.Modules
{
    public class Notification : ModuleBase<SocketCommandContext>
    {
        [Command("notification")] //Checks Pandascore api for upcoming games
        public async Task streamNotifications([Remainder]string enable)
        {
            DateTime[] startTimes = new DateTime[4];
            if (enable.Equals("enable", StringComparison.OrdinalIgnoreCase))
            {
                var client = new HttpClient();
                string message = "https://www.twitch.tv/";
                client.DefaultRequestHeaders.Add("Client-ID", Config.bot.clientID);

                string[] users = File.ReadAllLines("streams.txt");
                while (true) //Checks stream live after a certain period of time
                {
                    int index = 0;
                    foreach (string user in users)//cycles through the different usernames
                    {
                        Console.WriteLine("User: " + user);

                        HttpResponseMessage response = await
                        client.GetAsync("https://api.twitch.tv/helix/streams?user_login=" + user);

                        HttpContent responseContent = response.Content;
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var jsonObj = JObject.Parse(responseBody);
                        dynamic obj = jsonObj;
                       
                        if (obj.data.HasValues == false)
                        {
                            //Console.WriteLine("Object is null");
                        }
                        else
                        {
                            DateTime currTime = obj.data[0].started_at;
                            if (startTimes[index] == currTime)
                            {
                                Console.WriteLine("Already Posted");
                                index++;
                                //return;
                            }
                            else if(startTimes[index] != currTime)
                            {
                                startTimes[index] = currTime;
                                index++;
                                await ReplyAsync(user + " is online! " + Environment.NewLine + message + user);
                            }                            
                        }
                    }
                    Console.WriteLine("STILL RUNNING ");
                    await Task.Delay(300000);
                }
            }
        }
    }
}
