# Discord-Bot-for-Twitch
Also includes our website files. This project uses [PandaScore](https://developers.pandascore.co/) and [Discord's API](https://discordapp.com/developers/docs/intro) to post upcoming matches, stream statuses, and notifications in Discord. 

Notifications are set to check every 5 minutes by default (edit this delay in Notification.cs) and you may want to create a separate text channel in Discord if streams.txt becomes long.

## Command List

### !echo (string)
repeats anything user inputs
### !live (streamname)
checks twitch if user-input streamer is online and returns a link to that stream
### !upcoming (lol / ow / dota2)
returns 4 upcoming matches for input game
### !notification (enable)
sends a notification whenever a stream goes live*

*Edit the list of streams using streams.txt
