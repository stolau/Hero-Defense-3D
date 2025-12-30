# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**HeroDefender3D** is a Unity 3D tower defense game where players control a hero character to defend against waves of enemies. Built with Unity 2021.3.25f1.

**Product Name:** HeroDefender3D
**Company:** Upicentre Software
**Platform Target:** Mobile (default orientation: landscape)

## Building and Running

### Opening the Project
- Open the project in Unity 2021.3.25f1
- Main scenes are located in `Assets/` directory: `level.unity` (gameplay) and `menu.unity` (main menu)

### Building
- Use Unity's Build Settings (File > Build Settings)
- Target platform is configured for mobile devices
- Project uses C# scripts compiled via Unity's build system

### Testing in Unity
- Open `Assets/level.unity` for gameplay testing
- Press Play in Unity Editor
- Use virtual joystick controls for player movement
- Test spell casting via the UI spell slots

## Git Workflow

### Main Branch
The main development branch is **master**.

### Before Starting Any Task

**ALWAYS** verify you are on the latest master branch before making changes:

```bash
git status                    # Check current branch
git checkout master          # Switch to master if needed
git pull origin master       # Get latest changes
```

### Creating Feature/Fix Branches

When tasked to make changes, **ALWAYS create a new branch** unless explicitly told to work within an existing branch.

#### Branch Naming Convention

Use descriptive branch names with appropriate prefixes:

