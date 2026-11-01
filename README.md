# Recovered Unity project (

)

Status: **recovery in progress.** This is not yet an openable Unity project.

- Unity version used for the original build: **6000.3.8f1**
- `recovered-source/GameAssembly/`: C# scripts decompiled from the macOS Mono build
  (`GameAssembly.dll`, 956 files). Third-party packages (Fungus, DOTween, Live2D Cubism,
  UniTask, ...) are intentionally not included; reinstall them via Package Manager.
- Scenes, prefabs, textures, audio and Live2D assets are **not recovered yet**
  (next step: extract them from the build with AssetRipper).
- `.github/workflows/build-ios.yml`: iOS build workflow (untested). Setup and secrets: see `docs/IOS_BUILD.md`.
- 42 decompiled files have random-string names (likely generated code); review before use.
