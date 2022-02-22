# Boom Blox
A simple block destroying project to practice procedurally generated blocks, how sounds are created, and taking advantage of Unity's Input System.

# How to play
Use WASD to pan the scene.
Hold down left mouse button to adjust projectile velocity.
Release to shoot.
Right Click to reset scene.

# Code
Using Unity's Input system to take advantage of multi-platform support and easy to map for custom/user created controls. 
Using Unity's TextMeshPro for a much clear and sharper font style.
Code have been abstract/simplified down to rely on event system. Each script expose an type action delegate that updates only when the value has changed. This would trigger any UI/non-dependency objects to update the new value upon request.
