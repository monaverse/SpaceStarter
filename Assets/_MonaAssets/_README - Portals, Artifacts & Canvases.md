# Portals & Artifacts in MoNA

---------------
Portals
---------------
Portals are custom objects that link to Spaces in the Mona Network.
The Space requires at least one Portal.

Portal object hierarchy is as follows :
1. PortalPrefab_ (Parent Object)
- Must be under Portals Scene
- Can be renamed
- Asset is in Project / MonaAssets

2. Name of Portal
- Must have the Portal tag (found at the top left of the Inspector)
- Must have a unique name
- Must have a collider on it for the player to 'see' it
- Set 'IsTrigger' to true if you want to walk through it

3. SpawnPoint
- When another space links to this portal the player will spawn here.
- Must NOT be Red (Overlapping a collider or not above a collider)
- Must NOT be in/under the ground, a little above the ground is recommended

4. Assets
- Put all custom game assets in here
- Colliders can be on this game object or on the objects themselves
- Make sure that these colliders do not overlap the 'Portal' tagged object collider

----------------
SpawnPoint
----------------
If you delete the SpawnPoint in the scene, Portals or Artifacts, you can just drag and drop this prefab to replace it.

- Spawnpoint must NOT be Red (Overlapping a collider or not above a collider)
- Must not be in/under the ground, a little above the ground is recommended
- Be sure to put it in a logical position

----------------
Artifacts
----------------
The Artifacts Scene/Gameobject will be editable to some degree after the Space has been minted.

An Artifact is a custom object that can link to an external website.
Your space does not require any Artifacts but make sure to keep the Artifacts Parent Gameobject otherwise your space will fail QA.

Artifact Hierarchy is as follows :
1. ArtifactPrefab (Parent Object)
- Must be under Artifacts scene > Artifacts Gameobject
- Can be renamed
- Found in Project / MonaAssets

2. Name of Artifact
- Must have the Artifact tag (found at the top left of the Inspector)
- Must have a unique name
- Must have collider for the player to 'see' it
- Set 'IsTrigger' to true if you want to walk through it

3. SpawnPoint
- Future Functionality
- Must NOT be in/under the ground, a little above the ground is recommended

4. Assets
- Put all custom artifact assets in here
- Colliders can be on this game object or on the objects themselves
- Make sure that these colliders do not overlap the 'Artifact' tagged object collider

----------------
Canvases
----------------
A Canvas is a object that can link to an external website AND display external media which can be modified at any time.
Your space does not require any Canvases but make sure to keep the Artifacts Parent Gameobject otherwise your space will fail QA.

Canvas Hierarchy is as follows :
1. CanvasPrefab (Parent Object)
- Must be under Artifacts scene > Artifacts Gameobject
- Can be renamed
- Found in Project / MonaAssets

2. Name of Canvas
- Must have the Canvas tag (found at the top left of the Inspector)
- Must have a unique name
- Must have collider for the player to 'see' it
- Set 'IsTrigger' to true if you want to walk through it

3. SpawnPoint
- Future Functionality
- Must NOT be in/under the ground, a little above the ground is recommended

4. FrameAssets
- Put all custom frame assets in here
- Frames do NOT scale (with the autoscale option)
- Colliders can be on this game object or on the objects themselves
- Make sure that these colliders do not overlap the 'Canvas' tagged object collider