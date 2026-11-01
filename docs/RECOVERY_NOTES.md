# Recovery handoff notes

Goal: rebuild a Unity project for **My Dystopian Robot Girlfriend** (owner-confirmed, project files lost)
and produce an iOS .ipa via the GitHub Actions workflow in `.github/workflows/build-ios.yml`.

## Inputs (owner's Google Drive, share links held by the owner)
- Android APK (IL2CPP, ~696 MB, Unity 6000.3.8f1, Live2D Cubism). Not usable for recovery of scripts.
- macOS build (.app zip, ~587 MB, **Mono backend**). This is the source for recovery.
  Download from Drive needs the "confirm=t&uuid=..." step from the warning page
  (use drive.usercontent.google.com/download).

## Done
- Unity version: 6000.3.8f1.
- Scripts decompiled with ilspycmd 11.2 (.NET 10 SDK via apt) from
  `Contents/Resources/Data/Managed/GameAssembly.dll` -> `recovered-source/GameAssembly/` (956 files, 0 failures).
  `Assembly-CSharp.dll` is nearly empty (7.5 KB); the game code lives in `GameAssembly.dll`.
  42 files have random-string names (probably generated code).
- iOS workflow + LFS rules + gitignore added (workflow untested).
- GitHub LFS budget set by owner (10 GB free storage/bandwidth, $6 budget for bandwidth).

## Blocked / not done
- **Scenes, prefabs, textures, audio, Live2D assets not recovered.** Plan: run **AssetRipper** on the macOS build
  (File > Open > the .app, then Export > Export all files), merge with `recovered-source/`,
  reinstall packages (Fungus, DOTween, UniTask, Live2D Cubism, Addressables, etc.) via Package Manager.
- AssetRipper could not be fetched in the previous session: github.com/AssetRipper/* and nuget.samboy.dev
  returned 403 from the session proxy. Building the owner's fork from source failed because the source needs
  packages only on nuget.samboy.dev (nuget.org alone gives compile errors). Prebuilt release is the way to go.
- Needs a real Unity 6000.3.8f1 Editor to open/fix the recovered project (not available in cloud sessions).

## Then
1. Commit the AssetRipper export (Assets/, ProjectSettings/, Packages/) with Git LFS.
2. Add repo secrets listed in `docs/IOS_BUILD.md`, run **Build iOS** from the Actions tab.
