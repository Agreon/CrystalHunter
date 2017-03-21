## Intro
Oh No! 
The CrystalMaster has found you stealing his Crystals. 
Run for your life!

**Control**
+ Collect Crystals to use their power and speedUp
+ Collect 3 Crystals to lay a trap [Space] and hopefully get some time
+ If you need a break, press [escape] to pause the game!

**Multiplayer**
+ Run with the arrows
+ Shoot Crystals with [ctrl]


		
	+ TODO: (Nach Prio)

		+ TODO 
			+ Major							
				+ Camera weiter nach vorne setzen?
					=> Ja
					=> FlyAnim muss abgeändert werden


				+ Sounds
					+ Golem-Stomp
						=> Wie checke ich den schritt?
					
					=> Reset queue on Menu
							

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
					+ Debug.Logs raus
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


			
		+ Music 
			+ Brechja raus?			



		+ NiceToHave
		
			Theft 		
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
		+ Multiplayer
			+ Input 
				+ Golem is too fast
				=> Ugly workaround?
					=> PlayerInput ( if ai, halbiere(0.6/07) den move-vector

				+ SpeedUp is too fast
					=> workaround
		
	+ assetstore bringt vlt. Inspiration
		+ https://www.assetstore.unity3d.com/en/#!/content/58821 | Low Poly: Free Pack
		+ https://www.assetstore.unity3d.com/en/#!/content/57394 | Voxel-Dungeon
		+ https://www.assetstore.unity3d.com/en/#!/content/74611 | Voxel-Dungeon-Interior
		+ https://www.assetstore.unity3d.com/en/#!/content/8312	| Make Your Fantasy Game
		+ https://www.assetstore.unity3d.com/en/#!/content/44045
		+ https://www.assetstore.unity3d.com/en/#!/content/80898 | Goldbarren
		+ https://www.assetstore.unity3d.com/en/#!/content/57538 | Kristalle
			+ Könnte für Wälle benutzt werden
		+ https://www.assetstore.unity3d.com/en/#!/content/75524 | Tiles für Floor?
		+ https://www.assetstore.unity3d.com/en/#!/content/7275 | Fackel
		+ https://www.assetstore.unity3d.com/en/#!/content/3356 | Bücher
		+ https://www.assetstore.unity3d.com/en/#!/content/9994 | RPG-Interior
		+ https://www.assetstore.unity3d.com/en/#!/content/44185 | Paintings
		+ https://www.assetstore.unity3d.com/en/#!/content/59038 | Old Paintings
		+ https://www.assetstore.unity3d.com/en/#!/content/8458 | Bookshelfes
		+ https://www.assetstore.unity3d.com/en/#!/content/67811 | Voxel interior
		
		+ Charaktere
			+ https://www.assetstore.unity3d.com/en/#!/content/3746 | Ice-Golem (Passt zu kristallen)
				+ To throw => punch animation
			+ https://www.assetstore.unity3d.com/en/#!/content/21874 | StealthChar
				+ Verfolgter
				+ Müsste waffen entfernen
		+ Partikel
			=> Schießt Kristall, der ganz wenig partikel hat?
6-9, 10?

			+ https://www.assetstore.unity3d.com/en/#!/content/72399 | Attack
			+ https://www.assetstore.unity3d.com/en/#!/content/59315 | Attack
			+ https://www.assetstore.unity3d.com/en/#!/content/65810 | Attack
			+ https://www.assetstore.unity3d.com/en/#!/content/20404 | Smoke (Attack?)
			+ https://www.assetstore.unity3d.com/en/#!/content/68246 | Highlight	
		
		+ https://www.assetstore.unity3d.com/en/#!/content/3192 | AI Scripts
		+ https://www.assetstore.unity3d.com/en/#!/content/54579 | Top-Down AI
		+ https://www.assetstore.unity3d.com/en/#!/content/40756 | Movement / Camera scripts7
		+ https://www.assetstore.unity3d.com/en/#!/content/5141 | Ideen zum Procedural Generation
		


Musik
https://github.com/allanpichardo/Unity-Beat-Detection
+ Smthg. with beat detect?
Techno
Liquid dnb
https://www.youtube.com/watch?v=Tykyu1J-adk


oder
+ too many chiefs
+ sylosis?
+ Bones exposed?
+ go with the flowc
+ 34 - karma to burn
+ Come alive
+ Dragona Dragona
+ OCD
+ In search of (the)
+ prehistoric dog
+ waidmanns heil
+ sarcastrophe
+ aesthetics of hate
+ lamb of god?
+ marrow - meshuggah :D
+ were going in, were going down



# Anforderung
+ Entkoppelter code
	+ UnityEvent
+ RequireComponent-Annotation
+ Interfaces!
	+ Find Objects of Type
		=> Nur bei abstract class [Name] : MonoBehaviour
		=>  var stuff = InterfaceHelper.FindObject<IIrresistible>();	
	
