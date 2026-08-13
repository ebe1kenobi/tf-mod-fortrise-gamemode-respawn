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

The popup sets the number of lives **per player**, plus the immunity window shared
by everyone:

| Input | Effect |
|-------|--------|
| Up / Down | switch player |
| Alt (RB) | switch between the LIFE and IMMUNITY fields |
| Left / Right | adjust the value |
| A or B | close |

Immunity is a single value, so it sits on its own line under the player list
(`IMMUNITY (ALL)`) rather than being repeated on every row.

### Life display

A bar of segments is drawn above each archer. Past **8 lives** the bar would be
unreadable, so it is replaced by a plain counter.

The arrow counter is hidden during the immunity window that follows a respawn.

## Settings

| Setting | Purpose |
|---------|---------|
| Respawn: starting lives (needs > 1) | number of lives, applied to every player |
| Respawn: immunity on respawn (seconds) | immunity window after respawning |

### Settings and popup are linked

They act on the same values, but the popup is per player while a setting is global:

- changing a **setting** applies the value to **every** player;
- changing a value in the **popup** only updates the setting when **all active
  players share it** — a single global value cannot represent four different ones,
  and overwriting it would show something misleading.

Immunity has no such issue: one value on both sides, always in sync.

Every change is written to disk immediately. FortRise only saves settings when
leaving the game's Options menu, so a value changed in the popup — or right before
quitting — used to be lost.

The life bar only shows up in this mode: it is gated on the session's game mode, so
it cannot leak into another one.

## Game mode icon

The mode has its own icon, at the size of the game's four (184x82) and in their
style - a silhouette in three shades of one colour, no black: the archer inside a looping arrow.

It used to be borrowed from LAST MAN STANDING's, which says the opposite of this mode. Two modes sharing
one picture cannot be told apart in the list.

The file is `ModFile/Content/Atlas/gamemode.png`.

## Build / deployment

| Script | Purpose |
|--------|---------|
| `script/release.bat` | build, then assemble into `release/` |
| `script/deploy.bat` | copy `release/` into the TowerFall `Mods` folder |
| `script/release_deploy.bat` | both, one after the other |

Paths (game folder, module name) are set in `script/config.bat`.
