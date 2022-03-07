public static class ErrorDescriptions
{
    // Missing Layer Errors
    public static string MISSING_SPACE_LAYER_DESCRIPTION = "You're missing a Space layer";
    public static string MISSING_ARTIFACT_LAYER_DESCRIPTION = "You're missing the Artifact Layer";
    public static string MISSING_PORTAL_LAYER_DESCRIPTION = "You're missing the Portal Layer";

    // Missing Scene Errors
    public static string MISSING_SPACE_SCENE_DESCRIPTION = "You're missing the Space scene";
    public static string MISSING_PORTAL_SCENE_DESCRIPTION = "You're missing the Portal scene";
    public static string MISSING_ARTIFACT_SCENE_DESCRIPTION = "You're missing the Artifact Scene";

    // Multiple Root Object Errors
    public static string MULTIPLE_SPACE_ROOTS_DESCRIPTION = "You have more than one root object in the Space scene";
    public static string MULTIPLE_ARTIFACT_ROOTS_DESCRIPTION = "Your Artifact Layer has more than one root object";
    public static string MULTIPLE_PORTAL_ROOTS_DESCRIPTION = "You have multiple root objects in your Portal scene";

    // Object Placement errors
    public static string BAD_ARTIFACT_PLACEMENT_DESCRIPTION = "One of your Artifacts is outside of the Artifact Layer";
    public static string BAD_CANVAS_PLACEMENT_DESCRIPTION = "One of your Canvases is outside of the Artifact Layer";
    public static string BAD_PORTAL_PLACEMENT_DESCRIPTION = "One of your Portals is outside of the PortalLayer";

    // Duplicate Name Errors
    public static string DUPLICATE_ARTIFACT_NAME_DESCRIPTION = "Two or more of your Artifacts have the same GameObject name";
    public static string DUPLICATE_CANVAS_NAME_DESCRIPTION = "Two or more of your Canvases have the same GameObject name";
    public static string DUPLICATE_PORTAL_NAME_DESCRIPTION = "Two or more of your Portals have the same GameObject name";

    // Layer Content Errors
    public static string BAD_PORTAL_LAYER_CONTENTS_DESCRIPTION = "You have non-Portal objects in your Portal Layer";

    // Collider Errors
    public static string BAD_PORTAL_COLLIDER_DESCRIPTION = "There is a problem with the collider on one of your Portals";
    public static string BAD_ARTIFACT_COLLIDER_DESCRIPTION = "There is a problem with the collider on one of your Artifacts";
    public static string BAD_CANVAS_COLLIDER_DESCRIPTION = "There is a problem with the collider on one of your Canvases";
}