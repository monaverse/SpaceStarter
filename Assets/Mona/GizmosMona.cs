#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// Draw an image above the light when the light is not selected
// The icon has to be stored in Assets/Gizmos
public class GizmosMona : MonoBehaviour {

    // Place a red sphere around a selected light.
    // Surround the sphere dark shaded when not selected.
    [DrawGizmo (GizmoType.Selected | GizmoType.NonSelected)]
    static void drawGizmo2 (Transform transform, GizmoType gizmoType ) {

        if(transform.tag != "SpawnPoint" || transform.gameObject.scene.name.Equals("Artifacts")){
            return;
        }

        if(transform.gameObject.scene.name.Equals("Space")){
            Gizmos.color = Color.blue * 0.7f;
        }else{
            Gizmos.color = Color.green * 0.7f;
        }

        Gizmos.DrawMesh(
            Resources.Load<Mesh>("Avatar"),
            transform.position,
            transform.rotation,
            new Vector3(1, 1, 1)
        );

        // Draw capsule
        Handles.color = Color.blue;
        float offset = 0.93f;
        float height = 0.65f;
        Vector3 pos = transform.position;
        pos.y += offset;
        DrawWireCapsule(pos + new Vector3(0, height, 0), pos - new Vector3(0, height, 0), 0.28f);
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