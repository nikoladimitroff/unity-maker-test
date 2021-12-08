#  Game mixer-maker mini-project

This project explores building a mini game-maker in Unity.

Some notes on the design:

- The UI is build on top of the standard MVVM approach with custom made model, VM and view.
    - Sadly, as Unity's UI seems to be incapable of doing even the most basic operations without breaking issues (like nesting canvases), the views themselves are all made within the main scene instead of being separate prefabs. I'm sorry for that, I'm not proud of it either.
- The communication is done via 3 global singletons (which sounds much worse than it actually is).
    - AppModeManager is basically the UI manager and handles which screen is currently active i.e. which part of the UI tree should be active.
    - GlobalSettings is stores all settings you would normally add into the game's config file but doing so would take far longer time than allocated
    - GameState is a repository of global variables which effectively control the game(maker). For anyone familiar with Redux, it would seem to be pretty obvious what it does.
    - All 3 of these exist for the sole purposes of decoupling other systems because that way systems only care about the global singletons, not about any other system
- The interactability is done via a simple component which triggers all of the required behaviours. I've chosen 1 component instead of 2 independent ones because:
    - 1 raycast is better than 2
    - some of these might require more complex interactions and synchronization between the systems and if we were to add more than 2 options having a sync point like that would prove beneficial.
- This work was done in about 6-7 hours, 2-3 of which were spent on figuring out why does Unity behave the way it does (mostly in the UI department)

## If I had more time

- I'd avoid using Unity's UI system at all costs as getting it to work took the majority of my time
    - may be their new XML thing will be a game changer?
- The only thing I'd add is probably an explosion VFX along the SFX that's currently present. I tried doing that but in the end decided against it since it seems to require a touch of magic to play correctly and I've already spent too long developing this simple of a prototype.
