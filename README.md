# ALT+F4 League of Legends game bypass 5s confirm
So recently me and my friend (u/Pizgamkrix) have been annoyed by the 5s confirmation popup when you alt + F4 during league of legends game. We just wanted to leave the game before the nexus animation ended, so we came up with this.
<br>
If you're not interested in story and want a solution then simply download [this](http://www.mediafire.com/file/41rctsr6uoix7jx/LoLkiller.zip/file).
<br>

## How it works?
1. Download and install program.
2. Run LoLkiller from startup menu or restart your PC
3. Test in your game!
PS: You don't need to run LoLkiller everytime you wanna play league, the program will automatically run on system startup. (You can obviously disable it tho).
How did we get here?
<br>
First we needed a way to kill League of Legend's game process and you can do it in cmd by this command:
```bat
wmic process where name="League of Legends.exe" delete
```
Now you need to bind that command with some hotkey. Windows allows that by creating a shortcut for your `.bat` file and specifying whichever shortcut key you'd like to use to execute that script.
<br>

Okay, but does it actually work in game? Unfortunately, no.
<br>

We don't wanna go into details why it doesn't work, but you can read about it online.
<br>

So it was clear that we needed a better solution since simply minimizing our game just to close it, isn't really alt + F4'ing it.
## Application that listens for keys with higher priority than LoL
Is it possible? Of course, discord or Team Speak uses such solutions to run push to talk.
So we've basically created an application that listens for alt + F4 and then it runs the command above.
Thanks for reading, hopefully it will save those 5 seconds of your life. 
## Links
- [download installer](http://www.mediafire.com/file/41rctsr6uoix7jx/LoLkiller.zip/file)
