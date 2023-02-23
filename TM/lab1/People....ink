VAR SAFENESS = 10
VAR CASE = 0

-> Introduction

=== Introduction ===
    18th October 20** 11:05PM San Francisco, California State, USA, North America, Earth, Milky Way, long: -122.396257 lat: 37.795356 +4UTF
    Loud sounds of police sirens came from afar. The air was heavy with terror and shock. Yellow police tape fluttered in the breeze, marking off a house that was now a crime scene. A police officer was consoling a middle-aged couple that seemed to have lost all hope in their eyes.
   "I know it's hard for you to be rational right now, but please, I have a couple of questions to ask you. Would you be able to answer them?" - with regret in his eyes - asked the police officer as he waited for a response that never came.
   "Rational!? Are you kidding me?" yelled the woman through her sobs. "I still see her face full of fear in front of my eyes! We lost her! I can't believe she would do that to herself!"
   **    25th October 20** 8:02AM
        ->Scene_1
->END
=== Scene_1 ===
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
    The morning light spilled in through the window, casting a warm glow over the room. In the background, the television murmured with the latest headlines, but Yuna barely registered any of the phrases. She let herself fall on the couch and took a sip of espresso from her favorite cup. Suddenly, jumped up and walked to the table as her phone rang.
    "Am I speaking to detective White?" the desperate voice of a stranger asked.
    "Yes, that's me, give me a second..." - the TV was to loud to continue the conversation.
    
    **Turn off the TV
        ->Scene_1_1
    **Mute the TV
        ->Scene_1_2

->END

=== Scene_1_1 ===
    "I'm back. How can I help you?" - The tone of interest in her voice could be easily recognized.
    "My little girl... She... She was killed, but the police just won't listen to me and my wife! You need to help us - to find this monster that took our precious daughter from us! We have the money..."
    "Wait, wait" - Yuna tried to stop the desperate speech - "Can you at least tell me your name?"
    "Oh, sorry, I'm guess I'm not in my right mind" - he took a deep breath trying to calm down - "My name is mr. Smith, and as I already said, I need your help"
    "Ok, we can meet today, and you can tell me all the necessary details".
    The coffee mug was cooling down as Yuna took her trench coat in a rush and left the apartment.
->Scene_2

=== Scene_1_2 ===
    ~CASE++
    "I'm back. How can I help you?" - The tone of interest in her voice could be easily recognized.
    "My little girl... She... She was killed, but the police just won't listen to me and my wife! You need to help us - to find this monster that took our precious daughter from us! We have the money..."
    "Wait, wait" - Yuna tried to stop the desperate speech - "Can you at least tell me your name?"
    "Oh, sorry, I'm guess I'm not in my right mind" - he took a deep breath trying to calm down - "My name is mr. Smith, and as I already said, I need your help"
    "Ok, we can meet today, and you can tell me all the necessary details".
    She hang the phone and her glance stopped upon the horrifying scene that was shown on the TV: multiple photos of mutilated corpses covered in blood. Trying to understand what is going on in her city, she turned the sound on. All over the news the reporters were talking about a dreadful murderer <i>"They call him <strong>The Artist</strong>. The police was able to classify him as a serial killer by his signature - the victims were posed postmortem in sculptures-like shapes. At each scene was find a picture of his "creations". Currently, there are no active leads that would trace the criminal."</i>.
    "Ow, I gotta hurry up!" thought Yuna as the news broke for the adds. The coffee mug was cooling down as Yuna took her trench coat in a rush and left the apartment.
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
->Scene_2

