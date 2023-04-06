#if UNITY_EDITOR
using System.Collections.Generic;

public static class ErrorDescriptions
{
    // Missing Layer Errors
    public static string MISSING_SPACE_LAYER = "You're missing a Space layer";
    public static string MISSING_PORTAL_LAYER = "You're missing the Portal Layer";

    // Missing Scene Errors
    public static string MISSING_SPACE_SCENE = "You're missing the Space scene";
    public static string MISSING_PORTAL_SCENE = "You're missing the Portal scene";

    // Multiple Root Object Errors
    public static string MULTIPLE_SPACE_ROOTS = "You have more than one root object in the Space scene";
    public static string MULTIPLE_PORTAL_ROOTS = "You have multiple root objects in your Portal scene";

    // Object Placement errors
    public static string BAD_PORTAL_PLACEMENT = "One of your Portals is outside of the PortalLayer";

    // Duplicate Name Errors
    public static string DUPLICATE_PORTAL_NAME = "Two or more of your Portals have the same GameObject name";

    // Layer Content Errors
    public static string BAD_PORTAL_LAYER_CONTENTS = "You have non-Portal objects in your Portal Layer";

    // Collider Errors
    public static string BAD_PORTAL_COLLIDER = "There is a problem with the collider on one of your Portals";
}

public static class MonaErrorCodes
{
    // Missing Layer Errors
    public static string MISSING_SPACE_LAYER = "space.missing-layer";
    public static string MISSING_PORTAL_LAYER = "portal.missing-layer";

    // Missing Scene Errors
    public static string MISSING_SPACE_SCENE = "space.missing-scene";
    public static string MISSING_PORTAL_SCENE = "portal.missing-scene";

    // Multiple Root Object Errors
    public static string MULTIPLE_SPACE_ROOTS = "space.multiple-roots";
    public static string MULTIPLE_PORTAL_ROOTS = "portal.multiple-roots";

    // Object Placement errors
    public static string BAD_PORTAL_PLACEMENT = "portal.bad-placement";

    // Duplicate Name Errors
    public static string DUPLICATE_PORTAL_NAME = "portal.duplicate-name";

    // Layer Content Errors
    public static string BAD_PORTAL_LAYER_CONTENTS = "portal.layer-contents";

    // Collider Errors
    public static string BAD_PORTAL_COLLIDER = "portal.bad-collider";

    public static Dictionary<string, string> GetErrorDescriptionMap()
    {
        return new Dictionary<string, string> {
            { MonaErrorCodes.MISSING_SPACE_LAYER, ErrorDescriptions.MISSING_SPACE_LAYER },
            { MonaErrorCodes.MISSING_PORTAL_LAYER, ErrorDescriptions.MISSING_PORTAL_LAYER },
            { MonaErrorCodes.MISSING_SPACE_SCENE, ErrorDescriptions.MISSING_SPACE_SCENE },
            { MonaErrorCodes.MISSING_PORTAL_SCENE, ErrorDescriptions.MISSING_PORTAL_SCENE },
            { MonaErrorCodes.MULTIPLE_SPACE_ROOTS, ErrorDescriptions.MULTIPLE_SPACE_ROOTS },
            { MonaErrorCodes.MULTIPLE_PORTAL_ROOTS, ErrorDescriptions.MULTIPLE_PORTAL_ROOTS },
            { MonaErrorCodes.BAD_PORTAL_PLACEMENT, ErrorDescriptions.BAD_PORTAL_PLACEMENT },
            { MonaErrorCodes.DUPLICATE_PORTAL_NAME, ErrorDescriptions.DUPLICATE_PORTAL_NAME },
            { MonaErrorCodes.BAD_PORTAL_LAYER_CONTENTS, ErrorDescriptions.BAD_PORTAL_LAYER_CONTENTS },
            { MonaErrorCodes.BAD_PORTAL_COLLIDER, ErrorDescriptions.BAD_PORTAL_COLLIDER },
        };
    }
}
#endif