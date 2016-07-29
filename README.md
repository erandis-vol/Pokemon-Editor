# Hopeless Pokémon Editor
Copyright 2015-2016 Lost

An Pokémon editor for 3rd generation Pokémon games. Currently incomplete, may or may not see further development.

## Supported games
All non-Japanese 3rd generation games should be supported. .ini only contains data for English games.

## Features
Allows the editing of:
* Base stats
* Evolutions
* Movesets
* Pokédex entry
* Move tutor compatibility (non-RS games)
* Sprite and icon offsets
* Cries (incomplete, does not load Treecko+ cries correctly)

Additonally, it can:
* Expand Pokémon and number of evolutions (FR-only, untested)
* Export an .ini file for use with Wichu's Advanced Series

## Basic usage
When you first open a ROM file with the editor, a custom .ini is created in the ROM file's directory. For example, if you open "BPRE.gba" then "BPRE.hpe.ini" will be created within the file's directory. You can modify this file to suit your game as needed.

The .ini contains an entry for MrDollSteak's ROM base. To use it, change your game's ROM code to "MrDS" with a hex editor and open the game as normal. You can then change your game's code back to "BPRE" but if you do so be sure to edit your game's generated .ini as well to reflect the change.

## Warning
The majority of this program was coded over a week in 2014... please do not consider this an example of good code or use this as something to learn from.
