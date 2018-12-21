# FourRooms
Unity Editor Version: 2018.1.7f1

This is a Unity project developed for running human behavioural navigation experiments online using webGL. It has been designed to include a general purpose codebase that can easily serve as the basis for other online behavioural experiments in Unity.

## Overview

- You will need to edit filepath.path in filepath.cs script for an appropriate  location for the log file.

- To run an experiment, you must start the game from the persistent scene, 'Persistent'
- The gameplay is controlled *almost* entirely from the singleton GameController.cs. There is some gameplay that is local to short scripts attached to objects in different scenes, but where possible these scripts trigger functions within GameController.cs to keep gameplay centralised and readable. A finite-state-machine tracks within-trial changes to the game state.

- The data management is operated through DataController.cs. Any configuration file that needs to be read or loaded, any online changes to trial list sequencing, and any saving of the data (to either the Summerfield Lab webserver or to a location on your local computer), is performed here. There is one instance of DataController that persists between scenes and is fetched/found by other smaller scripts when needed - so it is effectively a singleton but implemented slightly differently.

- The experiment configuration is controlled through the script ExperimentConfig.cs, which specifies the trial sequencing, randomisation, and experiment-specific controlled variables e.g. the duration and frequency of restbreaks.

- When playing the game, movements are controlled with the arrow keys. The space-bar is used to unwrap gift boxes. There may be small differences in the sensitivity of different computers to these key inputs. These keys should automatically adjust to the keypad/game control you are using, but if not then these can be adjusted using Edit > ProjectSettings > Input. Sensitivity can be edited within the FPSController in each scene. Make sure you edit ALL FPSController instances if you do this!

## Notes:

- At the start of each major script I have left some notes on things that remain to be tidied, but shouldn't massively change functionality. I will address these at some point...
