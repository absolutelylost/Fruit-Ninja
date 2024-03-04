CAP6121 3D UI for Games and VR Project #2

Contributors
Delroy Jordan
Nathan Justin

What works
- we are ninja's (mostly)
- collecting cookies and cakes heals us
- movement via left hand grip and gaze tracking
- jump/fly (time based for brief period)
- duck (just based on head position, but not needed in this demo)
- 5 different fruit with different attack patterns 
	(some fire 1, some fire 3, some fire at different angles and directions)
- stab, slice, chop, parry (works sometimes, didnt work in demo but because collider is small)
- grappling hook to pull enemies in
- finite set of ninja stars set at 15. (thrown by holding trigger and letting go while moving hand forward)
	(sometimes this may collide with player controller collider)
	(uses hand velocity to see how fast or hard the star will be thrown)

************************************************************************************************************
Bonus - fruit that gets attacked with blade cuts the model in the plane it was cut by the sword
	- Really cool, took a little bit to implement but not too hard.
************************************************************************************************************

What doesn't work
- fruit to move and act based on use position or visibility
- Voice Recognition worked on my partner's (Nathan) PC but not when I ran it on my system. 
	This was the last thing we added so ran out of time to fix or try again on a different system.

Individual Contributions

Delroy:
- configured HUD interaction
- character movement, jumping, flying
- fruit behavior in terms of attack, damage, and score
- stab, slice, chop, parry (parry didnt work as well as others)
- fruit slicing to seperate a mesh into two meshes based on bisection
- grappling
- ninja stars mechanics

Nathan:
 - Created scene/level for the player to play on
 - Created Navmesh, NavAgents, and NavObstables for AI movement of the fruit enemies
 - Created voice recognition to enable the fruit to pause when player vocally give a command
 - Configured interactions between the sweets and player character 


Youtube Link: 
https://youtu.be/0lMeTisNXRU
