#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class GizmosMona : MonoBehaviour
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void drawReactor(Transform transform, GizmoType gizmoType)
    {
        Mona.MonaReactor reactor = transform.GetComponent<Mona.MonaReactor>();
        if (reactor == null)
        {
            return;
        }

        Gizmos.color = Color.magenta * 0.6f;

        // Draw a arrow towards the reactor's target
        if (reactor.OnEnterTrigger != null)
        {
            if (reactor.OnEnterTrigger.Length != 0)
            {
                for (int i = 0; i < reactor.OnEnterTrigger.Length; i++)
                {
                    GameObject rObject = reactor.OnEnterTrigger[i].Object;
                    if (rObject != null)
                    {
                        // Draw a line to the object and the reactor
                        Gizmos.DrawLine(transform.position, rObject.transform.position);
                        Gizmos.DrawIcon(rObject.transform.position, "hooked", true);
                    }
                }
            }
        }

        if (reactor.OnExitTrigger != null)
        {
            if (reactor.OnExitTrigger.Length != 0)
            {
                for (int i = 0; i < reactor.OnExitTrigger.Length; i++)
                {
                    GameObject rObject = reactor.OnExitTrigger[i].Object;
                    if (rObject != null)
                    {
                        // Draw a line to the object and the reactor
                        Gizmos.DrawLine(transform.position, rObject.transform.position);
                        Gizmos.DrawIcon(rObject.transform.position, "hooked", true);
                    }
                }
            }
        }

        // Draw icon from Resources folder
        var icon = Resources.Load("Editor/Reactor") as Texture2D;
        Gizmos.DrawIcon(transform.position, "Reactor", true);

        // Draw the outline of the box collider
        var collider = transform.GetComponent<BoxCollider>();
        if (collider != null)
        {
            Gizmos.color = Color.magenta * 0.4f;
            Gizmos.DrawWireCube(collider.center + transform.position, collider.size);
        }


    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void drawGizmo(Transform transform, GizmoType gizmoType)
    {

        if (transform.tag != "SpawnPoint" || transform.gameObject.scene.name.Equals("Artifacts"))
        {
            return;
        }

        // Raycast check if ground is valid
        bool spawnNotOK = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000f))
        {
            if (hit.collider.gameObject.layer != 0)
            {
                spawnNotOK = true;
            }
            else
            {
                Handles.color = Color.green;
                Gizmos.color = Color.green;
                Handles.DrawWireDisc(hit.point, Vector3.up, 0.5f);
                Gizmos.DrawLine(transform.position, hit.point);
            }
        }
        else
        {
            spawnNotOK = true;
            Handles.color = Color.red;
        }

        // Check capsule collision so we don't spawn inside a wall
        if (Physics.CheckCapsule(transform.position + Vector3.up * 0.28f, transform.position + Vector3.up * 1.58f, 0.28f))
        {
            spawnNotOK = true;
        }

        if (transform.gameObject.scene.name.Equals("Space"))
        {
            Gizmos.color = Color.blue * 0.7f;
        }
        else
        {
            Gizmos.color = Color.green * 0.7f;
        }

        if (spawnNotOK)
        {
            Handles.color = Color.red;
            Gizmos.color = Color.red;
        }

        Gizmos.DrawMesh(
            Resources.Load<Mesh>("Editor/Avatar"),
            transform.position,
            transform.rotation,
            new Vector3(1, 1, 1)
        );

        // Draw capsule
        if (!spawnNotOK)
        {
            Handles.color = Color.blue;
        }
        float offset = 0.93f;
        float height = 0.65f;
        Vector3 pos = transform.position;
        pos.y += offset;
        DrawWireCapsule(pos + new Vector3(0, height, 0), pos - new Vector3(0, height, 0), 0.28f);

        // Draw yellow line arrow
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pos, pos + transform.forward * 0.7f);
        Gizmos.DrawLine(pos + transform.right * 0.3f + transform.forward * 0.4f, pos + transform.forward * 0.7f);
        Gizmos.DrawLine(pos + transform.right * -0.3f + transform.forward * 0.4f, pos + transform.forward * 0.7f);

    }

    static void DrawWireCapsule(Vector3 upper, Vector3 lower, float radius)
    {

        var offsetCenter = Vector3.Distance(upper, lower);
        var offsetX = new Vector3(radius, 0f, 0f);
        var offsetZ = new Vector3(0f, 0f, radius);

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
}
#endif