VAR loveMeter = 0

Helvezia honey, is this you? CAN YOU HEAR ME?? #Grandma


Yes Nana I can hear you, what’s up? #Helvezia Neutral

My email isn’t working, I need your help. #Grandma

Can’t you ask Grandpa? I really don’t have time for this! #Helvezia Angry

There was an icon on my screen that I used to click but it disappeared. I need to send Ruth a picture of the scarf I knit. #Grandma

You know she has a very handsome grandson. You two would get on like a house on fire! #Grandma
    
    *[Grandma I’m not looking for love, I’m still far too young to settle down.] #Helvezia Neutral
        ->G1
    
    *[Okay Nana listen, first of all I don’t need your help finding a boyfriend, second of all I’m happily single.] #Helvezia Angry
        ~ loveMeter -= 5
        ->G2
    
    *[Why not, I’m having the worst luck with men lately.] #Helvezia Sad
        ~ loveMeter += 5
        ->G3
        
        
===G1===
-That’s what you say now but soon you’re going to be all wrinkles and creaking bones! #Grandma

 ->Continue1
===G2===
-Helvezia Maria Geneva, how dare you speak to me like that?! #Grandma

 ->Continue1

===G3===
-You need to check your attitude, young lady. #Grandma

 ->Continue1
 
 ===Continue1===
 -No matter, we shan’t be speaking about it right now. I just don’t want you to end up like aunt Linda. #Grandma

 -I know Na- #Helvezia Angry

 -She was eaten by all her cats. Do you want that Helvezia? Do you?! #Grandma
    
    *[No Nana, no I don’t.] #Helvezia Neutral
        ~ loveMeter += 5
        ->G4
    
    *[I like cats Nana, if they eat me that’s on me for slipping.] #Helvezia Angry
        ~ loveMeter -= 5
        ->G5
        
===G4===
-Alright dear, I will have to let you go now, your grandpa needs me. #Grandma

 ->DONE
===G5===
-My goodness Helvezia! This is no way to talk to your grandma! #Grandma

 ->DONE
