In this asset there are two methods which are called by the editor
- OnInspect 
- OnHierarchyInspect

OnInspect is called on MonoBehaviours automatically when you inspect a gameobject, it is also called
on scriptable objects that derive from InspectedSO though if you make a custom editor for you 
scriptable object you need to derive from OnInspectSOInspector.

OnHierarchyInspect will called on MonoBehaviours automatically and will be called when you inspect any 
gameobject in a hierarchy 

These methods need to be private and you can have multiple of these methods in a derived class and 
base class.

There are snippet scripts for these methods in the editor folder inside the snippets folder,
these files can be added to your coding environment like Visual Studio.

There are also singleton base scripts which can be derived from to have a singleton, some of the singletons will have to be put in a folder called for Singletons in
the Resources folder, while others should never be on a gameobject in the editor, the last one is a Scene singleton that needs to be in a scene it won't be made to a prefab.