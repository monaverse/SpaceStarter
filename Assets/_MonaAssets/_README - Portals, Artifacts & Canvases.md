# Portals

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
If you delete the SpawnPoint in the Scene, Portal, you can just drag and drop this prefab to replace it.

- Spawnpoint must NOT be Red (Overlapping a collider or not above a collider)
- Must not be in/under the ground, a little above the ground is recommended
- Be sure to put it in a logical position