=== Scene_2 ===
    25th October 20** 9:27AM; Victim's house
    "Could you please describe more detailed your daughter?"
    Yuna was engaged in a conversation with the grieving parents, Mr. and Mrs. Smiths.
    While the sunlight was filling the bright room, Yuna thought for a moment that the world didn't change and the other's lives were going on, as this family was stuck in a never-ending moment of anguish. "How unfair is that..."
    "Angy was loved by everyone. She had a dozen of kind friends, was good at her job, I can not think of at least one reason why she would..." the loving mother's voice started shaking while her eyes where filling with tears.
    "Where were you in the night of the tragedy?"
    "Oh, we visited our friends. It was Bingo night, so we stayed longer than expected."
    "When you came home, was the door opened?"
    "No, there weren't any signs of effraction. This was one of reasons the police classified it as a suicide."
    About 20 minutes later, the parents weren't able to give any more useful clues, that's why Yuna asked to take a look at the crime scene. They led Yuna to the room were the crime happened.
    After a minute of hesitation she opened the door. It seemed like a normal room in which a woman once was peaceful. Even if the hanging body wasn't there anymore, she could easily see it in front of her eyes. She inspected the room trying to find any loose ends that police couldn't observe, but it was like the police said - nothing indicated signs of murder. The image of the woeful parents surfaced in her mind. "I can't give up on them now!" - she decided to take a closer look at:
   
   {
        - CASE <= 1 :
        ->Choices
        
        - else:
        ->Scene_3
   }
   
    = Choices
    **Rug
        ->Rug_Scene
    **Window
        ->Window_Scene
    **Nightstand
        ->Nightstand_Scene

    = Rug_Scene
        At the first glance, the carpet seemed ordinary - however, Yuna decided to take a closer look in case of any blood traces. She kneeled on the carpet and took out her ultraviolet flashlight. Unfortunately, no clues were found, that's why she moved the carpet and started knocking on the floor, eager to find a hidden basement. "Nothing useful, maybe I should take a look somewhere else."
    -> Choices
    = Window_Scene
        Yuna got closer to the window. "The door was closed as the Smiths arrived home in the day of the crime, so, maybe the murderer entered through the window". Her assumption was quickly dismissed because the window was closed and it didn't have any scratch or marks.
    -> Choices
    = Nightstand_Scene
        ~CASE++
        To get to know the victim, Yuna decided to look into her nightstand. She opened the wood door, but nothing seemed unordinary: just makeup tools, some accessories and a couple of portrait pictures of Angela. "Hm, polaroid photos? Not everybody have these nowadays". Just as she was standing up, her leg accidentally hit the nightstand - it moved just a few inches, but enough to make visible and white object. "What do we have here?"
        Yuna reached for the unknown thing, and how surprised was she when she discovered another polaroid picture. The picture was still of Angela, but now, totally different - it was an image of her lifeless body posed on the carpet that Yuna was inspecting earlier. Her hands started shaking, and despite her fear, she realized that this case is now a <i>murder case</i>.
        "I need to take this to the police as soon as possible! How could they miss that?"
        {
            - CASE == 2 :
            She recalled with panic the news blog she saw this morning. "This have to be the work of <i>The Artist!</i>"
        }
        
        ** 25th October 20** 11:00AM; Police station
    -> Scene_3
-> END

=== Scene_3 ===
    Yuna entered the police station and talked to the officers that were responsible for the case before it was closed. She showed them the photo she found in the victim's house, demanding to talk to the department chief. After some minutes of convincing that she will be helpful in solving this case, they led her to the chief's office. Yuna read "W. Black" written on the name plate and knocked on the door. A deep voice invited her in.
    "How can I be helpful?"
    He had a mustache and a greyish beard. His voice was calm but the way he looked at Yuna made her slightly shiver.
    "My name is Yuna White, I'm a private detective, I came here to talk about..."
    "The Angela Smith case. My subordinate have already reported to me about your discovery."
    "Oh, fine. I wanted to offer my services and help the investigation. I can give you my number to..."
    "Wait, wait. We appreciate your proactivity, however... Uh, Yuna, let me talk honest to you. Do you even imagine how many criminals are on the loose? Do you know what effort it takes to re-open a case, especially after we stated it's a suicide?"
    "But... Her parents are destroyed! And the photo... She was definitely murdered..."
    "Look, I get that you wanna play the hero in here, and I was just like you once, but right now I have to solve some real cases, and you are standing in my way." - as he finished his phrase, the office phone rang, and Yuna had no choice but to leave defeated.
    ~SAFENESS--
    ~CASE++
    SAFENESS = {SAFENESS}
    **25th October 20** 19:43AM; Yuna's house
-> Scene_4

=== Scene_4 ===
    The unpleasant conversation with the police chief, was replaying in Yuna's head all day. Passing the room, she decided: "I can't leave it like this. I gotta go and talk to him. I don't understand, why was he denying the murder?"
    {
        - CASE == 3:
            After some time of thinking, Yuna said: "The evidence clearly shows that this is a case related to The Artist. The officer behavior is extremely suspicion"
            ~SAFENESS--
            ~CASE++
            SOLVING THE CASE = {CASE}
            
    }
    "I need to decide whether to go at the police station now, or not to bother the police chief"
    **Go to the police station
        ->Scene_4_1
    **Stay at home
        ->Scene_4_2
    


-> END

=== Scene_4_1 ===
    ~SAFENESS--
    SAFENESS = {SAFENESS}
    "I have to go back to the police station before the chief leaves it. It's already too late, so I need to hurry."
    Yuna took a taxi to arrive as quick as possible, and luckily, the police officer didn't leave yet, even though it was past the working hours. Yuna ran into the room, where she found Mr. Black sitting in the same position when she earlier spoke to him. She addressed the officer:
        **Calmly
            ->Scene_4_1_CALM
        **Aggressively
            ->Scene_4_1_AGGRESSIVELY

