
using System;
using System.Collections.Generic;

namespace GodotAssetLibrary.Application
{
    public enum EditStatus
    {
        New = 0, // corresponds to 'new' => 0
        InReview = 1, // corresponds to 'in_review' => 1
        Accepted = 2, // corresponds to 'accepted' => 2
        Rejected = 3 // corresponds to 'rejected' => 3
    }

    public enum EditPreviewOperation
    {
        Insert = 0, // corresponds to 'insert' => 0
        Remove = 1, // corresponds to 'remove' => 1
        Update = 2 // corresponds to 'update' => 2
    }

    // For enums that need to associate with strings, you might need a different approach, such as a class with constant strings or a method to retrieve the string based on the enum value.
    public static class CategoryType
    {
        public const string Addon = "0"; // corresponds to 'addon' => '0'
        public const string Project = "1"; // corresponds to 'project' => '1'
        public const string Any = "%"; // corresponds to 'any' => '%'
    }

    public enum SupportLevel
    {
        Testing = 0, // corresponds to 'testing' => 0
        Community = 1, // corresponds to 'community' => 1
        Official = 2 // corresponds to 'official' => 2
    }

    public enum UserType
    {
        Normal = 0, // corresponds to 'normal' => 0
        Verified = 5, // corresponds to 'verified' => 5
        Editor = 25, // corresponds to 'editor' => 25
        Moderator = 50, // corresponds to 'moderator' => 50
        Admin = 100 // corresponds to 'admin' => 100
    }

    public enum DownloadProvider
    {
        Custom = -1, // corresponds to 'Custom' => -1
        GitHub = 0, // corresponds to 'GitHub' => 0
        GitLab = 1, // corresponds to 'GitLab' => 1
        BitBucket = 2, // corresponds to 'BitBucket' => 2
        GogsGiteaCodeberg = 3, // corresponds to 'Gogs/Gitea/Codeberg' => 3
        Cgit = 4 // corresponds to 'cgit' => 4
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
