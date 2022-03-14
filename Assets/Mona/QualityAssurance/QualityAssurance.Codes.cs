#if UNITY_EDITOR
using System.Collections.Generic;

public static class ErrorDescriptions
{
    // Missing Layer Errors
    public static string MISSING_SPACE_LAYER = "You're missing a Space layer";
    public static string MISSING_ARTIFACT_LAYER = "You're missing the Artifact Layer";
    public static string MISSING_PORTAL_LAYER = "You're missing the Portal Layer";

    // Missing Scene Errors
    public static string MISSING_SPACE_SCENE = "You're missing the Space scene";
    public static string MISSING_PORTAL_SCENE = "You're missing the Portal scene";
    public static string MISSING_ARTIFACT_SCENE = "You're missing the Artifact Scene";

    // Multiple Root Object Errors
    public static string MULTIPLE_SPACE_ROOTS = "You have more than one root object in the Space scene";
    public static string MULTIPLE_ARTIFACT_ROOTS = "Your Artifact Layer has more than one root object";
    public static string MULTIPLE_PORTAL_ROOTS = "You have multiple root objects in your Portal scene";

    // Object Placement errors
    public static string BAD_ARTIFACT_PLACEMENT = "One of your Artifacts is outside of the Artifact Layer";
    public static string BAD_CANVAS_PLACEMENT = "One of your Canvases is outside of the Artifact Layer";
    public static string BAD_PORTAL_PLACEMENT = "One of your Portals is outside of the PortalLayer";

    // Duplicate Name Errors
    public static string DUPLICATE_ARTIFACT_NAME = "Two or more of your Artifacts have the same GameObject name";
    public static string DUPLICATE_CANVAS_NAME = "Two or more of your Canvases have the same GameObject name";
    public static string DUPLICATE_PORTAL_NAME = "Two or more of your Portals have the same GameObject name";

    // Layer Content Errors
    public static string BAD_PORTAL_LAYER_CONTENTS = "You have non-Portal objects in your Portal Layer";

    // Collider Errors
    public static string BAD_PORTAL_COLLIDER = "There is a problem with the collider on one of your Portals";
    public static string BAD_ARTIFACT_COLLIDER = "There is a problem with the collider on one of your Artifacts";
    public static string BAD_CANVAS_COLLIDER = "There is a problem with the collider on one of your Canvases";
}

public static class MonaErrorCodes
{
    // Missing Layer Errors
    public static string MISSING_SPACE_LAYER = "space.missing-layer";
    public static string MISSING_ARTIFACT_LAYER = "artifact.missing-layer";
    public static string MISSING_PORTAL_LAYER = "portal.missing-layer";

    // Missing Scene Errors
    public static string MISSING_SPACE_SCENE = "space.missing-scene";
    public static string MISSING_PORTAL_SCENE = "portal.missing-scene";
    public static string MISSING_ARTIFACT_SCENE = "artifact.missing-scene";

    // Multiple Root Object Errors
    public static string MULTIPLE_SPACE_ROOTS = "space.multiple-roots";
    public static string MULTIPLE_ARTIFACT_ROOTS = "artifact.multiple-roots";
    public static string MULTIPLE_PORTAL_ROOTS = "portal.multiple-roots";

    // Object Placement errors
    public static string BAD_ARTIFACT_PLACEMENT = "artifact.bad-placement";
    public static string BAD_CANVAS_PLACEMENT = "canvas.bad-placement";
    public static string BAD_PORTAL_PLACEMENT = "portal.bad-placement";

    // Duplicate Name Errors
    public static string DUPLICATE_ARTIFACT_NAME = "artifact.duplicate-name";
    public static string DUPLICATE_CANVAS_NAME = "canvas.duplicate-name";
    public static string DUPLICATE_PORTAL_NAME = "portal.duplicate-name";

    // Layer Content Errors
    public static string BAD_PORTAL_LAYER_CONTENTS = "portal.layer-contents";

    // Collider Errors
    public static string BAD_PORTAL_COLLIDER = "portal.bad-collider";
    public static string BAD_ARTIFACT_COLLIDER = "artifact.bad-collider";
    public static string BAD_CANVAS_COLLIDER = "canvas.bad-collider";

    public static Dictionary<string, string> GetErrorDescriptionMap()
    {
        return new Dictionary<string, string> {
            { MonaErrorCodes.MISSING_SPACE_LAYER, ErrorDescriptions.MISSING_SPACE_LAYER },
            { MonaErrorCodes.MISSING_ARTIFACT_LAYER, ErrorDescriptions.MISSING_ARTIFACT_LAYER },
            { MonaErrorCodes.MISSING_PORTAL_LAYER, ErrorDescriptions.MISSING_PORTAL_LAYER },
            { MonaErrorCodes.MISSING_SPACE_SCENE, ErrorDescriptions.MISSING_SPACE_SCENE },
            { MonaErrorCodes.MISSING_ARTIFACT_SCENE, ErrorDescriptions.MISSING_ARTIFACT_SCENE },
            { MonaErrorCodes.MISSING_PORTAL_SCENE, ErrorDescriptions.MISSING_PORTAL_SCENE },
            { MonaErrorCodes.MULTIPLE_SPACE_ROOTS, ErrorDescriptions.MULTIPLE_SPACE_ROOTS },
            { MonaErrorCodes.MULTIPLE_ARTIFACT_ROOTS, ErrorDescriptions.MULTIPLE_ARTIFACT_ROOTS },
            { MonaErrorCodes.MULTIPLE_PORTAL_ROOTS, ErrorDescriptions.MULTIPLE_PORTAL_ROOTS },
            { MonaErrorCodes.BAD_ARTIFACT_PLACEMENT, ErrorDescriptions.BAD_ARTIFACT_PLACEMENT },
            { MonaErrorCodes.BAD_PORTAL_PLACEMENT, ErrorDescriptions.BAD_PORTAL_PLACEMENT },
            { MonaErrorCodes.BAD_CANVAS_PLACEMENT, ErrorDescriptions.BAD_CANVAS_PLACEMENT },
            { MonaErrorCodes.DUPLICATE_ARTIFACT_NAME, ErrorDescriptions.DUPLICATE_ARTIFACT_NAME },
            { MonaErrorCodes.DUPLICATE_PORTAL_NAME, ErrorDescriptions.DUPLICATE_PORTAL_NAME },
            { MonaErrorCodes.DUPLICATE_CANVAS_NAME, ErrorDescriptions.DUPLICATE_CANVAS_NAME },
            { MonaErrorCodes.BAD_PORTAL_LAYER_CONTENTS, ErrorDescriptions.BAD_PORTAL_LAYER_CONTENTS },
            { MonaErrorCodes.BAD_PORTAL_COLLIDER, ErrorDescriptions.BAD_PORTAL_COLLIDER },
            { MonaErrorCodes.BAD_ARTIFACT_COLLIDER, ErrorDescriptions.BAD_ARTIFACT_COLLIDER },
            { MonaErrorCodes.BAD_CANVAS_COLLIDER, ErrorDescriptions.BAD_CANVAS_COLLIDER }
        };
    }
}
#endif