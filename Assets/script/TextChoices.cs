using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChoices : MonoBehaviour
{
	
	string currentNode = "Title";
	public Text titleText;
	//	public Text timerText;
	public Text clockCheck;
	public Text decisionTimerText;
	public Text decisionTimerText2;
	//	float timeRemaining = 6;
	float introTime = 20;
	float decisionTime = 15;
	//	float decisionTime2 = 25;
	//	bool timerOn = false;
	bool introDone = false;
	bool gun = false;
	bool papers = false;
	bool glove = false;
	bool radio = false;
	bool inCar = true;
		
	public AudioSource heartbeat;
	public AudioSource heartbeat2;
	public AudioSource radioStatic;
	public AudioSource radioStatic2;
	public AudioSource radioBeep;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		string textBuffer = "";
		string headerBuffer = "";
		string endText = "";

		if (currentNode == "Title") {
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n\n<size=12><color=#C6CCCCFF>Please adjust your sound so you can hear the heartbeat\n\n[ENTER] to start</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				currentNode = "Car";
				heartbeat.Stop ();
			}
		} else if (currentNode == "Car") {
			introTime -= Time.deltaTime;
			headerBuffer += "it's late...";
			textBuffer += "You've just returned to your car after responding to the latest call: a baby with breathing problems. Luckily, you come from a big family with lots of little ones, so you knew this was just a summer cold - nothing to worry about. \n\nIt's nice helping out a couple of concerned parents.\n\n[L]ook around";

			//timer moved story forward
			if (introTime < 0) {
				currentNode = "Car2";
				introTime = 90;
			}

			//key press moves story forward
			if (Input.GetKeyDown (KeyCode.L)) {
				currentNode = "Car2";
				introTime = 90;
			}

		} else if (currentNode == "Car2") {
			//all the text
			headerBuffer += "your vehicle's a little worn...";
			textBuffer += "Not just worn - also messy at the moment - but you kind of like the way it reflects your mood as of late. \n\nYou whistle as you take in the papers scattered across the passenger's seat, the police radio dangling, and your police issued Glock - all within reaching distance.";
			textBuffer += "\n\nYou lean over to\nPick up the [P]apers";
			textBuffer += "\nPush the [R]adio button";
			textBuffer += "\nExamine your [G]un";

			//intro timer function
			introTime -= Time.deltaTime;
			if (introTime < 0) {
				currentNode = "Intro1";
			}

			//key presses
			if (Input.GetKeyDown (KeyCode.P)) {
				currentNode = "Papers";
			} else if (Input.GetKeyDown (KeyCode.R)) {
				currentNode = "Radio";
			} else if (Input.GetKeyDown (KeyCode.G)) {
				currentNode = "Glove";
			}

		} else if (currentNode == "Papers") {
			papers = true;

			if (introDone == false) {
				//text
				headerBuffer += "you grab the papers next to you...";
				textBuffer += "Might be nice to have some semblance of order, you think as you pick up the scattered pieces of paper.";
				textBuffer += "\n\nUnder a couple of reports you find the commendation you got from the Police Department for taking down that drug dealer. God, you still remember how good it felt to finally get some recognition from the higher ups.";
				textBuffer += "\n\nYou put them back down next to you in a neat stack and lean over to...\nPush the [R]adio button";
				textBuffer += "\nExamine your [G]un";
			} else {
				//text
				headerBuffer += "you glance over at the papers...";
				textBuffer += "You push them onto the passenger seat floor. You wait...";
				if (gun == false) {
					textBuffer += "\n\nYou take this time to examine your \n[G]un";
				}
			}

			introTime -= Time.deltaTime;
			if (introTime < 0) {
				if (introDone == false) {
					currentNode = "Intro1";
					introTime = 20;
				} else {
					currentNode = "Call2";
					decisionTime = 10;
				}
			} 

			//key presses
			if (Input.GetKeyDown (KeyCode.R)) {
				currentNode = "Radio";
			} else if (Input.GetKeyDown (KeyCode.G)) {
				if (glove == false) {
					currentNode = "Glove";
				} else {
					currentNode = "EmptyGlove";
				}
			}
			
		} else if (currentNode == "Radio") {
			radio = true;
			radioStatic.Play ();
			//text
			headerBuffer += "you peer at the radio...";
			textBuffer += "There's nothing but silence punctuated by an almost inaudible hiss. Sometimes the waiting is peaceful, but tonight you feel antsy, electrified.";
			textBuffer += "\n\nWhat to do? Your hands move automatically to...";
			if (papers == false) {
				textBuffer += "\nPick up the [P]apers next to you";
				textBuffer += "\nExamine your [G]un";
			} else {
				textBuffer += "\nExamine your [G]un";
			}
			
			introTime -= Time.deltaTime;
			if (introTime < 0) {
				currentNode = "Intro1";
				introTime = 30;
			}

			//key presses
			if (Input.GetKeyDown (KeyCode.P) && papers == false) {
				radioStatic.Stop ();
				currentNode = "Papers";
			} else if (Input.GetKeyDown (KeyCode.G)) {
				if (glove == false) {
					radioStatic.Stop ();
					currentNode = "Glove";
				} else {
					radioStatic.Stop ();
					currentNode = "EmptyGlove";
				}
			}
			
		} else if (currentNode == "Glove") {
			gun = true;
			glove = true;

			//text
			headerBuffer += "you pick the Glock up by the grip...";
			textBuffer += "There's a heft, a solidness to it that's comforting.";
			textBuffer += "\n\nYou put the gun a little closer to you. What now?";
			if (introDone == false) {
				if (papers == false) {
					textBuffer += "\n\nYou reach to...\nPick up the [P]apers";
					textBuffer += "\nPress the [R]adio button";
				} else {
					textBuffer += "\n\nYou reach to...\nPick up the [R]adio";
				}
			} else {
				textBuffer += "\n\nYou wait...\n\n...";
			}

			introTime -= Time.deltaTime;
			if (introTime < 0) {
				if (introDone == false) {
					currentNode = "Intro1";
					introTime = 30;
				} else {
					currentNode = "Call2";
					decisionTime = 10;
				}

			}

			//key presses
			if (Input.GetKeyDown (KeyCode.P) && papers == false) {
				currentNode = "Papers";
			} else if (Input.GetKeyDown (KeyCode.R)) {
				currentNode = "Radio";
			}
			
		} else if (currentNode == "EmptyGlove") {
			
			//text
			headerBuffer += "you absently pick up the gun again...";
			textBuffer += "For whatever reason, your mind wander to the last time you played a first-person shooter video game.\n\nIt's been a while and, if you're completely honest, it freaked you out when the controller triggers started to feel wrong - too plastic and light.\n\nShaking your head, you turn to...";
			if (papers == false) {
				textBuffer += "\nPick up the [P]apers";
				textBuffer += "\nPres the [R]adio button";
			} else {
				textBuffer += "\nPress the [R]adio button";
			}
			
			introTime -= Time.deltaTime;
			if (introTime < 0) {
				currentNode = "Intro1";
				introTime = 30;
			}
			
			//key presses
			if (Input.GetKeyDown (KeyCode.P) && papers == false) {
				currentNode = "Papers";
			} else if (Input.GetKeyDown (KeyCode.R)) {
				if (radio == false) {
					currentNode = "Radio";
				} else {
					radioBeep.Play ();
					radioStatic2.Play ();
					currentNode = "Intro1";
					introTime = 30;
				}
			}
		
			//**********CHAPTER 1**************//
		} else if (currentNode == "Intro1") {
			headerBuffer += "the radio interrupts your search...";

			textBuffer += "<i>Reporting a possible stealing in progress at the Market on 9101 West Florissant. Suspect is reported stealing a box of Swedish cigars. Possible physical assault on store clerk. Suspect is a black male wearing a white t-shirt accompanied by another black male.</i>";
			textBuffer += "\n\nYou...\n[L] to listen in closer";

			if (Input.GetKeyDown (KeyCode.L)) {
				heartbeat.Play ();
				ResetHeartbeat ();
				currentNode = "Intro2";
				decisionTime = 20;
			}
		} else if (currentNode == "Intro2") {
			//intro is done after this
			introDone = true;

			//heartbeat gets louder
			SpeedHeartbeat ();

			//text
			headerBuffer += "you listen closer...";
			textBuffer += "<i>Robbery suspect sighted traveling east on Canfield from West Florissant. Suspect is a black male wearing a red St. Louis Cardinals hat, a white T-shirt, yellow socks, and khaki shorts. Suspect is accompanied by second black male. \n\nUnits 25 and 22 requesting backup to search the area.</i>";
			textBuffer += "\n\nYou...\n[I]gnore the request";
			textBuffer += "\n[O]ffer assistance";

			if (Input.GetKeyDown (KeyCode.I)) {
				ResetHeartbeat ();
				currentNode = "Ignore";
				decisionTime = 20;
			} else if (Input.GetKeyDown (KeyCode.O)) {
				ResetHeartbeat ();
				radioBeep.Play ();
//				radioStatic2.Play ();
				currentNode = "Offer";
				introTime = 12;
			}

			//decision timer
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Ignore";
				decisionTime = 17;
			}
			
		} else if (currentNode == "Ignore") {
			//heartbeat gets louder
			SpeedHeartbeat ();

			headerBuffer += "you ignore the request...";
			textBuffer += "<i>Suspects have disappeared,</i> a staticky voice proclaims defeat.";
			textBuffer += "\n\nBut there...uphead ahead you see a flash of red. A Cardinals hat...the suspect...and he’s not a small dude. It all comes into focus. He’s walking right down the middle of the road with a friend with dreadlocks that hang to his shoulders and a white wifebeater. The two of them together just <i>look</i> like trouble.";
			//Like the kind of trouble you see all too often around here - a couple of smart kids with way too much anger at the world.";
			textBuffer += "\n\nYou...\n[C]all for backup";
			textBuffer += "\n[A]pproach suspects in your car";
			textBuffer += "\n[S]top car and approach suspects on foot\n\n";

			if (Input.GetKeyDown (KeyCode.C)) {
				ResetHeartbeat ();
				currentNode = "Call";
				introTime = 10;
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.A)) {
				ResetHeartbeat ();
				currentNode = "Approach";
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				ResetHeartbeat ();
				currentNode = "StopOut";
				decisionTime = 15;
			}

			//decision timer
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				currentNode = "Call2";
				decisionTime = 10;
			}
			
		} else if (currentNode == "Offer") {

			headerBuffer += "You press the button...";
			textBuffer += "You speak into the police radio: \n<i>25 and 22. I’m back in service. Heading down Canfield Drive</i>.";
			textBuffer += "\n\n(static crackles) <i>Suspects have disappeared.</i>";
			textBuffer += "\n\nOh well, maybe one more loop around and then back to the station. Paperwork awaits. Your bed awaits.\n\n...";

			introTime -= Time.deltaTime;
			if (introTime < 0) {
				ResetHeartbeat ();
				currentNode = "Offer2";
				decisionTime = 20;
			}
			
		} else if (currentNode == "Offer2") {
			headerBuffer += "and then you see them...";
			textBuffer += "They're walking straight down the middle of the street as if they hadn’t a care in the world. One is fit with wiry black dreadlocks that barely brush the top of his white wife-beater. The other...the other black guy fits the suspect’s description exactly - Cardinal cap and all. You sigh. When will kids here stop doing stupid shit like this? When will they stop thinking they’ll get away with it?";
			textBuffer += "\n\nYou...\n[C]all for backup";
			textBuffer += "\n[A]pproach the suspects in your car";
			textBuffer += "\n[S]top the car and approach on foot";

			if (Input.GetKeyDown (KeyCode.C)) {
				ResetHeartbeat ();
				currentNode = "Call";
				introTime = 10;
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.A)) {
				ResetHeartbeat ();
				currentNode = "Approach";
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				ResetHeartbeat ();
				currentNode = "StopOut";
				decisionTime = 15;
			}

			SpeedHeartbeat ();

			//decision timer
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Call";
				introTime = 10;
				decisionTime = 15;
			}
			
		} else if (currentNode == "Call") {

			headerBuffer += "you call for backup...";
			textBuffer += "You can never really be too careful in this part of town...there’s definitely no shame in calling for some reinforcements, right? \n\nYou radio into the dispatcher: <i>21. Put me on Canfield with two. And send me another car.</i> \n\nTime stretches as you wait for a response...\n\n...";

			if (gun == false) {
				textBuffer += "\n\nYou keep one eye on the suspects as you reach to...";
				if (papers == false) {
					textBuffer += "\nPick up the [P]apers";
					textBuffer += "\nReach for your [G]un";
				} else {
					textBuffer += "\nReach for your [G]un";
				}
				
			}

			//key presses
			if (Input.GetKeyDown (KeyCode.P) && papers == false) {
				currentNode = "Papers";
				introTime = 10;
			} else if (Input.GetKeyDown (KeyCode.G) && gun == false) {
				currentNode = "Glove";
				introTime = 10;
			}

			//invisible timer
			introTime -= Time.deltaTime;
			if (introTime < 0) {
				ResetHeartbeat ();
				currentNode = "Call2";
				decisionTime = 10;
			}

		} else if (currentNode == "Call2") {
			headerBuffer += "Your muscles feel tense...";
			textBuffer += "You see the two men exchange a few nervous glances. They look ready to bolt. \n\nYou...";
			textBuffer += "\n[A]pproach the suspects in your car";
			textBuffer += "\n[S]top the car and approach on foot";

			if (Input.GetKeyDown (KeyCode.A)) {
				ResetHeartbeat ();
				currentNode = "Approach";
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				ResetHeartbeat ();
				currentNode = "StopOut";
				decisionTime = 15;
			}			

			SpeedHeartbeat ();
			//decision timer
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Run";
				decisionTime = 5;
			}

		} else if (currentNode == "Run") {
			SpeedHeartbeat ();

			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				heartbeat.Stop ();
				currentNode = "Escape";
			}

			headerBuffer += "You're too slow...";
			textBuffer += "With a final furtive glance at one another, the two boys take off running, ducking into an alley. You barely have time to react...\n\nYou...";
			textBuffer += "\n[C]hase them";
			textBuffer += "\n[Y]ell for them to stop";

			if (Input.GetKeyDown (KeyCode.C)) {
				ResetHeartbeat ();
				currentNode = "Chase";
				decisionTime = 5;
			} else if (Input.GetKeyDown (KeyCode.Y)) {
				ResetHeartbeat ();
				currentNode = "Yell";
				decisionTime = 6;
			}
			
		} else if (currentNode == "Chase") {
			headerBuffer += "You throw yourself out of the car...";
			if (gun == true) {
				textBuffer += "<i>Stop. Police.</i> you yell. As you do, one of the suspects turns. Your eyes flash to his hands, which are moving way to fast...";
				textBuffer += "\n\nYou...\n\n[S]hoot";
				textBuffer += "\n[T]ell him to freeze";

				if (Input.GetKeyDown (KeyCode.S)) {
					heartbeat.Stop ();
					currentNode = "SuspectDead";
				} else if (Input.GetKeyDown (KeyCode.T)) {
					ResetHeartbeat ();
					currentNode = "Put";
					introTime = 4;
					decisionTime = 15;
				}

			} else {
				textBuffer += "You're about to sprint after them, but you stop short when you realize that something feels wrong. Your gun. Where is it?\n\nYou scramble back into the car, reaching for your Glock...";
				textBuffer += "\n\n...even though you know it's too late.";
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				heartbeat.Stop ();
				currentNode = "Escape";
			}

		} else if (currentNode == "Yell") {
			headerBuffer += "You yell ...";
			if (gun == true) {
				textBuffer += "<i>Stop. Police.</i> As you draw even with the suspects, one of them turns. Your eyes flash to his hands, which are moving way to fast...";
				textBuffer += "You...\n\n[S]hoot";
				textBuffer += "\n[T]ell him to freeze";
				
				if (Input.GetKeyDown (KeyCode.S)) {
					heartbeat.Stop ();
					currentNode = "SuspectDead";
				} else if (Input.GetKeyDown (KeyCode.T)) {
					ResetHeartbeat ();
					currentNode = "Put";
					introTime = 4;
					decisionTime = 15;
				}
				
			} else {
				textBuffer += "<i>Stop. Police.</i> As you draw even with the suspects, one of them turns. Your eyes flash to his hands, which are moving way to fast. You reach for your...where is your gun?";
				textBuffer += "\n\nYou scramble for your gun, but it's too late...";
				endText += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>When you look up again, they've ducked into an alley. You lost them.\n\n[ENTER] to restart</color></size>";
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				heartbeat.Stop ();
				currentNode = "YouUnconscious";
			}

		} else if (currentNode == "Approach") {
			headerBuffer += "you pull up next to the suspects...";
			textBuffer += "<i>You need to move off the street and onto the sidewalk,</i> you say. There's a brief moment when time seems to hang, suspended. \n\nThe two look at each other then continue straight ahead - down the fucking middle of the street. As if you didn't exist.";
			textBuffer += "\n\nYou drive ahead of them and...\n[S]top the car";
				
			if (Input.GetKeyDown (KeyCode.S)) {
				ResetHeartbeat ();
				currentNode = "Stop";
				decisionTime = 15;
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Run";
				decisionTime = 5;
			}
		
			//in car
		} else if (currentNode == "Stop") {
			headerBuffer += "you stop the car...";
			textBuffer += "You're just ahead of the two, so they have no choice but to engage. The one in the Cardinal hat, the theft suspect, takes a step toward you.";
			textBuffer += "\n\n<i>We're almost to our destination,</i> are the words coming out of his mouth. <i>Fuck you,</i> his eyes say.";
			textBuffer += "\n\nTell them...";
			textBuffer += "\n<i>[P]ut your hands up</i>";
			textBuffer += "\n<i>[C]ome here</i>";

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Run";
				decisionTime = 5;
			}
			
			if (Input.GetKeyDown (KeyCode.P)) {
				ResetHeartbeat ();
				currentNode = "Put";
				introTime = 4;
				decisionTime = 15;
			} else if (Input.GetKeyDown (KeyCode.C)) {
				ResetHeartbeat ();
				currentNode = "Come";
				decisionTime = 10;
			}
			
			//out of car
		} else if (currentNode == "StopOut") {
			inCar = false;
			headerBuffer += "you stop the car...";
			if (gun == true) {
				textBuffer += "<i>You need to get out of the street and onto the sidewalk,</i> you say firmly as you walk up to them.";
				textBuffer += "\n\n<i>We're almost to our destination,</i> says the guy with the dreads.\n\n<i>Fuck what you have to say,</i> his friend mutters under his breath.";

				textBuffer += "\n\nTell them...";
				textBuffer += "\n<i>[P]ut your hands on your head and get down</i>";
				textBuffer += "\n<i>[C]ome here</i>";

				if (Input.GetKeyDown (KeyCode.P)) {
					ResetHeartbeat ();
					currentNode = "Put";
					introTime = 4;
				} else if (Input.GetKeyDown (KeyCode.C)) {
					ResetHeartbeat ();
					currentNode = "Come";
					decisionTime = 10;
				}

				SpeedHeartbeat ();
				//decision timer
				decisionTime -= Time.deltaTime;
				if (decisionTime < 0) {
					ResetHeartbeat ();
					currentNode = "Aggression";
					decisionTime = 4;
				}
			} else {
				textBuffer += "You've got one foot out the door, but you stop short when you realize that something feels wrong. Your gun. Where is it?\n\nYou duck back into the car and pull out your Glock...";
				gun = true;
				SpeedHeartbeat ();
				//decision timer
				decisionTime -= Time.deltaTime;
				if (decisionTime < 0) {
					ResetHeartbeat ();
					currentNode = "Run";
					decisionTime = 5;
				}
			}
		} else if (currentNode == "Put") {

			headerBuffer += "<i>Put your hands up...</i>";
			if (inCar == false) {
				//stuff happens out of the car
				textBuffer += "<i>Get on the ground,</i> your voice is steady. You're in control of this situation.\n\n...";
			} else {
				//stuff happens in the car
				textBuffer += "<i>Get on the ground,</i>The two men hesitate. You get out of the car.\n\n...";
				inCar = false;
			}

			introTime -= Time.deltaTime;
			if (introTime < 0) {
				ResetHeartbeat ();
				currentNode = "Put2";
				decisionTime = 13;
			}
		} else if (currentNode == "Put2") {
			headerBuffer += "<i>The two men comply...</i>";
			if (inCar == false) {
				//stuff happens out of the car
				textBuffer += "But one of them - the one with the Cardinal hat - stops partway. Crouched, he stares you down.";
				textBuffer += "\n\n<i>Fuckin' pussy. You ain't got the balls to do shit.</i>\n\nSpit lands hot on your uniform. He starts to get up.";
				textBuffer += "\n\nYou...\n[S]hoot";
				textBuffer += "\n[P]ush him back down hard";

			} else {
				//stuff happens in the car
				textBuffer += "But one of them - the one with the Cardinal hat - stops partway. Crouched, he stares you down.";
				textBuffer += "\n\n<i>Fuckin' pussy. You ain't got the balls to do shit.</i>\n\nSpit lands hot on your uniform. He starts to get up.";
				textBuffer += "\n\nYou...\n[S]hoot";
				textBuffer += "\n[P]ush him back down hard";
				
				inCar = false;
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				currentNode = "SuspectDead";
			} else if (Input.GetKeyDown (KeyCode.P)) {
				currentNode = "Reprimanded";
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				currentNode = "BackupArrives";
			}
		} else if (currentNode == "Come") {
			headerBuffer += "<i>Come here...</i>";
			if (inCar == false) {
				//stuff happens out of the car
				textBuffer += "The dude in the Cardinal hat walks towards you. His stance is aggressive.\n\n<i>What the fuck you gonna do?</i> he challenges.";
				textBuffer += "\n\nIn this instant you feel your own vulnerability.";
				if (gun == true) {
					textBuffer += "\n\nYou...\n[D]raw your gun";
				} else {
					textBuffer += "\n\nYou reach for your gun...but your hands come away empty. Fuck it's in still in the car.\n\n...";
				}

				if (Input.GetKeyDown (KeyCode.D)) {
					ResetHeartbeat ();
					currentNode = "Draw";
					decisionTime = 4;
				}

			} else {
				//stuff happens in the car
				textBuffer += "The dude in the Cardinal hat walks towards you. His stance is aggressive.\n\n<i>What the fuck you gonna do?</i> he challenges as he advances.";
				textBuffer += "\n\nYou...\n[O]pen the car door";

				if (Input.GetKeyDown (KeyCode.O)) {
					ResetHeartbeat ();
					currentNode = "Aggression";
					decisionTime = 4;
				}
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				ResetHeartbeat ();
				currentNode = "Aggression";
				decisionTime = 4;

			}
			//out of car
		} else if (currentNode == "Aggression") {
			if (inCar == false) {
				textBuffer += "In a blink he's coming at you fast...he's in your face...his hands are reaching for...\n\nYou...";
				textBuffer += "\n[S]hoot";
				textBuffer += "\n[G]rab him";

				if (Input.GetKeyDown (KeyCode.S)) {
					currentNode = "SuspectDead";
				} else if (Input.GetKeyDown (KeyCode.G)) {
					ResetHeartbeat ();
					currentNode = "Grab";
					decisionTime = 4;
				}

				SpeedHeartbeat ();
				decisionTime -= Time.deltaTime;
				if (decisionTime < 0) {
					currentNode = "YouUnconscious";
				}
			} else {
				textBuffer += "In a blink he's right up in your face, hand on either side of the car window. \n\nYou...";
				textBuffer += "\n[T]ry to grab him";
				textBuffer += "\n[D]raw your gun";

				SpeedHeartbeat ();
				decisionTime -= Time.deltaTime;
				if (decisionTime < 0) {
					currentNode = "YouUnconscious";
				}
				
				if (Input.GetKeyDown (KeyCode.T)) {
					ResetHeartbeat ();
					currentNode = "Grab";
					decisionTime = 4;
				} else if (Input.GetKeyDown (KeyCode.D)) {
					ResetHeartbeat ();
					currentNode = "Draw";
					decisionTime = 4;
				}
			}

		} else if (currentNode == "Grab") {
			headerBuffer += "You grab at him...";
			textBuffer += "He flails his arms violently, catching you across the cheek. Your head spins and for a second your vision blurs.\n\nYou...";
			textBuffer += "\n[T]ry to hit him with your gun";
			textBuffer += "\n[S]hoot him";

			if (Input.GetKeyDown (KeyCode.T)) {
				ResetHeartbeat ();
				currentNode = "Draw";
				decisionTime = 4;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				currentNode = "SuspectDead";
			}

			SpeedHeartbeat ();
			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				currentNode = "BackupArrives";
			}
			
		} else if (currentNode == "Draw") {
			headerBuffer += "You reach for you gun...";
			textBuffer += "You swing it towards him. He grabs for it also.\n\n<i>You're too much of a pussy to shoot me,</i> he sneers.";
			textBuffer += "\n\nYou...";
			textBuffer += "\n[S]hoot";
			
			if (Input.GetKeyDown (KeyCode.S)) {
				currentNode = "SuspectDead";
			}

			decisionTime -= Time.deltaTime;
			if (decisionTime < 0) {
				currentNode = "BackupArrives";
			}
			
		} else if (currentNode == "Escape") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>You let a couple of little thiefs escape.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		} else if (currentNode == "BackupArrives") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>Backup arrives.\n\n<i>Guess you couldn't handle a couple of little boys.</i> No one says it, but they're certainly thinking it.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		} else if (currentNode == "Reprimanded") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>Backup arrives...just in time to see you shove an unarmed man. You're taken off active duty while your conduct is reviewed.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		} else if (currentNode == "SuspectDead") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>The suspect drops to the ground.\n\nUnarmed. Dead.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		} else if (currentNode == "YouDead") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>You don't even have time to register the shot. There's only pain. \n\nThen nothing.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		} else if (currentNode == "YouUnconscious") {
			heartbeat.Stop ();
			decisionTime = 15;
			headerBuffer += "\n\n<size=40><color=#D33800FF>BANG BANG</color></size>\n<size=12><color=#C6CCCCFF>The impact punches the air out of your lungs. A flash of sky. A crack. Then nothing.\n\n[ENTER] to restart</color></size>";
			if (Input.GetKeyDown (KeyCode.Return)) {
				Reset ();
			}
		}

		//test timed transitions - TAKE OUT FOR FINAL
//		clockCheck.GetComponent<Text>().text = introTime.ToString("F1");

		//decision timer display text
		if (decisionTime < 6 && decisionTime > 0) {
			decisionTimerText.GetComponent<Text> ().text = "<size=12><color=white>You have </color></size>" + decisionTime.ToString ("F1") + "<size=12><color=white> to decide</color></size>";
		} else {
			decisionTimerText.GetComponent<Text> ().text = "";
		}

		titleText.GetComponent<Text> ().text = headerBuffer;
		GetComponent<Text> ().text = textBuffer;
	}

	void Reset ()
	{
		currentNode = "Car";
		introTime = 15;
		decisionTime = 15;
		introDone = false;
		gun = false;
		papers = false;
		glove = false;
		radio = false;
		ResetHeartbeat ();

	}

	void SpeedHeartbeat ()
	{
		heartbeat.pitch += 0.0005f;
		heartbeat.volume += 0.0005f;
		if (decisionTime < 6 && decisionTime > 0) {
			heartbeat.volume = 0.6f;
		}
	}

	void ResetHeartbeat ()
	{
		heartbeat.volume = 0.25f;
		heartbeat.pitch = 1.0f;
	}
}
