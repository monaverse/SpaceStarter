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
}