-> main

=== main ===
What a cow-incidence meeting you here!
Did you take a look arond the farm yet?
    * [Yes, it's coming along.]
        -> Looked
    * [No]
        -> NoLooked
-> END

=== Looked === 
Wonderful, before you go want to hear a joke?
    * [Sure why not?]
        -> CowPuns
    * [No! You're going to make another pun!]
        -> Bye
=== NoLooked === 
Well wh-udder you waiting for! Go take a look around!
-> DONE

=== CowPuns ===
How do you count cows?
With a cow-culator!
    * [You totally butchered that joke!]
        -> MorePuns
    * [...]
        -> Bye
=== MorePuns ===
...
I see what you did there!
I'll see you around. Best of cluck!
-> END  

=== Bye ===
...
Not in the MOO-d huh? Maybe an-udder time.
-> END
