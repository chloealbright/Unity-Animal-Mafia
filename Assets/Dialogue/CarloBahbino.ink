-> main

=== main ===
Welcome to the dungeons...
Are you ready to prove your worth to the Don?
    * [Yes, I was born ready!]
        -> Ready
    * [No, I'm not ready!]
        -> NotReady
        
=== Ready ===
Wonderful, do you need me to explain the dungeons?
    * [Yes, this is my first run]
        -> Instructions
    * [No, I've been in the dungeons before]
        -> Bye

=== NotReady ===
You're off to a baaa-d start if you're trying to impress the Don.
Oh wells, whatever floats you goat.
Come back to me when you're ready. I'll explain the dungeon run.
-> END

=== Instructions ===
There are infinite levels to the dungeon. Once you get overrun by monsters that's it.
There is no going back. No checkpoints. So try your best to prove your worth.
    * [How do I defend myself?]
        -> Gun
    * [How do I get to the next level?]
        -> Ladder

=== Gun ===
You get a gun with 8 rounds in one clip. Don't worry though you have infinite ammunition so that you can get through each level with ease.
    * [How do I get to the next level again?]
        -> Ladder
    * [Thanks for the info, I'll be on my way now]
        -> Bye

=== Ladder ===
To move on to the next level, you need to find the ladder. There is always one in each level. 
However some levels the exit is blocked by tougher opponents. You must defeat the first before moving on!
    * [How do I defend myself again?]
            -> Gun
    * [Thanks for the info, I'll be on my way now]
        -> Bye
        
=== Bye ===
I wish you the baa-st of luck. I'll be here if you ever have questions. Baa-bye!
-> END



