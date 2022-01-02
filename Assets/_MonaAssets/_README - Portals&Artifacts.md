# Portals & Artifacts in MoNA

---------------
Portals
---------------
Portals are custom objects that link to Spaces in the Mona Network.
The Space requires at least one Portal.

Portal object hierarchy is as follows :
1. Portal Prefab 
- Must be under Portals Scene
- Asset is in Project / MonaAssets

2. Name of Portal
- Must have the Portal tag (found at the top right of the Inspector)
- Must have a unique name
- Must have a collider on it for the player to 'see' it

3. SpawnPoint
- Used when linking from other Spaces

4. Assets
- Put all custom game assets in here
- Colliders can be on this game object or on the objects themselves

----------------
SpawnPoint
----------------
If you delete the SpawnPoint in the scene, Portals or Artifacts, you can just drag and drop this prefab to replace it.

----------------
Artifacts
----------------
An Artifact is a custom object that can link to 
an external website.
Your space does not require any Artifacts.

Artifact Hierarchy is as follows :
1. Artifact Prefab 
- Must be under Artifacts scene
- Found in Project / MonaAssets

2. Name of Artifact
- Must have the Artifact tag (found at the top right of the Inspector)
- Must have a unique name
- Must have collider for the player to 'see' it

3. SpawnPoint
- Functionality coming soon

4. Assets
- Put all custom portal assets in here
- Colliders can be on this game object or on the objects themselves
