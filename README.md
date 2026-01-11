# Duality 
https://nunnyu.itch.io/duality
https://itch.io/jam/gamedevjs-2025/rate/3504627

#### 25th out of 411 in Gamedev.js Jam 2025 (and 12th in audio!) 

## Overview

**Duality** is a 2D puzzle-platformer game built with Unity Engine, featuring dual-character cooperative gameplay mechanics. Players control two complementary characters—Lumine (light) and Umbra (dark)—navigating through interconnected levels that require strategic coordination to progress.

## Technology Stack

- **Game Engine**: Unity 6000.0.32f1
- **Programming Language**: C#
- **Rendering Pipeline**: Universal Render Pipeline (URP) 17.0.3
- **Input System**: Unity Input System 1.11.2
- **Physics**: Unity Physics2D
- **Audio**: Unity Audio System
- **Platform**: 2D

## Core Architecture

### Game State Management
- **Singleton Pattern**: Implemented via `ManageGame` class for global state management
- **Scene Management**: Multi-level system with dynamic loading and progression
- **Persistent Data**: Game manager persists across scene transitions using `DontDestroyOnLoad`

### Dual Character System
The game features two distinct playable characters with complementary mechanics:

- **Lumine (Light)**: 
  - Controlled via WASD keys
  - Health represented as brightness (decreases in darkness)
  - Attacks with light-based projectiles
  - Stronger at low health (inverse health-to-power scaling)

- **Umbra (Dark)**:
  - Controlled via arrow keys
  - Health increases in bright zones (opposite mechanics)
  - Attacks with dark-based projectiles
  - Complementary power scaling system

## Systems Architecture

### Health & Lighting System
- **Dynamic Health Model**: Health directly influences lighting intensity in real-time
- **Territory-Based Mechanics**: Characters take damage in opposing territory zones
- **Regenerative System**: Health regeneration when in safe zones
- **Visual Feedback**: Lighting intensity scales with character health using `Light2D` component

### Combat System
- **Directional Attack System**: Attacks dynamically orient based on movement input
- **Dynamic Scaling**: Attack size and damage scale inversely with health (risk-reward mechanic)
- **Collision Detection**: Circle collider-based hit detection with type-specific damage
- **State-Dependent Visibility**: Attacks only visible/active during movement

### Level Progression
- **Progressive Level System**: 7 levels (Level0-Level6) with incremental difficulty
- **Gate Mechanics**: Key-based progression requiring both characters to collect keys
- **Win Condition**: Both characters must reach their respective exits (with the keys)
- **Failure State**: Reset on death of both characters or timeout

### Audio Management
- **Singleton Audio Manager**: Centralized audio system with persistent instance
- **Dynamic Audio Sources**: Runtime audio source creation and management
- **Sound Categories**: Title music, gameplay themes, effect sounds (death, key collection, enemy destruction)
- **Loop Management**: Configurable looping for background music

### Timer System
- **Level Time Limits**: Configurable per-level time constraints
- **Visual Feedback**: UI bar with color transitions indicating time remaining
- **Warning System**: Visual flashing when time runs low (5 seconds remaining)

## Technical Highlights

### Performance Optimizations
- Singleton pattern implementation prevents duplicate manager instances
- Efficient Update/FixedUpdate separation for physics calculations
- Vector2 operations for 2D-specific calculations
- Linear interpolation for smooth movement transitions

### Code Quality
- Separation of concerns across specialized scripts
- Modular design allowing independent system updates
- Serialized fields for designer-friendly parameter tuning
- Tag-based collision detection system

### Design Patterns
- **Singleton**: Game and audio managers
- **Component-Based Architecture**: Unity's component system
- **Observer Pattern**: Event-driven health and state changes
- **State Pattern**: Character states (menu, gameplay, level transition)
