
Catch-Another / Each other
+ Abwechselndes Fangen
+ Coins aufheben beschleunigen einen sehr kurzfristig
+ 3x Coin => Item
+ Man kann items aufheben
	+ Verfolger
		+ Schleimschüsse
		+ Wände hochziehen an stellen
		+ vlt. Wenn schüsse daneben gehen
	+ Verfolgter
		+ Fallen
			+ Gegner sieht sie nicht? Schwer zu machen.. in lokalem koop ^^
			+ Dann sieht sie keiner von beiden?
			+ Oder beide
			+ der verfolgte wirft sie hinter sich
	+ Droppen zufällig alle par sekunden
+ Wechsel
	+ Alle 30 Sekunden?
	+ In Box?
	+ insgesamt 3 minuten zeit?
	+ Abwechselnd >= Wer hält länger aus?
+ Map
	+ Möglichst Klein und verwinkelt, aber immer mit 2 Ausgängen
	+ Viele einzelne säulen?
	+ Bei jedem game neu generiert?

+ Fragen
	+ Wie wird es über Zeit schwerer?
		+ Durch die Blöcke die man hochzieht?
	+ wie droppen items?
		+ Zufällig alle paar sekunden
		+ Nach 3 Coins
	+ Kamera?
		+ Geht soweit nach hinten, solange noch beide charaktere im bild sind
		+ Min/Max-Distanz
	+ Gewonnen
		+ Wenn man den anderen fängt
		+ Wenn der Dieb 50 Kristalle hat

+ Setting?
	+ Magier in kerker?
	+ Crystal Hunter
		+ Einer klaut kristalle vom Kristallmeister
			+ und legt kristall-fallen
		+ dieser versucht ihn zu Fangen
			+ mit magie(kristall)-schüssen
			+ wo sie treffen, kommen kristalle als blockaden
			+ wenn sie den verfolgten treffen, [ist er gefangen?/ wird er kurz langsamer]
		+ beide sammeln die kristalle(3/1) in [der Höhle/Kerker/Meistershaus(Bücherregale)] um deren Energie für ihre Magie zu verwenden
		+ Jeder Kristall bring einen kurzen Boost?
			+ Nur wenn gegner gefangen werde muss
		+ Sobald er gefangen wurde, 
			+ Punch-Animation
			+ Der andere ist dran
		+ wer mehr kristalle gesammelt hat, gewinnt! ( verrechnet mit zeit? )
		+ Golem bisschen langsamer als Dieb
	+ Schlechte beleuchtung
	+ Schüsse sind ParticleSystems
		+ Erleuchten umgebung
	+ Kristall glimmen leicht im takt der musik?
		=> Get all PointLights
		=> Intensity => Set
		
	+ TODO: 

		+ Wenn Singleplayer
			=> Show Trap?

		+ UI
			+ Aufgeladener Crystall-Animation wenn 3 gesammelt
			
		+ Theft
			+ Action is buggy
		

		+ Animationen
			+ Wenn gefangen

		+ Code-Architektur
			+ Manager
				+ Wie war das mit den States und den GameStates?
			+ GameManager (HowTo make GameManager-init-class? => examples von leissler)
				+ currentRound
				+ Spiel gewonnen
				+ punktemanagement
				+ rundenmanagement
					+ rounds[0/1] Set points at currentRound
				+ Zeitmanagement
					+ if newTime > time
						+ set ui
			+ (ItemManager)
				+ kümmert sich darum, dass items gespawned werden
				+ calculated an standart-stellen
				+ wie checkt man ob aktuelle stelle belegt ist?

			
			+ Ki ist einmal Master, einmal Theft
				+ so hängen die statemachines und mit den Objekten zusammen
			+ Player ist einmal Movement
				+ Jeweils einmal 
			
			# Neue strukt
				+ 
				
			AIInput
			
				+ Character character
				+ StateMachine
				+ Chasing 
					+ cast char to CrystalMaster-Class
					+ check if crystall near
					+ shoot if 3 crystalls 
						+ character.action
					+ NavMesh.setDestination();
					+ character.move(navmesh.move);
				+ chased
					+ get to next crystall
					+ character.move(navmesh.mvoe);
			
			=> Entkopplung macht keinen SInn, da AIInput den Char eh braucht 
				
				
			GameController 
				+ onstart 
					+ FindObjectByTag
					+ set CrystalMaster
						+ disable AIInput
						+ enable PlayerInput
						
					+ set Theft
						+ disable PlayerInput
						+ enable AIInput
						+ AI.setRunning
			
			abstract CharacterCtrl
				+ items: num;
				+ moveSpeed
				+ currentSpeed
				
				+ move-ausgeschrieben
				+ abstract action
				+ abstract onCollision
			
						
			+ CameraController
				+ Verbessern
	+ Bedenken. 
		+ Nicht zu wenige Items, damit es nicht zu langweilig wird
		+ Nicht zu viele, damit es nicht zu schnell ist.
		
		=> UI
		
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
	
