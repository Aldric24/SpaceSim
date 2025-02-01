# Space Simulator Readme

A pet project to test my skills at programming outer space physics charactersitics and orbital mechanics.

## Core Mechanics

### Spaceship Controls

The spaceship has two distinct control modes:

*   **Space Controls:** Used when the spaceship is not under the influence of a planet's gravity.
    *   W/S: Forward/Backward Thrust
    *   A/D: Left/Right Thrust
    *   Q/E: Roll Left/Right
    *   Mouse: Pitch/Yaw Rotation

*   **Gravity Controls:** Used when the spaceship is within a planet's gravitational pull.
    *   W/S: Pitch Down/Up
    *   A/D: Roll Left/Right
    *   Q/E: Yaw Left/Right
    *   Shift/Ctrl: Thrust Forward/Backward
    *   Space: Vertical Thrust
    *   Mouse: Camera Control

### Gravity

*   Planets have individual gravity fields that pull the spaceship towards them.
*   The strength of the gravitational pull decreases with distance from the planet.

### Collision Detection

*   The spaceship uses raycasting to detect and calculate the distance to planets.
*   This information is used for landing and displaying distance on the UI.


## Environments

The simulator includes the Sun and four planets with unique environmental characteristics:

### Earth 
*   Starting location for the player.


### Sun

*   The central star of the system.
*   Emits light and has a strong gravitational pull.
*   All Planets revolve around it

### Xylos

*   Weak gravity.

### Valka

*   Constant meteor shower.
*   Moderate gravity.

### Aethel

*   Ring of meteorites.
*   Weak gravity.

## Additional Features

*   Meteor showers can be triggered on planets.
*   The lifetime of meteors can be controlled.
*   UI elements display the spaceship's velocity and the current planet or "Space."
*   Gizmos are used to visualize gravity fields, orbits, and raycasts in the editor.

## Gameplay Footage 
https://drive.google.com/file/d/1Lx6eXEz9zlumJXJL3Vpb6JZjY1Vorq2X/view?usp=sharing
