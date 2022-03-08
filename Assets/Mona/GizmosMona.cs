#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class GizmosMona : MonoBehaviour
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawSceneObjects(Transform transform, GizmoType gizmoType)
    {
        Mona.MonaReactor _reactor = transform.GetComponent<Mona.MonaReactor>();
        Mona.PlayerPropertiesVolume _ppv = transform.GetComponent<Mona.PlayerPropertiesVolume>();

        if (_reactor == null && _ppv == null) return;

        if (_reactor != null)
        {
            Gizmos.color = Color.magenta * 0.6f;

            DrawHooks(_reactor.OnEnterTrigger, transform);
            DrawHooks(_reactor.OnExitTrigger, transform);

            // Draw icon from Resources folder
            Gizmos.DrawIcon(transform.position, "Reactor", true);
        }

        Gizmos.color = Color.yellow * 0.6f;
        Gizmos.DrawIcon(transform.position, "PPV", true);

        // Draw the outline of the box collider
        BoxCollider _collider = transform.GetComponent<BoxCollider>();
        if (_collider != null) return;
        Gizmos.DrawWireCube(_collider.center + transform.position, _collider.size);

    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawGizmo(Transform transform, GizmoType gizmoType)
    {
        if (transform.tag != "SpawnPoint" || transform.gameObject.scene.name.Equals("Artifacts")) return;

        // Raycast check if ground is valid
        bool _spawnNotOK = false;
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 1000f))
        {
            if (_hit.collider.gameObject.layer != 0)
            {
                _spawnNotOK = true;
            }
            else
            {
                Handles.color = Color.green;
                Gizmos.color = Color.green;
                Handles.DrawWireDisc(_hit.point, Vector3.up, 0.5f);
                Gizmos.DrawLine(transform.position, _hit.point);
            }
        }
        else
        {
            _spawnNotOK = true;
            Handles.color = Color.red;
        }

        // Check capsule collision so we don't spawn inside a wall
        if (Physics.CheckCapsule(transform.position + Vector3.up * 0.28f, transform.position + Vector3.up * 1.58f, 0.28f))
        {
            _spawnNotOK = true;
        }

        if (transform.gameObject.scene.name.Equals("Space"))
        {
            Gizmos.color = Color.blue * 0.7f;
        }
        else
        {
            Gizmos.color = Color.green * 0.7f;
        }

        if (_spawnNotOK)
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
        if (!_spawnNotOK)
        {
            Handles.color = Color.blue;
        }

        float _offset = 0.93f;
        float _height = 0.65f;
        Vector3 pos = transform.position;
        pos.y += _offset;
        DrawWireCapsule(pos + new Vector3(0, _height, 0), pos - new Vector3(0, _height, 0), 0.28f);
    }

    static void DrawWireCapsule(Vector3 upper, Vector3 lower, float radius)
    {
        Vector3 _offsetX = new Vector3(radius, 0f, 0f);
        Vector3 _offsetZ = new Vector3(0f, 0f, radius);

        //draw frontways
        Handles.DrawWireArc(upper, Vector3.back, Vector3.left, 180, radius);
        Handles.DrawLine(lower + _offsetX, upper + _offsetX);
        Handles.DrawLine(lower - _offsetX, upper - _offsetX);
        Handles.DrawWireArc(lower, Vector3.back, Vector3.left, -180, radius);

        //draw sideways
        Handles.DrawWireArc(upper, Vector3.left, Vector3.back, -180, radius);
        Handles.DrawLine(lower + _offsetZ, upper + _offsetZ);
        Handles.DrawLine(lower - _offsetZ, upper - _offsetZ);
        Handles.DrawWireArc(lower, Vector3.left, Vector3.back, 180, radius);

        //draw center
        Handles.DrawWireDisc(upper, Vector3.up, radius);
        Handles.DrawWireDisc(lower, Vector3.up, radius);
    }
    
    // Draws all connections between MonaReactors
    static void DrawHooks(Mona.MonaEvent[] events, Transform transform)
    {
        foreach (Mona.MonaEvent _monaEvent in events)
        {
            if (_monaEvent.Object != null)
            {
                // Draw a line to the object and the reactor
                Gizmos.DrawLine(transform.position, _monaEvent.Object.transform.position);
                Gizmos.DrawIcon(_monaEvent.Object.transform.position, "hooked", true);
            }
        }
    }
}
#endif