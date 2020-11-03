# ALT+F4 League of Legends game bypass 5s confirm
So recently I've been annoyed by the 5s confirmation dialog when you alt + F4 during league of legends game. So me and my friend came up with a simple solution.
If you're not interested in story and want a solution then simply download this.
## .bat script
First we needed a way to kill League of Legend's game process and you can do it in cmd by this command:
wmic process where name="League of Legends.exe" delete
Now you need to bind that command with some hotkey. Windows allows that by creating a shortcut for your `.bat` file and specifying whichever shortcut key you'd like to use to execute that script.
Ok, but does it actually work in game? Unfortunately, no.
I don't wanna go into details why it doesn't work, but you can read about that online.
So it was clear that we needed a better solution since simply minimizing our game just to close it, isn't really alt + F4'ing it.
## Application that listens for keys with higher priority than LoL
Is it possible? Of course, discord or Team Speak uses such solutions to run push to talk.
So we've basically created an application that listens for alt + F4 and then it runs the above command.
## Links
- installer
- github.com/ - the code is open source so feel free to use it any way you'd like to, stars are very much appreciated and if you run into any issues or you'd like to see something improve then feel free to file an issue there.
## Current issues
1. While killing other windows with alt + F4 it also kills league of legend's game - this can hopefully be easily fixed by checking for current focused window so.
