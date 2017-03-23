## Intro
Oh No!

The CrystalMaster has found you stealing his Crystals! 

Run for your life!

**Control**
+ Collect Crystals [wasd] to use their power and speedUp
	+ Feel free to try out the gamepad-input ;)
+ Collect 3 Crystals to lay a trap [space] and hopefully get some time
+ If you need a break, press [escape] to pause the game!

**Multiplayer**
+ Run with the arrows
+ Shoot Crystals with [right-ctrl]


## Dev
Please Ignore the MenuScene as it is not used anymore. The IntroScene is the new Start-Scene.


There are still a lot of TODOS left:

+ Major							
  + Camera weiter nach vorne setzen?
	  + Ja
		+ FlyAnim muss abgeändert werden


		+ Sounds
					+ Golem-Stomp
						+ Wie checke ich den schritt?
				  + Reset Soundqueue on Menu
							

			+ Map verschönern?
					+ Pillar-Beleuchtung
					+ Accesoirs
						+ Skelette?	
						+ Wände als Kristalle?				
					  + Fackeln
			+ Play-Again bei score?

		  + CrystalMaster
				+ AI
						+ verbessern

			+ Codequali
					+ Möglichst viel Zuweisen, ohne .Find
					+ Namespaces
					+ überflüssiges raus
					+ Methoden alle groß/klein?
					+ PIllar-Mat in MenuState Laden, nicht einzeln 
					+ menumanager kann raus?
					+ instantiate-objs zu prefabs?
			+ Minor
				+ falle benötigt zu lange um aus dem boden zu kommen 

			  + Camera-blur out/in weil figuren fallen 

			 + NavMeshAgent Disable wenn gefangen
					+ und wenn schießen
				+ Pause 
					+ nach der gefangenschaft wesentlich schneller

				+ Golem hat initial 3 schuss 
					=> zum testen

				+ IntroScreen
					+ Player1/2 über kopf der objekte
			
		+ MetalMode
			+ Flammen in den kristallen?

		+ NiceToHave
		
		+ Theft 		
			+ specials
				+ trap
				+ invisible
			+ getroffen
				+ Bildschirm wird blurry und twisted
				+ m_Counter * intensity


			+ MenuManager
			=> Camera fliegt in StartPosition bei Singleplayer-click
				=> Kein Countdown, aber Intro

			+ GameOver
				+ BlurOut
			+ Mehr als 3 Kristalle sammeln
				=> Overload
				=> Grayscale
				=> Twist
			=> Wenn getroffen
				=> Twist
		
			+ Aufgeladener Crystall-Animation wenn 3 gesammelt?
				+ shininess material
				=> Animation hinkriegen
				=> Lowe pri
