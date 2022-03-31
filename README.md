# About
Main purpose of this micro-game project is to demonstrate decoupling between Unity-specific code and common code using [LeoECS](https://github.com/Leopotam/ecslite) framework. For example, it could be useful for development of engine-agnostic reusable modules or sharing code between Unity client and pure-Mono server.

# Details
## Project structure

### Assets/Code/GameCore
This folder is Unity-free (actually, not really - it uses Vector3, but it doesn't depend on Unity's runtime environment)

### Assets/Code/UnityAware
ECS components, systems and other things that make sense only in context of Unity engine.

Most of ECS systems here are purely presentational. But there are exceptions: `UnityNavigationSystem`, `UnityCoreTimeSystem` and `UnityLoadLevelSystem`. 
These systems are Unity-based implementations of the features that should be replaced by custom variants in another environments.

## Decisions

### Events
Entities with the only `*Event` component are meant to be exposed to anyone who like and be disposed at the and of frame by special `DelComponent<>` system.

### Requests 
Entities with the only `*Request` components are meant to be handled by some particular (possibly environment specific) system. 
Consumer system is responsible for disposal of this entity. 

### Entity lifecycle
`*Initialization` and `*Termination` components are meant to be exposed on some entity during one frame for all interested systems to allow handling this entity's lifecycle. Like constructors and destructors in OOP.
`*Initialization` sould be destroyed by special `DelComponent<>` system and `Termination` should be destroyed by special `DelEntityByMarker<>` system with the entity itself.
 
### References
`*Ref` components are used to store Unity component references as ECS components. Main purpose - avoid overhead from `GetComponent*` family of Unity methods.

### Multiple worlds
Due to specifics of internal memory organization in LeoECS Lite, to reach optimal memory usage, two ECS worlds are used: default and "short". 
World "short" serves as home for short-living entity components.

### DI
This project uses two DI mechanisms in parallel. [Leopotam's one](https://github.com/Leopotam/ecslite-di) for ECS resources (pools, filters, worlds) and [Zenject](https://github.com/modesttree/Zenject) for others. 
They used both because both mechanisms do better than each other in their areas, both quite popular and do not conflict.
