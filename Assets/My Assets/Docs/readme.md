# GolfBots GDD

### Student Info

Jake O'Reilly D00256438

### Blurb

GolfBots is a 3D Point n' Click puzzle game where you learn about using angles to get Robots to press buttons.

---

# Mechanics

### Ground Types

NON-WALKABLE: Walls, areas you cannot access / walk through.\
SENDING-AREA: This is where you can aim and send Bots from.\
WALKABLE: This is where the Player can walk. Not all WALKABLE areas are SENDING-AREAs.

### Controls

Right Click - Place a point for the player to try move to\
Left Click - Hold to display what path your Bot will follow, let go to send the Bot\
Mouse Wheel Up/Down - Swap between your available Bots

### Mining Bot

This Bot will only walk in a straight line and follow its path.\
This Bot will destroy breakable blocks that are in front of it.\
This Bot can press buttons.

### Jumping Bot

This Bot will walk in a straight line, jumping at a set interval.\
This Bot cannot break the breakable blocks.\
This Bot can press buttons.

### Hint System

When the Player presses the hint button, or the H key:
- A decoration in the NON-WALKABLE area will have a glow around it, you must position your mouse here.
- A new path item will appear in the SENDING-AREA, you must stand here and send your Bot.

### Doors

Doors can't be walked through.\
There is one button that will open one door.

### Buttons

Buttons can be pressed by bots ONLY.\
One button will open the current level's exit / progression door.

### Breakable Blocks

Breakable Blocks will either be Crates or Rocks.\
These are NON-WALKABLE areas, until they are broken and are not there!\
These can be broken by Jumping Bots.

---

# Level Design

---

# References / Third Party Assets

What Models, Sounds, Music, and Plugins I used.

Most of my 3D models are from free CC0 asset packs by Kenney.

- [Mini Characters: Player Model](https://kenney.nl/assets/mini-characters-1)
- [Prototype Kit: Level Design Pieces](https://kenney.nl/assets/prototype-kit)
- [Platformer Kit: Level Design Pieces](https://kenney.nl/assets/platformer-kit)
- [Nature Kit: Level Decorations](https://kenney.nl/assets/nature-kit)

---

# Requirements

**DUE DATE: 22:00, Monday, *13th January*, 2025**

### 1. Functional Requirements (30 Marks)

| Criteria | Complete? | Value (Marks) |
| --- | --- | --- |
| 3D Scene Exploration: 100x100 gamespace | x | 5 |
| Navigation: Point n' Click Movement using navmesh | x | 5 |
| Item Interaction: Examine / Interact with items to modify game progression (press buttons with bots!) | x | 5 |
| Collectible Items (Breaking blocks with bots?) | x | 5 |
| Display Progress in UI | x | 5 |
| Endgame Feedback and further exploration after the game | x | 5 |

### 2. Software Requirements (20 Marks)

| Criteria | Complete? | Value (Marks) |
| --- | --- | --- |
| Use SelectionManager.cs |  | 5 |
| Use EventManager.cs | x | 5 |
| Use StateManager.cs | x | 5 |
| Use InventoryManager.cs |  | 5 |

### 3. Project Requirements (5 Marks)

| Criteria | Complete? | Value (Marks) |
| --- | --- | --- |
| File Structure is logical and neat | x | 5 when all are complete |
| Has _Data folder | x | 5 when all are complete |
| Has _Scenes folder | x | 5 when all are complete |
| Has My Assets folder | x | 5 when all are complete |
| Has Third Party Assets folder | x | 5 when all are complete |
| All additional plugins that are needed, are provided | x | 5 when all are complete |
| 2 Scenes **MAXIMUM** in _Scenes | x | 5 when all are complete |

## 4. Gameplay Requirements (45 Marks TOTAL)

| Criteria | Complete? |
| --- | --- |
| Game is a Single Level | x |
| Optimised for 5-10 minutes of playing, prioritise quality over quantity | x |

### 4.1 Engagement (15 Marks)

| Criteria | Complete? |
| --- | --- |
| Intuitive Controls | x |
| Immersive Experience: Use sounds, lighting, visuals to set mood and provide player feedback |  |
| Clear Objectives and Feedback | x |
| Motivation to Continue: Reward the player | x |

### 4.2 Interaction Mechanics (15 Marks)

| Criteria | Complete? |
| --- | --- |
| Purposeful Interactions: Make the player think and strategise | x |
| Variety of Actions | x |
| Balanced Complexity | x |
| Creative use of Environment: Outer-level hints, raycast hint lineups! | x |

### 4.3 Educational Effectiveness (15 Marks)

| Criteria | Complete? |
| --- | --- |
| Clear Learning Objectives: To manipulate and learn about using angles | x |
| Immediate Reinforcement: See where you can shoot and what angle changes | x |
| Assessment through Gameplay | x |
| Constructive Endgame Feedback | x |

### 5. Screencast Explanation Video

| Criteria | Complete? |
| --- | --- |
| Video called "2024 - GD3A - 3DGED - ICA - Jake O'Reilly" | x |
| 10 Minutes **MAXIMUM** | x |
| Explain Implementation | x |
| Explain Player Objectives | x |
| Explain Design Process | x |
| Explain Core Game Classes being used | x |
| Unlisted / Public on YouTube | x |

### 6. Submission Requirements

| Criteria | Complete? |
| --- | --- |
| Has a README.md with: GitHub Repo link, Screencast link  | x |
| Screencast is available on YouTube | x |
