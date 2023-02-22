VAR SAFENESS = 10
VAR CASE = 0

-> Introduction

=== Introduction ===
    18th Octomber 20** 11:05PM San Francisco, California State, USA, North America, Earth, Milky Way, long: -122.396257 lat: 37.795356 +4UTF
    Loud sounds of police sirens came from afar. The air was heavy with terror and shock. Yellow police tape fluttered in the breeze, marking off a house that was now a crime scene. A police officer was consoling a middle-aged couple that seemed to have lost all hope in their eyes.
   "I know it's hard for you to be rational right now, but please, I have a couple of questions to ask you. Would you be able to answer them?" - with regret in his eyes - asked the police officer as he waited for a response that never came.
   "Rational!? Are you kidding me?" yelled the woman through her sobs. "I still see her face full of fear in front of my eyes! We lost her! I can't believe she would do that to herself!"
   **    25th Octomber 20** 8:02AM
        ->Scene_1
->END
=== Scene_1 ===
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
    The morning light spilled in through the window, casting a warm glow over the room. In the background, the television murmured with the latest headlines, but Yuna barely registered any of the phrases. She let herself fall on the couch and took a sip of esspresso from her favorite cup. Suddenly, jumped up and walked to the table as her phone rang.
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
    "Ok, we can meet today, and you can tell me all the necesarry detailes".
    The coffee mug was cooling down as Yuna took her tranch coat in a rush and left the apartment.
->Scene_2

=== Scene_1_2 ===
    ~CASE++
    "I'm back. How can I help you?" - The tone of interest in her voice could be easily recognized.
    "My little girl... She... She was killed, but the police just won't listen to me and my wife! You need to help us - to find this monster that took our precious daughter from us! We have the money..."
    "Wait, wait" - Yuna tried to stop the desperate speech - "Can you at least tell me your name?"
    "Oh, sorry, I'm guess I'm not in my right mind" - he took a deep breath trying to calm down - "My name is mr. Smith, and as I already said, I need your help"
    "Ok, we can meet today, and you can tell me all the necesarry detailes".
    She hang the phone and her glance stoped upon the horrifying scene that was shown on the TV: multiple photos of mutilated corpses covered in blood. Trying to understand what is going on in her city, she turned the sound on. All over the news the reporters were talking about a dreadful murderer <i>"They call him <strong>The Artist</strong>. The police was able to classify him as a serial killer by his signature - the victims were posed postmortum in sculptures-like shapes. At each scene was find a picture of his "creations". Currently, there are no active leads that would trace the criminal."</i>.
    "Ow, I gotta hurry up!" thought Yuna as the news broke for the adds. The coffee mug was cooling down as Yuna took her tranch coat in a rush and left the apartment.
    SAFENESS = {SAFENESS}
    SOLVING THE CASE = {CASE}
->Scene_2

=== Scene_2 ===
    25th Octomber 20** 9:27AM; Victim's house
    "Could you please describe more detailed your daughter?"
    Yuna was engaged in a conversation with the grieving parents, Mr. and Mrs. Smiths.
    While the sunlight was filling the bright room, Yuna thought for a moment that the world didn't change and the other's lives were going on, as this family was stuck in a never-ending moment of anguish. "How unfair is that..."
    "Angy was loved by everyone. She had a dozen of kind friends, was good at her joob, I can not think of at least one reason why she would..." the loving mother's voice started shaking while her eyes where filling with tears.
    "Where were you in the night of the tragedy?"
    "Oh, we visited our friends. It was Bingo night, so we stayed longer than expected."
    "When you came home, was the door opened?"
    "No, there weren't any signs of effraction. This was one of reasones the police classified it as a suicide."
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
        Yuna got closer to the window. "The door was closed as the Smiths arrived home in the day of the crime, so, maybe the murderer entered through the window". Her assumption was quickly dismised because the window was closed and it didn't have any scratch or marks.
    -> Choices
    = Nightstand_Scene
        ~CASE++
        To get to know the victim, Yuna decided to look into her nightstand. She opened the wood door, but nothing seemed unordinary: just makeup tools, some accessories and a couple of portrait pictures of Angela. "Hm, palaroid photos? Not everybody have these nowadays". Just as she was standing up, her leg accidentally hit the nightstand - it moved just a few inches, but enough to make visible and white object. "What do we have here?"
        Yuna reached for the unknown thing, and how surprised was she when she discovered another palaroid picture. The picture was still of Angela, but now, totaly different - it was an image of her lifeless body posed on the carpet that Yuna was inspecting earlier. Her hands started shaking, and despite her fear, she realized that this case is now a <i>murder case</i>.
        "I need to take this to the police as soon as possible! How could they miss that?"
        {
            - CASE == 2 :
            She recalled with panic the news blog she saw this morning. "This have to be the work of <i>The Artist!</i>"
        }
        
        ** 25th Octomber 20** 11:00AM; Police station
    -> Scene_3
-> END

=== Scene_3 ===
    Yuna entered the police station and talked to the officers that were responsible for the case before it was closed. She showed them the photo she found in the victim's house, demanding to talk to the department chief. After some minutes of convincing that she will be helpfull in solving this case, they led her to the chief's office. Yuna read "W. Black" written on the name plate and knocked on the door. A deep voice invited her in.
    "How can I be helpful?"
    He had a mustache and a greish beard. His voice was calm but the way he looked at Yuna made her slightly shiver.
    "My name is Yuna White, I'm a private detective, I came here to talk about..."
    "The Angela Smith case. My subordinate have already reported to me about your discovery."
    "Oh, fine. I wanted to offer my services and help the investigation. I can give you my number to..."
    "Wait, wait. We appreciate your proactivity, however... Uh, Yuna, let me talk honest to you. Do you even imagine how many criminals are on the loose? Do you know what effort it takes to re-open a case, especially after we stated it's a suicide?"
    "But... Her parents are destroied! And the photo... She was definitely murdered..."
    "Look, I get that you wanna play the hero in here, and I was just like you once, but right now I have to solve some real cases, and you are standing in my way." - as he finished his phrase, the office phone rang, and Yuna had no choice but to leave defeated.
    ~SAFENESS--
    ~CASE++
    SAFENESS = {SAFENESS}
-> Scene_4

=== Scene_4 ===
    ** 25th Octomber 20** 19:43AM; Yuna's house
    The unpleasant conversation with the police chief, was replaying in Yuna's head all day. Passing the room, she decided: "I can't leave it like this. I gotta go and talk to him. I don't understand, why was he denying the evidence?"

-> END






