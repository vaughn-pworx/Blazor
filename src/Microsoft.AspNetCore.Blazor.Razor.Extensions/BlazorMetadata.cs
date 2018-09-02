// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Blazor.Razor
{
    // Metadata used for Blazor's interactions with the tag helper system
    internal static class BlazorMetadata
    {
        // There's a bug in the 15.7 preview 1 Razor that prevents 'Kind' from being serialized
        // this affects both tooling and build. For now our workaround is to ignore 'Kind' and
        // use our own metadata entry to denote non-Component tag helpers.
        public static readonly string SpecialKindKey = "Blazor.IsSpecialKind";

        public static class Bind
        {
            public static readonly string RuntimeName = "Blazor.None";

            public readonly static string TagHelperKind = "Blazor.Bind";

            public readonly static string FallbackKey = "Blazor.Bind.Fallback";

            public readonly static string TypeAttribute = "Blazor.Bind.TypeAttribute";

            public readonly static string ValueAttribute = "Blazor.Bind.ValueAttribute";

            public readonly static string ChangeAttribute = "Blazor.Bind.ChangeAttribute";
        }

        public static class ChildContent
        {
            public static readonly string RuntimeName = "Blazor.None";

            public static readonly string TagHelperKind = "Blazor.ChildContent";
        }

        public static class Component
        {
            public static readonly string ChildContentKey = "Blazor.ChildContent";

            public static readonly string DelegateSignatureKey = "Blazor.DelegateSignature";

            public static readonly string WeaklyTypedKey = "Blazor.IsWeaklyTyped";

            public static readonly string RuntimeName = "Blazor.IComponent";

            public readonly static string TagHelperKind = "Blazor.Component";
        }

        public static class EventHandler
        {
            public static readonly string EventArgsType = "Blazor.EventHandler.EventArgs";

            public static readonly string RuntimeName = "Blazor.None";

            public readonly static string TagHelperKind = "Blazor.EventHandler";
        }

        public static class Ref
        {
            public readonly static string TagHelperKind = "Blazor.Ref";

            public static readonly string RuntimeName = "Blazor.None";
        }
    }
}