->END

=== Scene_4_1_CALM ===
    ~CASE++
    Mr. Black took a deep sigh when he raised his eyes and noticed who broke into the office.
    "Sir, I don't have the intention to bother you, but please, take in consideration our earlier conversation."
    "You are very persistent even if I suggested you to stay out of the case."
    After some moments of hesitation, Yuna took the courage to explain her point - "Yes, but as I already said to you, I don't understand why the case was classified as a suicide in the first place!
    {
    - CASE > 3:
        ~SAFENESS--
        SAFENESS = {SAFENESS}
        You do realize that this case is related to The Artist, right?" - as she said this phrase, Yuna noticed a terrifying glance from the officer.
        "The Artist..." - he paused for a moment - "just because all the media is talking about him right now, it doesn't mean that you need to connect every odd case to this <i>monster</i>"
        ~CASE++
        SOLVING THE CASE = {CASE}
    -else:
        "All the evidence led to this conclusion."
    }
    Yuna found his change of the intonation very disturbing. <i>"I have a feeling that I don't have to trust him"</i> - her thoughts were interrupted:
        "However, it's a late hour. You came here just as I was thinking about leaving home." - his face warped in a peculiar smile.
        "I can give you a lift if you want."
        ->Scene_4_1_CHOICE

->END

=== Scene_4_1_CHOICE ===
        **Accept
            ->Scene_4_1_ACCEPT_RIDE
        **Decline
            ->Scene_4_1_DECLINE_RIDE

->END

=== Scene_4_1_ACCEPT_RIDE ===
    ~SAFENESS--
    ~CASE++
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
->END

=== Scene_4_1_DECLINE_RIDE ===
    ~CASE--
    SOLVING THE CASE = {CASE}
    <i>"No way I'm going with him"</i> - Yuna thought, as her gut feeling told her to cautious around this man.
    "That's kind of you! But I'll better take a taxi."
    An anxious foreboding followed her all the way home, and it confirmed as soon as she got out of the car.
    "What are you doing here? And how did you know my address?"
    The panic was increasing as Yuna looked right in the eyes of Mr. Black.
    "I'm a police officer and that's not a big deal for me." - Suddenly, he smiled as his tone became more frightening - "But this is not what you should be concerned now." - some moments of terrifying silence - "Get in my car."
    ~SAFENESS--
    SAFENESS = {SAFENESS}
    Without any second thought, Yuna ran as fast as she could.

    {
        -SAFENESS < 6:
            But the strength of a young girls was clearly not enought to escape the vicious danger. She felt that he catched her, and his hands pressed a rag with chloroform onto her nose and mouth. Slowly, her eyelids became too heavy, and the darkness flooded her eyes.
            ->Scene_5
        -else:
            She felt the danger following her step by step, but the energy of a young girl was clearly more enough to outrun the middle-aged man, tired after a day of work. She was lucky to catch a taxi in her way.
            "I'll pay you double if you will ride as fast as you can!"
            "Ok lady" - said the driver as he drowe off his car - "Can you at least say me where do you need to go so urgently?"
            After a moment of hezitation she replied - "The nearest emmergency center in the city. Do you know where it can be?"
            "Sure".
            The day after, all the news were talking about a horrifying and shocking discovering. Nobody could thought that the serial killer that police was trying to catch for such a long time - <i>The Artist</i> - was himself an officer.
    }
->END


=== Scene_4_1_AGGRESSIVELY ===
    ~SAFENESS--

->END



=== Scene_4_2 ===
    Yuna was eager to find the truth about Angela's case, but the more she thought, the more irrational her behavior seemed to be. She is just a private detective with little experience, and even if her intuition was saying that this was a murder, the photo is too insignificant to re-open the case. It is impossible to say when the picture was taken and if Angela was alive or not at that moment.

    "It is really late and the chief may be at home already. And even if I find him, what could I possibly say to convince him to re-open the case? He seemed really stubborn about this, so maybe I'm just getting myself in trouble for nothing. I need to think it through."
    
    The days passed, and Yuna was trying to find other clues, but she had no luck. The police didn't re-open the case and even the parents seemed to make peace with the idea that their daughter wasn't there anymore. Yuna had no other choice than to stop her investigation, even if this case would always remain a mystery to her.
    ~SAFENESS = 10
    ~CASE =0
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
    ->DONE

->END

=== Scene_5 ===


->END


