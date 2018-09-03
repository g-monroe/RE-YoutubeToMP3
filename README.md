# This is only for Educational Purposes only!
# RE-YoutubeToMP3
Reverse Engineering Project for a .Net Program. 

# BACKGROUND:

I wanted to start cracking software, so I started with cracking .Net Applications. Going a little back to why I choose to Crack Youtube downloader because I was curious how they grabbed youtube direct video Streams(MP4, MV4, MP3, AUDIO/Video codecs, etc) directly from the googles servers. I was curious because I made own Music Youtube listen platform for myself a little over a month ago. NOW lets begin.



# INFO:

Application: YoutubeDownloader.exe

Client Coded In: C#

Server Coded In: Node.js or Php

Cracking Tools: dnSpy, Generate Key(custom generator by me)



# 3RD Party Services:

Google API - For searching and grabbing youtube video details along with searching.

Videoplex - Grabs Direct Video Urls(For download or Streaming)



# CRACKING:

The application has a startup class that returns a true or false to tell the application if he/she is "Activated/Pro". Changing that to be true off the bat and breaking of the method, is how I cracked the application itself. I didn't want to just do that I wanted all there Server protocols, API keys, literally everything that makes the video actually download as a video or an audio file. So thats what I did. 

# SERVER INFO: 

VIDEOPLEX API KEY URL: https://api.videoplex.com/getVideo?url=YOUTUBE_URL&token=TOKEN&optimized=true&excludeFields=quality,videoType,bitrate,resolution,qualityDescription,videoOnly,audioOnly,duration,metaData,uploadDate,type,websiteLogos,isAdult,website



There API Keys are listed in simple request which is:

https://youtubedownloader.com/wd/apikeys/apikeys.json



You can use these API keys for anything Google wise for YOUTUBE DATA v3
