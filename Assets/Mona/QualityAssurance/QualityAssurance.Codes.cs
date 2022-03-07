using System.Collections.Generic;

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
            { MonaErrorCodes.MISSING_SPACE_LAYER, ErrorDescriptions.MISSING_SPACE_LAYER_DESCRIPTION },
            { MonaErrorCodes.MISSING_ARTIFACT_LAYER, ErrorDescriptions.MISSING_ARTIFACT_LAYER_DESCRIPTION},
            { MonaErrorCodes.MISSING_PORTAL_LAYER, ErrorDescriptions.MISSING_PORTAL_LAYER_DESCRIPTION},
            { MonaErrorCodes.MISSING_SPACE_SCENE, ErrorDescriptions.MISSING_SPACE_SCENE_DESCRIPTION},
            { MonaErrorCodes.MISSING_ARTIFACT_SCENE, ErrorDescriptions.MISSING_ARTIFACT_SCENE_DESCRIPTION},
            { MonaErrorCodes.MISSING_PORTAL_SCENE, ErrorDescriptions.MISSING_PORTAL_SCENE_DESCRIPTION},
            { MonaErrorCodes.MULTIPLE_SPACE_ROOTS, ErrorDescriptions.MULTIPLE_SPACE_ROOTS_DESCRIPTION},
            { MonaErrorCodes.MULTIPLE_ARTIFACT_ROOTS, ErrorDescriptions.MULTIPLE_ARTIFACT_ROOTS_DESCRIPTION},
            { MonaErrorCodes.MULTIPLE_PORTAL_ROOTS, ErrorDescriptions.MULTIPLE_PORTAL_ROOTS_DESCRIPTION},
            { MonaErrorCodes.BAD_ARTIFACT_PLACEMENT, ErrorDescriptions.BAD_ARTIFACT_PLACEMENT_DESCRIPTION},
            { MonaErrorCodes.BAD_PORTAL_PLACEMENT, ErrorDescriptions.BAD_PORTAL_PLACEMENT_DESCRIPTION},
            { MonaErrorCodes.BAD_CANVAS_PLACEMENT, ErrorDescriptions.BAD_CANVAS_PLACEMENT_DESCRIPTION},
            { MonaErrorCodes.DUPLICATE_ARTIFACT_NAME, ErrorDescriptions.DUPLICATE_ARTIFACT_NAME_DESCRIPTION},
            { MonaErrorCodes.DUPLICATE_PORTAL_NAME, ErrorDescriptions.DUPLICATE_PORTAL_NAME_DESCRIPTION},
            { MonaErrorCodes.DUPLICATE_CANVAS_NAME, ErrorDescriptions.DUPLICATE_CANVAS_NAME_DESCRIPTION},
            { MonaErrorCodes.BAD_PORTAL_LAYER_CONTENTS, ErrorDescriptions.BAD_PORTAL_LAYER_CONTENTS_DESCRIPTION},
            { MonaErrorCodes.BAD_PORTAL_COLLIDER, ErrorDescriptions.BAD_PORTAL_COLLIDER_DESCRIPTION},
            { MonaErrorCodes.BAD_ARTIFACT_COLLIDER, ErrorDescriptions.BAD_ARTIFACT_COLLIDER_DESCRIPTION},
            { MonaErrorCodes.BAD_CANVAS_COLLIDER, ErrorDescriptions.BAD_CANVAS_COLLIDER_DESCRIPTION}
        };
    }
}