## Purpose

This file gives repository-specific guidance for AI coding agents (Copilot-style) to be immediately productive in this Unity project.

## Quick facts
- **Unity Editor version:** `ProjectSettings/ProjectVersion.txt` contains `m_EditorVersion: 6000.2.8f1` (open project with matching Unity editor build).
- **Key folders:** `Assets/Scenes`, `Assets/Prefabs`, `Assets/Scripts`, `ProjectSettings`.
- **Large assets:** Some assets use Git LFS (see root `README.md`); expect binary assets in `Assets/`.

## Goals for an agent
- Make small, focused changes to C# MonoBehaviour scripts in `Assets/Scripts`.
- Preserve Unity project structure and `.meta` files; do not rename or remove files without user confirmation.

## Project conventions (observed patterns)
- **Lesson-numbered script folders:** Scripts are grouped into `1-movers`, `2-spawners`, `3-collisions`, `4-levels` — numeric prefixes indicate lesson/order and should be preserved.
- **MonoBehaviour style:** Scripts are small `MonoBehaviour` components that interact via `GetComponent<T>()`, `RequireComponent`, and prefabs.
- **Input handling:** Uses the new Input System API (`UnityEngine.InputSystem`). Pattern: declare `InputAction` as a `[SerializeField]`, call `Enable()`/`Disable()` in `OnEnable`/`OnDisable`, and check `WasPressedThisFrame()` in `Update()` (see `Assets/Scripts/3-collisions/ClickScoreAdder.cs`).
- **Text display:** Uses `TextMeshPro`; Unity Editor may prompt to import it on first open.

## Typical change patterns (examples)
- Add a new small component: create `Assets/Scripts/<folder>/MyNewBehaviour.cs` with a PascalCase class matching the filename and derive from `MonoBehaviour`.
- Input example (follow this pattern):
```
[SerializeField] InputAction addAction = new InputAction(type: InputActionType.Button);
void OnEnable() { addAction.Enable(); }
void OnDisable() { addAction.Disable(); }
void Update() { if (addAction.WasPressedThisFrame()) DoSomething(); }
```
- Inter-component communication: prefer `GetComponent<OtherComponent>()` on the same GameObject (short-lived lookups are common in this repo); if performance-critical, cache in `Start()`.

## Files to inspect when making changes
- `Assets/Scripts/3-collisions/ClickScoreAdder.cs` — example Input System + `NumberField` usage.
- `Assets/Scripts/1-movers/*` and `2-spawners/*` — movement and spawning patterns to reuse.
- `ProjectSettings/ProjectVersion.txt` — Unity editor version to match.
- `README.md` — notes about `git lfs` and project purpose.

## Build / run / debug notes
- This is a Unity Editor project. Open it with the Unity Editor matching `ProjectVersion.txt`.
- Import `TextMeshPro` if prompted; scenes rely on it for score displays.
- There are no automated unit tests in the repo—validate changes by opening the relevant scene in the Editor and pressing Play.

## What to avoid
- Do not modify `.meta` files or move assets without explicit instruction from the user.
- Avoid changing project-level settings (in `ProjectSettings/`) unless the user requests it.

## Prompt examples for agents
- "Add a new `MonoBehaviour` named `AutoDestroy` under `Assets/Scripts/2-spawners` that destroys the GameObject after a serialized `lifetime` seconds. Follow the repo's file/class naming and use `Start()`/`Destroy()` patterns."
- "Fix input handling in `ClickScoreAdder` by caching the `NumberField` component in `Start()` instead of calling `GetComponent` in `Update()`; keep behavior identical and run basic playtest steps in the Editor." 

## If you need clarification
- Ask the user before renaming, moving, or deleting assets/prefabs.
- Ask which Unity Editor version the user prefers if `ProjectVersion.txt` appears out-of-date.

---
If any section is unclear or you'd like this file to include extra examples (tests, playtest steps, or preferred naming conventions), tell me what to add and I'll iterate.
