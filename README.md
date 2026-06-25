# 🎡 Vertigo Games - Wheel of Fortune (Demo)

A Wheel of Fortune-style risk & reward game developed as part of the Vertigo Games Game Developer assessment.

---

## 🎥 Gameplay Video

[![Wheel of Fortune Gameplay](https://img.youtube.com/vi/pnUFxZCAMfk/0.jpg)](https://youtu.be/pnUFxZCAMfk)

---

## 📸 Screenshots

### Wheel Spin (20:9)
![Wheel Spin](InGamePictures/golden_spin_20_9.png)

### Wheel Spin (16:9)
![Wheel Spin](InGamePictures/silver_spin_16_9.png)

### Wheel Spin (4:3)
![Wheel Spin](InGamePictures/bronze_spin_4_3.png)

### Bomb Screen (4:3)
![Wheel Spin](InGamePictures/bomb_screen_4_3.png)

---

## Additional Screenshots

Additional gameplay screenshots are available in the /InGamePictures folder.

🎮 Core Gameplay Features
- Dynamic wheel spin system with deterministic zone logic
- Zone-based progression with increasing risk/reward scaling
- Bomb mechanic that resets progress on failure
- Safe Zones (every 5th zone, no-bomb guaranteed spins)
- Super Zones (every 30th zone, high reward/high risk)
- Player decision system (walk-away before spin option)
- Inventory system for collected rewards
- Rewarded continue system (optional ad-based revive)
- Currency-based revive system after failure

⚙️ Game Systems
- ScriptableObject-driven configuration system
- Modular wheel & zone architecture
- Configurable reward and progression pipeline
- Clean separation between UI, gameplay logic, and data layers
- DOTween-based animation system for smooth transitions
- Responsive UI supporting multiple aspect ratios (20:9 / 16:9 / 4:3)

🧱 Architecture Overview
- Clean and reusable project structure
- SOLID principles applied where applicable
- Data-driven design using ScriptableObjects
- Decoupled systems for scalability and maintainability
- Centralized configuration for gameplay balancing
- Improved null safety and runtime stability in core systems

🛠 Tech Stack
- Unity 2021 LTS
- TextMeshPro
- DOTween
- ScriptableObjects
- New Input System

🚀 How to Run
- Open project in Unity 2021 LTS
- Load the main scene
- Press Play

📦 Build
- Android build available in the GitHub Releases section

📌 Notes
- UI is optimized for multiple aspect ratios (20:9, 16:9, 4:3)
- All systems are modular and extendable
- Designed with scalability, testability, and maintainability in mind
- Core gameplay logic is deterministic to ensure consistent outcomes across sessions
