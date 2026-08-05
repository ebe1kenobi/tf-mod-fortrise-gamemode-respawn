# GameModeRespawn

**Respawn** game mode: every player gets a number of lives. On death they respawn
right away with a short immunity window, until they run out of lives. A life bar is
drawn above each archer.

A mod for **FortRise 5** (>= 5.3.3). The FortRise 4 version (`tf-mod-fortrise-gamemode-respawn`) is no longer maintained: fixes and new features only land in this repository.

## Installation

1. Install FortRise 5 and start the game through `FortRise.exe`.
2. Copy `release/gamemoderespawn` (or the shipped folder) into `<TowerFall>/FortRise/Mods/`.

Settings are under **Options > Mods > GameModeRespawn**.
Data and log files live in `<TowerFall>/FortRise/Saves/GameModeRespawn/` and `<TowerFall>/FortRise/Logs/`.

## Usage

Pick the **Respawn** mode on the versus screen, then start the match.

> **Opening the popup**: on the versus screen, with the relevant mode selected,
> press **Y** (the "arrows" button on the controller) on the mode button. A hint is
> shown under the button. The popup locks the menu while it is open (no going back,
> no starting the match); **A** or **B** closes it.

The popup sets, **per player**, the win handicap and the number of lives:

| Input | Effect |
|-------|--------|
| Up / Down | switch player |
| Alt (RB) | switch between the "wins" and "lives" fields |
| Left / Right | adjust the value |
| A or B | close |

## Settings

| Setting | Purpose |
|---------|---------|
| Respawn: starting lives (needs > 1) | default number of lives |
| Respawn: immunity on respawn (seconds) | immunity window after respawning |

The life bar only shows up in this mode: it is gated on the session's game mode, so
it cannot leak into another one.

## Build / deployment

| Script | Purpose |
|--------|---------|
| `script/release.bat` | build, then assemble into `release/` |
| `script/deploy.bat` | copy `release/` into the TowerFall `Mods` folder |
| `script/release_deploy.bat` | both, one after the other |

Paths (game folder, module name) are set in `script/config.bat`.
