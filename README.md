Player Scripts - Player movement and stats. Basic animation occurs when player is moving. After hero shield is depleted, health is drained.
  * HeroShield
  * PlayerHealth
  * PlayerMovement
  * RunAnimScript
  * WorldInteraction
  
Environment Scripts - Camera control and an interactable script for future use on items in the world. 
  * Camera3rdPerson
  * CameraIsometric
  * Interactable
   
Enemy Scripts - Two basic enemies. Both damage player when collisions occur. One has an energy shield. 
  *canvasPosition
  * EasyEnemy
  * EnemyCollision
  * ProwlerCollision
  * ProwlerScript
  * ShieldScript

CyborgScripts - Code for the 'boss' of the game. Boss movement doesn't begin until the player enters the area, and damage is only dealt if the metal blocks are hit when the boss is moving, since the boss is on a higher plane. 
  * CyborgMovement
  * HealthCyborg
  * MetalDamager
  * PlayerEntered

