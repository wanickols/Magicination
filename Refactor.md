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


### Reconsider Namespaces
Relook at some global namespaces and see if they could be core. 
Also consider making a util namespace or something that's
	meant to be incorperated into both core and battle. 

In Addition here, several scriptable objects have no namespace and could be reworked

#### File Management 

The scripts folder, scriptable objects, and all the extra folders I have in the editor are a bit messy. Reworking this as I rework the namespaces would be nice. 


#### ints
I want to refactor the integers in stats class to be unsigned ints of specific sizes. Since the stat class is being passed around so much, and these values are pretty well determined to always be positive and of set maximums, I think it'd be worthwhile.