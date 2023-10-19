
using System;
using System.Collections.Generic;

namespace GodotAssetLibrary.Application
{
    // For enums that need to associate with strings, you might need a different approach, such as a class with constant strings or a method to retrieve the string based on the enum value.
    public static class CategoryType
    {
        public const string Addon = "0"; // corresponds to 'addon' => '0'
        public const string Project = "1"; // corresponds to 'project' => '1'
        public const string Any = "%"; // corresponds to 'any' => '%'
    }

    public static class Constants
    {
        // Mimic the 'double_map' functionality from PHP
        private static Dictionary<object, object> DoubleMap(IDictionary<object, object> dict)
        {
            var newDict = new Dictionary<object, object>();
            foreach (var kvp in dict)
            {
                newDict[kvp.Key] = kvp.Value;
                newDict[kvp.Value.ToString()] = kvp.Key;
                newDict[kvp.Value] = kvp.Key;
            }
            return newDict;
        }

        public static readonly Dictionary<object, object> EditStatus = DoubleMap(new Dictionary<object, object>
    {
        {"new", 0},
        {"in_review", 1},
        {"accepted", 2},
        {"rejected", 3}
    });

        // ... Add other fields similarly ...

        public static readonly List<string> AssetEditFields = new List<string>
    {
        "title", "description", "category_id", "godot_version",
        "version_string", "cost",
        "download_provider", "download_commit", "browse_url", "issues_url", "icon_url",
    };

        public static readonly List<string> AssetEditPreviewFields = new List<string>
    {
        "type", "link", "thumbnail",
    };

        public static readonly Dictionary<object, object> SpecialGodotVersions = DoubleMap(new Dictionary<object, object>
    {
        {0, "unknown"},
        {9999999, "custom_build"}
    });

        public static readonly List<string> CommonGodotVersions = new List<string>
    {
        "2.0", "2.1", "2.2", "3.0", "3.1", "3.2", "3.3", "3.4", "3.5", "4.0", "4.1", "4.2", "unknown", "custom_build"
    };

        public static readonly Dictionary<string, string> Licenses = new Dictionary<string, string>
    {
        {"MIT", "MIT"},
        // ... other licenses ...
    };

        // Initialize the remaining constants similarly
    }
}