- **FIX/** - For bug fixes
  - Example: `FIX/enemy-spawn-timing`
  - Example: `FIX/spell-cooldown-not-resetting`

- **FEATURE/** - For new features
  - Example: `FEATURE/new-fire-spell`
  - Example: `FEATURE/boss-enemy-type`

- Other common prefixes:
  - **REFACTOR/** - Code refactoring
  - **UPDATE/** - Updates to existing features
  - **TEST/** - Adding or modifying tests

```bash
# Create and switch to a new branch
git checkout -b FEATURE/short-description

# Example
git checkout -b FIX/player-health-bar-display
```

### Working on Existing Branches

If the user asks to make changes to a specific branch:
1. Verify the branch exists: `git branch -a`
2. Switch to it: `git checkout branch-name`
3. Make changes without creating a new branch
4. **IMPORTANT**: If unsure whether you're on the correct branch, always verify with `git status` before starting work

### Branch Verification Checklist

Before making any code changes:
1. Run `git status` to confirm current branch
2. Ensure you're NOT on master (unless explicitly instructed)
3. Ensure branch name matches the task (FIX/FEATURE/etc.)
4. If on wrong branch, switch to correct one or create new branch

### Example Workflow

```bash
# Starting a new bug fix
git checkout master
git pull origin master
git checkout -b FIX/enemy-health-calculation

# Make your changes...

# Verify branch before committing
git status

# When ready to commit
git add .
git commit -m "Fix enemy health calculation overflow"
```

## Code Architecture

### Core Game Loop

The game follows a wave-based defense structure managed by **LevelManager**:
- Spawns enemy waves from predefined Wave configurations
- Tracks player lives (enemies that reach the portal)
- Handles win/loss conditions
- Manages wave progression and enemy spawning

### Key Systems

#### 1. Player System
- **Player.cs**: Core player health, damage handling, and spell damage application
- **HeroMovement.cs**: Movement via virtual joystick (`VirtualJoystick.cs`)
- **Spellbook.cs**: Contains all spell implementations with individual cooldowns and particle systems
- **HeroManager.cs**: Manages hero selection/configuration

#### 2. Enemy System
- **Enemy.cs**: Enemy health, attacking, death handling, and DoT (Damage over Time) effects
- **EnemyMovement.cs**: AI pathfinding - enemies move toward player, or toward portal if player is too far (>20 units) or out of range (>60 units)
- **Wave.cs**: Data structure defining enemy waves
- Enemies spawn alternately from two spawn points for variety

#### 3. Spell/Combat System
- **Spellbook.cs**: Central spell repository with ~12 different spells (FrostNova, EarthQuake, Charge, Slash, Scream, Fireball, Roots, CursedGround, Spin, Blizzard, etc.)
- **HitBoxCheck.cs**: Collision detection for spell hit boxes, handles damage application, slow effects, lifesteal, DoT ticks, and explosive spells
- **HitBoxCheck_Refresh.cs**: Similar to HitBoxCheck but refreshes enemy hit lists periodically for continuous damage spells
- **spellBehavior.cs**: Handles spell movement and behavior patterns (projectiles, charges, etc.)
- **SpellManager.cs**: Manages equipped spells across 3 spell slots, persists between scenes
- **SpellDragHandler.cs**: UI drag-and-drop for spell selection
- Each spell has associated ParticleSystem and GameObject collider

#### 4. DoT (Damage over Time) System
- **dotManager.cs**: Manages multiple DoT effects on enemies
- **Dot.cs**: Individual DoT effect data structure
- **dot_Tick_Timer.cs**: Handles DoT tick timing
- Ticks occur every 2 seconds, applied in `Enemy.damageTicks()` coroutine

#### 5. Level Management
- **LevelManager.cs**: Wave spawning, enemy death counting, lives tracking, game end conditions
- **MenuManager.cs**: Menu scene management
- **GameOpen.cs** / **GameEnd.cs**: Game state transitions

### Important Architectural Patterns

#### Spell Execution Flow
1. UI button/drag triggers spell in Spellbook
2. Spellbook checks cooldown timestamp (`tsSpellName <= Time.time`)
3. Activates spell collider GameObject
4. Plays ParticleSystem effect
5. Triggers player animation (`playerModel.GetComponent<Animator>().Play("Attack")`)
6. HitBoxCheck detects collisions with enemies
7. Calls `Player.AttackPlayer()` which calls `Enemy.getHitEnemy()`
8. Damage applied, slow effects/DoT applied if configured
9. Collider deactivates after `duration`

#### Enemy AI Behavior
- Within `range_Enemy`: Attack player
- Distance < 20 units: Move toward player
- Distance > 20 units: Move toward portal
- Distance to portal < 4 units: Enemy survives (player loses a life)

#### Movement System
Both player and enemies use:
- `Rigidbody.MovePosition()` for physics-based movement
- `transform.LookAt()` for rotation toward targets
- Movement can be toggled via `heroMovementOnOff()` / `enemyMovementOnOff()` (used for charge/stun abilities)

### Scene Structure

Main scenes in `Assets/`:
- **level.unity**: Main gameplay scene
- **menu.unity**: Main menu

### Data Structures

#### Wave
Contains:
- `name`: Wave name displayed to player
- `count`: Number of enemies to spawn
- `enemyPrefab`: GameObject prefab to instantiate

#### Spell Hit Detection
Uses Lists to track hit enemies: `List<GameObject> enemiesHit`
- Prevents multiple hits from same spell cast
- Cleared via `enemiesHitList()` at spell activation
- Refreshed for continuous damage spells

### Object Pooling & Lifecycle

Enemies and effects are not pooled - they use position shifting (`transform.position = new Vector3(0, -100f, 0)`) instead of destruction to avoid instantiation overhead during waves.

### Important Tags
- `"Player"`: Player character
- `"opponent"`: Enemy units
- `"portal"`: Enemy goal destination
- `"levelManager"`: LevelManager GameObject
- `"Wall"`: Collision boundaries

### Animation System
- Uses Unity Animator controllers
- Common animations: "Attack", "Walk", "Idle", "Die", "SpecialAbility_Prepare", "SpecialAbility_Finish"
- Triggered via `GetComponent<Animator>().Play()`

### UI System
- Spell slots: 3 slots for equipped spells
- Virtual joystick for movement
- Health bars for player and enemies (using UI Slider/Image fillAmount)
- Wave counter and timer display
- Lives counter (enemies survived / max lives)

## Common Development Patterns

### Adding a New Spell
1. Add spell function to `Spellbook.cs`
2. Create timestamp variable: `private float tsSpellName = 0f;`
3. Create ParticleSystem field: `[SerializeField] private ParticleSystem spellNamePS;`
4. Create collider GameObject field: `[SerializeField] private GameObject SpellName_Collider;`
5. Implement spell function following existing pattern (check cooldown, activate collider, play particle system, call HitBoxCheck)
6. Configure HitBoxCheck component on spell collider with damage, slow, lifesteal, ticks, etc.
7. Wire up spell to UI button/slot

### Modifying Enemy Behavior
- Enemy stats (health, damage, range) are configured in Enemy.cs inspector
- Movement logic in `EnemyMovement.FixedUpdate()`
- Special abilities configured via Enemy.cs fields: `specialAbility`, `castTime`, `Enemy_SpecialAttack`

### Debugging Tips
- LevelManager uses `deathCount` to track wave completion - ensure `LevelManager.deadEnemy()` is called on enemy death
- Spell colliders must call `enemiesHitList()` to initialize before detecting hits
- Check `muteAbilities` flag in Spellbook - some spells (like Spin) disable other abilities during cast
- Enemy movement toggles off during special ability cast times

## File Organization

```
Assets/
├── Scripts/           # All C# game logic
├── Prefabs/          # Reusable game objects (buttons, heroes)
├── Heroes/           # Hero character models (.blend files)
├── Minions/          # Enemy character models
├── Materials/        # Textures and materials for spells/effects
├── Projectiles/      # Spell projectile assets and animations
├── Spells/           # Spell-specific assets
├── UI/               # UI sprites and elements
├── menu/             # Menu scene assets
├── Scenes/           # Unity scene files
├── MapObjects/       # Level environment objects
└── level.unity       # Main gameplay scene
```

## Known Patterns and Conventions

- Cooldowns use `Time.time` timestamp comparison pattern
- Health bars calculate as `(current / max)` for 0-1 range
- Scene management uses `SceneManager.LoadScene()`
- Game objects positioned at large negative Y coordinates (< -50) are effectively "destroyed"
- Inspector fields marked `[SerializeField]` for Unity editor configuration
- Boolean toggles for game object activation use `SetActive(!activeInHierarchy)` pattern
