[33mcommit ddd3a4a26d52f9c90ee83c4d4c074bb474eca4e4[m[33m ([m[1;31morigin/master[m[33m, [m[1;31morigin/HEAD[m[33m)[m
Author: Peet <rajputpreet72@gmail.com>
Date:   Sun Mar 8 01:15:03 2026 +0530

    Initialize README with project overview and details
    
    Added detailed project description, core mechanics, technology stack, and future features for HatchMate.

[1mdiff --git a/README.md b/README.md[m
[1mnew file mode 100644[m
[1mindex 0000000..c2c6519[m
[1m--- /dev/null[m
[1m+++ b/README.md[m
[36m@@ -0,0 +1,145 @@[m
[32m+[m[32m# HatchMate[m
[32m+[m
[32m+[m[32mHatchMate is a productivity-driven virtual pet application where users grow a digital pet by completing real-life tasks. Each completed task rewards experience points that help the pet evolve through different levels.[m
[32m+[m
[32m+[m[32mThe goal is to combine **task management** with **gamification**, encouraging users to stay consistent with their daily goals.[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Concept[m
[32m+[m
[32m+[m[32mUsers create tasks they want to complete during the day.[m
[32m+[m[32mWhen a task is marked as completed, the user earns experience points (XP).[m
[32m+[m[32mAccumulated XP helps the pet grow and level up.[m
[32m+[m
[32m+[m[32mThis turns productivity into a progression system similar to a game.[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Core Mechanics[m
[32m+[m
[32m+[m[32m### Task System[m
[32m+[m
[32m+[m[32mUsers can create any number of tasks.[m
[32m+[m
[32m+[m[32mEach task contains:[m
[32m+[m
[32m+[m[32m* Task ID[m
[32m+[m[32m* User ID[m
[32m+[m[32m* Task description[m
[32m+[m[32m* Completion status[m
[32m+[m[32m* XP granted flag[m
[32m+[m
[32m+[m[32mWhen a task is completed:[m
[32m+[m
[32m+[m[32m* The user earns **10 XP**[m
[32m+[m[32m* XP is only granted **once per task**[m
[32m+[m
[32m+[m[32mIf a task is unchecked later, the XP **remains granted**.[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m### Daily XP Limit[m
[32m+[m
[32m+[m[32mTo prevent abuse, a daily cap exists.[m
[32m+[m
[32m+[m[32mRules:[m
[32m+[m
[32m+[m[32m* 10 XP per completed task[m
[32m+[m[32m* Maximum **100 XP per day**[m
[32m+[m[32m* Maximum **10 rewarded tasks per day**[m
[32m+[m
[32m+[m[32mThe user can still complete more tasks, but **no additional XP is granted after the limit is reached**.[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m### Experience System[m
[32m+[m
[32m+[m[32mXP contributes to the pet’s progression.[m
[32m+[m
[32m+[m[32mExample progression:[m
[32m+[m
[32m+[m[32m| Level | XP Required |[m
[32m+[m[32m| ----- | ----------- |[m
[32m+[m[32m| 1 → 2 | 1000 XP     |[m
[32m+[m[32m| 2 → 3 | 1500 XP     |[m
[32m+[m[32m| 3 → 4 | 2000 XP     |[m
[32m+[m
[32m+[m[32mThe pet grows as total experience increases.[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m### Pet Growth[m
[32m+[m
[32m+[m[32mEach level unlocks a new stage of the pet.[m
[32m+[m
[32m+[m[32mGrowth stages may include:[m
[32m+[m
[32m+[m[32m* Visual evolution[m
[32m+[m[32m* Animation changes[m
[32m+[m[32m* New abilities[m
[32m+[m[32m* Cosmetic changes[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Technology Stack[m
[32m+[m
[32m+[m[32mFrontend[m
[32m+[m
[32m+[m[32m* .NET MAUI[m
[32m+[m
[32m+[m[32mBackend[m
[32m+[m
[32m+[m[32m* ASP.NET Core Web API[m
[32m+[m
[32m+[m[32mDatabase[m
[32m+[m
[32m+[m[32m* MongoDB Atlas[m
[32m+[m
[32m+[m[32mOther Tools[m
[32m+[m
[32m+[m[32m* REST APIs[m
[32m+[m[32m* JSON serialization[m
[32m+[m[32m* MVVM architecture[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Example Flow[m
[32m+[m
[32m+[m[32m1. User creates tasks[m
[32m+[m[32m2. User completes a task[m
[32m+[m[32m3. Backend verifies:[m
[32m+[m
[32m+[m[32m   * Task has not already granted XP[m
[32m+[m[32m   * Daily XP limit not reached[m
[32m+[m[32m4. User receives 10 XP[m
[32m+[m[32m5. Pet XP increases[m
[32m+[m[32m6. If XP threshold is reached → pet levels up[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Project Goals[m
[32m+[m
[32m+[m[32m* Encourage daily productivity[m
[32m+[m[32m* Apply game mechanics to habit building[m
[32m+[m[32m* Build a scalable backend for task tracking and progression[m
[32m+[m[32m* Practice full-stack development with MAUI and APIs[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## Future Features[m
[32m+[m
[32m+[m[32mPossible improvements:[m
[32m+[m
[32m+[m[32m* Pet animations[m
[32m+[m[32m* Achievements[m
[32m+[m[32m* Streak system[m
[32m+[m[32m* Multiple pets[m
[32m+[m[32m* Leaderboards[m
[32m+[m[32m* Habit tracking analytics[m
[32m+[m
[32m+[m[32m---[m
[32m+[m
[32m+[m[32m## License[m
[32m+[m
[32m+[m[32mThis project is currently experimental and built for learning and development purposes.[m
