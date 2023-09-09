# Refactor Notes
## Refactor 4
### UI

#### Selectors

The Selector class should be remade as an abstract with a couple variants,
so tree selector isn't reimplementing code needlessly. 

Tree Selector Shouldn't touch SkillHolder, just node. There should just be
logic inside the skill menu that converts the two or something. 
--> Skill Holder should be core, Selector's should be global. 


#### Main Window
I want the main window to be refactored.
Much of the funcitonionality in the main window is simply a connector to the other menu classes. 
Those classes should be accessed directly, and the main window should keep more specific functions. 

#### Skills

Show Description could really be put into a functon in object/skill

### Reconsider Namespaces
Relook at some global namespaces and see if they could be core. 
Also consider making a util namespace or something that's
	meant to be incorperated into both core and battle. 