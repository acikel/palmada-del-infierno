VAR loveMeter = 0

Helvetia honey, is this you? CAN YOU HEAR ME?? #Phone


-Yes Nana I can hear you, what’s up? #Helvetia Neutral

-My email isn’t working, I need your help. #Phone

-Can’t you ask Grandpa? I really don’t have time for this! #Helvetia Angry

-There was an icon on my screen that I used to click but it disappeared. I need to send Ruth a picture of the scarf I knit. #Phone

-You know she has a very handsome grandson. You two would get on like a house on fire! #Phone
    
    *[Grandma I’m not looking for love, I’m still far too young to settle down.] #Helvetia Neutral
        ->G1
    
    *[Okay Nana listen, first of all I don’t need your help finding a boyfriend, second of all I’m happily single.] #Helvetia Angry
        ~ loveMeter -= 5
        ->G2
    
    *[Why not, I’m having the worst luck with men lately.] #Helvetia Sad
        ~ loveMeter += 5
        ->G3
        
        
===G1===
-That’s what you say now but soon you’re going to be all wrinkles and creaking bones! #Phone

 ->Continue1
===G2===
-Helvetia Maria Geneva, how dare you speak to me like that?! #Phone

 ->Continue1

===G3===
-You need to check your attitude, young lady. #Phone

 ->Continue1
 
 ===Continue1===
 -No matter, we shan’t be speaking about it right now. I just don’t want you to end up like aunt Linda. #Phone

 -I know Na- #Helvetia Angry
 
 -She was eaten by all her cats. Do you want that Helvetia? Do you?! #Phone
    
    *[No Nana, no I don’t.] #Helvetia Neutral
        ~ loveMeter += 5
        ->G4
    
    *[I like cats Nana, if they eat me that’s on me for slipping.] #Helvetia Angry
        ~ loveMeter -= 5
        ->G5
        
===G4===
-Alright dear, I will have to let you go now, your grandpa needs me. #Phone

 ->DONE
===G5===
-My goodness Helvetia! This is no way to talk to your grandma! #Phone

 ->DONE
 
