# Softgames Unity Developer Assignment

Three interactive demos built with **Unity 6** demonstrating game development skills.

> **[Play Live Demo](https://shehabelgendy.itch.io/softgames)**

---

## Tasks

### 1. Ace of Shadows
Card deck animation with 144 stacked cards. Every second, the top card smoothly animates to another stack with counters and completion message.

### 2. Magic Words
Dynamic dialogue system loading data from a REST API. Supports emoji rendering, async avatar loading, and graceful error handling.

### 3. Phoenix Flame
Fire particle effect with UI-controlled color cycling (Orange → Green → Blue) using Animator Controller.

---

## Tech Stack

| Category | Technology |
|----------|------------|
| Engine | Unity 6 (6000.0.59f2) |
| DI Framework | VContainer 1.17.0 |
| Animation | DOTween |
| Rendering | Universal RP 2D |
| Build Target | WebGL |

---

## Architecture

```
Assets/_Project/
├── Scripts/
│   ├── Core/           # EventBus, DI, shared systems
│   ├── AceOfShadows/   # Card animation logic
│   ├── MagicWords/     # API & dialogue system
│   ├── PhoenixFlame/   # Particle color control
│   └── UI/             # Menu & navigation
├── Prefabs/
├── Config/             # ScriptableObjects
├── Scenes/             # 4 scenes (Menu + 3 tasks)
└── Tests/              # Unit tests
```

**Key Patterns:**
- Event-driven architecture (EventBus)
- Dependency Injection (VContainer)
- Interface-based design (SOLID)
- Pure C# models for testability

---

## Quick Start

### Requirements
- Unity 6.0.0+
- URP 2D Renderer

### Run
1. Open project in Unity
2. Load `Assets/_Project/Scenes/MainMenu.unity`
3. Press Play

### Build WebGL
1. File → Build Settings → WebGL
2. Set compression to Gzip
3. Build

---

## Testing

Run via **Window → General → Test Runner**

| Test Suite | Coverage |
|------------|----------|
| EventBusTests | Pub/sub messaging |
| CardStackTests | Stack operations |
| EmotionTagParserTests | Emoji tag parsing |
| ColorCycleModelTests | Color cycling |

---

## Controls

| Scene | Input |
|-------|-------|
| Main Menu | Click buttons to navigate |
| Ace of Shadows | Automatic animation |
| Magic Words | Scroll dialogue |
| Phoenix Flame | Click to cycle colors |

*Back button available in all scenes*

---

## License

Built for Softgames Unity Developer Assignment
