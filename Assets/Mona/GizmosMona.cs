#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class GizmosMona : MonoBehaviour
{
    // Draws all the connections between the reactors & volumes of the player properties.
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawSceneObjects(Transform transform, GizmoType gizmoType)
    {
        if (transform == null) return;

        DrawReactor(transform, gizmoType);
        DrawPlayerPropertiesVolume(transform, gizmoType);
        DrawArtifactSpawnPoint(transform, gizmoType);
    }

    // Draws all spawn points of the space
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawPlayerSpawn(Transform transform, GizmoType gizmoType)
    {
        if (transform.tag != "SpawnPoint" || transform.gameObject.scene.name.Equals("Artifacts")) return;

        bool spawnObstructed = true;

        // Check capsule collision so we don't spawn inside a wall
        if (!Physics.CheckCapsule(transform.position + Vector3.up * MonaConstants.PlayerWidth, transform.position + Vector3.up * MonaConstants.PlayerColliderYOffset, MonaConstants.PlayerWidth, 1, QueryTriggerInteraction.Ignore))
        {
            spawnObstructed = false;
        }

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1000f))
        {
            spawnObstructed = true;
            hit.point = Vector3.down * 1000;
        }

        SetDrawingColor(
            spawnObstructed
            ? Color.red
            : transform.gameObject.scene.name.Equals("Space")
                ? Color.blue * 0.7f
                : Color.green * 0.7f
        );

        // Draw ground distance
        Handles.DrawWireDisc(hit.point, Vector3.up, 0.5f);
        Gizmos.DrawLine(transform.position, hit.point);

        // Draw player mesh, for point of scale reference
        Gizmos.DrawMesh(
            Resources.Load<Mesh>("Editor/Avatar"),
            transform.position,
            transform.rotation,
            new Vector3(1, 1, 1)
        );

        float height = 0.65f;
        Vector3 pos = transform.position;
        pos.y += MonaConstants.PlayerColliderYOffset;
        DrawWireCapsule(pos + new Vector3(0, height, 0), pos - new Vector3(0, height, 0), MonaConstants.PlayerWidth);
    }

    // Draws the reactor
    static void DrawReactor(Transform transform, GizmoType gizmoType)
    {
        Mona.MonaReactor reactor = transform.GetComponent<Mona.MonaReactor>();
        if (reactor == null) return;

        Gizmos.color = Color.magenta * 0.6f;

        DrawHooks(reactor.OnEnterTrigger, transform);
        DrawHooks(reactor.OnExitTrigger, transform);
        // DrawHooks(reactor.OnObjectEnable, transform);
        // DrawHooks(reactor.OnObjectDisable, transform);
        DrawHooks(reactor.OnPlayerInteract, transform);
        DrawHooks(reactor.OnPlayerLookStart, transform);
        DrawHooks(reactor.OnPlayerLookEnd, transform);

        Gizmos.DrawIcon(transform.position, "Reactor", true);
    }

    // Draws the properties volumes
    static void DrawPlayerPropertiesVolume(Transform transform, GizmoType gizmoType)
    {
        Mona.PlayerPropertiesVolume ppv = transform.GetComponent<Mona.PlayerPropertiesVolume>();
        if (ppv == null) return;

        Gizmos.color = Color.yellow * 0.6f;
        if (transform.gameObject.TryGetComponent<BoxCollider>(out BoxCollider collider))
        {
            Gizmos.DrawIcon(collider.center + transform.position, "PPV", true);
        }
    }

    // Draws the rotation of the artifact spawn point
    static void DrawArtifactSpawnPoint(Transform transform, GizmoType gizmoType)
    {
        if (transform.tag != "SpawnPoint" || !transform.gameObject.scene.name.Equals("Artifacts")) return;

        Gizmos.color = Color.red * 0.6f;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawWireCube(transform.position, new Vector3(0.3f, 0.3f, 0.3f));
    }

    // Draws all connections between MonaReactors
    static void DrawHooks(Mona.MonaEvent[] events, Transform transform)
    {
        if (events == null) return;
        foreach (Mona.MonaEvent monaEvent in events)
        {
            if (monaEvent.Object == null) continue;

            Gizmos.DrawLine(transform.position, monaEvent.Object.transform.position);
            Gizmos.DrawIcon(monaEvent.Object.transform.position, "hooked", true);
        }
    }

    // Draw capsule collider
    static void DrawWireCapsule(Vector3 upper, Vector3 lower, float radius)
    {
        Vector3 offsetX = new Vector3(radius, 0f, 0f);
        Vector3 offsetZ = new Vector3(0f, 0f, radius);

        //draw frontways
        Handles.DrawWireArc(upper, Vector3.back, Vector3.left, 180, radius);
        Handles.DrawLine(lower + offsetX, upper + offsetX);
        Handles.DrawLine(lower - offsetX, upper - offsetX);
        Handles.DrawWireArc(lower, Vector3.back, Vector3.left, -180, radius);

        //draw sideways
        Handles.DrawWireArc(upper, Vector3.left, Vector3.back, -180, radius);
        Handles.DrawLine(lower + offsetZ, upper + offsetZ);
        Handles.DrawLine(lower - offsetZ, upper - offsetZ);
        Handles.DrawWireArc(lower, Vector3.left, Vector3.back, 180, radius);

        //draw center
        Handles.DrawWireDisc(upper, Vector3.up, radius);
        Handles.DrawWireDisc(lower, Vector3.up, radius);
    }

    // Sets drawing color for in-editor gizmos
    static void SetDrawingColor(Color color)
    {
        Handles.color = color;
        Gizmos.color = color;
    }
}
#endif