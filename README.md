# About
Main purpose of this micro-game project is to demonstrate decoupling between Unity-specific code and common code using [LeoECS](https://github.com/Leopotam/ecslite) framework. For example, it could be useful for development of engine-agnostic reusable modules or sharing code between Unity client and pure-Mono server.

# Details
## Project structure

### Code/GameCore
This folder is Unity-free (actually, not really - it uses Vector3, but it doesn't depend on Unity's runtime environment)

### Code/UnityAware
MonoBehaviors, ScriptableObjects, ECS components and systems and other things that make sense only in context of Unity engine.

Most of ECS systems here are purely presentational. But there are exceptions: `UnityNavigationSystem`, `UnityCoreTimeSystem` and `UnityLoadLevelSystem`. 
These systems are Unity-based implementations of the features that should be replaced by custom variants in another environments.

## DI
This project uses two DI mechanisms in parallel. [Leopotam's one](https://github.com/Leopotam/ecslite-di) for ECS resources (pools, filters, worlds) and [Zenject](https://github.com/modesttree/Zenject) for others.
