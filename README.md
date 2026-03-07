# HatchMate

HatchMate is a productivity-driven virtual pet application where users grow a digital pet by completing real-life tasks. Each completed task rewards experience points that help the pet evolve through different levels.

The goal is to combine **task management** with **gamification**, encouraging users to stay consistent with their daily goals.

---

## Concept

Users create tasks they want to complete during the day.
When a task is marked as completed, the user earns experience points (XP).
Accumulated XP helps the pet grow and level up.

This turns productivity into a progression system similar to a game.

---

## Core Mechanics

### Task System

Users can create any number of tasks.

Each task contains:

* Task ID
* User ID
* Task description
* Completion status
* XP granted flag

When a task is completed:

* The user earns **10 XP**
* XP is only granted **once per task**

If a task is unchecked later, the XP **remains granted**.

---

### Daily XP Limit

To prevent abuse, a daily cap exists.

Rules:

* 10 XP per completed task
* Maximum **100 XP per day**
* Maximum **10 rewarded tasks per day**

The user can still complete more tasks, but **no additional XP is granted after the limit is reached**.

---

### Experience System

XP contributes to the pet’s progression.

Example progression:

| Level | XP Required |
| ----- | ----------- |
| 1 → 2 | 1000 XP     |
| 2 → 3 | 1500 XP     |
| 3 → 4 | 2000 XP     |

The pet grows as total experience increases.

---

### Pet Growth

Each level unlocks a new stage of the pet.

Growth stages may include:

* Visual evolution
* Animation changes
* New abilities
* Cosmetic changes

---

## Technology Stack

Frontend

* .NET MAUI

Backend

* ASP.NET Core Web API

Database

* MongoDB Atlas

Other Tools

* REST APIs
* JSON serialization
* MVVM architecture

---

## Example Flow

1. User creates tasks
2. User completes a task
3. Backend verifies:

   * Task has not already granted XP
   * Daily XP limit not reached
4. User receives 10 XP
5. Pet XP increases
6. If XP threshold is reached → pet levels up

---

## Project Goals

* Encourage daily productivity
* Apply game mechanics to habit building
* Build a scalable backend for task tracking and progression
* Practice full-stack development with MAUI and APIs

---

## Future Features

Possible improvements:

* Pet animations
* Achievements
* Streak system
* Multiple pets
* Leaderboards
* Habit tracking analytics

---

## License

This project is currently experimental and built for learning and development purposes.
