# Simple Box Project

## OKR

To move a box in a 3D environment using the following methods:

1- Use the WASD keys on the keyboard in Unity's new input system (PlayerController.cs & Character Controller [1] ) and use the mouse to rotate the camera to see the surrounding environment (Cinemachine Free Look Camera [2] ).

2- Use the mouse to select a point and move the box to the desired point and rotate the box towards the selected point (AgentController.cs & Nav Mesh Agent [3] ).

3- Use directional buttons in Unity's UI system for movement and rotation (On-Screen Button [4] ).

 * Switch between these systems using the 1, 2, and 3 keys on the keyboard (InputManager.cs [5] ).

Note: In all cases above, the camera should follow the box's movement (Cinemachine Virtual Cameras [6] ).

## Explanations

[1]: Simple Player Controller code when enabled uses `movement value` from `CustomInputActions.Player.Movement` and `Main Camera Rotaion` to move our cube using `CharacterController.Move()` method.

[2]: Cinemachine Free Look Camera uses `look value` from `CustomInputActions.Player.Look` to provide u with this feature.

​	Further more if you have a look at `CustomInputActions.Player` you will find **two look actions**!, the reason is to separate `pointer delta value (mouse)` and `arrows value (used in UI buttons)` which will resolve these values interrupting each other when switching to input system #3.

[3]: Agent Controller when enabled check for a mouse click every frame then uses `Raycast` to cast a Rey from ScreenPoint downwards, and if was hit but something (in our case the surface), sets `nav mesh agent's destination` to achieve desired character movement and rotation.

[4]: The UI buttons are simple unity buttons which are mapped to trigger a specific action using Unity's new input system's `On-Screen Button Component`.

[5]: For better understanding of this script, here's the up scaled flow chart:


![InputManagerDiagram]([README\Images\InputManagerDiagram.png](https://github.com/shawnkasaei/badbadak-simple-box-test/blob/main/README/Images/InputManagerDiagram.png))
