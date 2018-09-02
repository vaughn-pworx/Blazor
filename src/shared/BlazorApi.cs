// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Blazor.Shared
{
    // Constants for method names used in code-generation
    // Keep these in sync with the actual definitions
    internal static class BlazorApi
    {
        public static readonly string AssemblyName = "Microsoft.AspNetCore.Blazor";

        public static class BlazorComponent
        {
            public static readonly string Namespace = "Microsoft.AspNetCore.Blazor.Components";
            public static readonly string FullTypeName = Namespace + ".BlazorComponent";

            public static readonly string BuildRenderTree = nameof(BuildRenderTree);
        }

        public static class ParameterAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.ParameterAttribute";
        }

        public static class LayoutAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute";
        }

        public static class IComponent
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.IComponent";

            public static readonly string MetadataName = FullTypeName;
        }

        public static class IDictionary
        {
            public static readonly string MetadataName = "System.Collection.IDictionary`2";
        }

        public static class RenderFragment
        {
            public static readonly string Namespace = "Microsoft.AspNetCore.Blazor";
            public static readonly string FullTypeName = Namespace + ".RenderFragment";
        }

        public static class RenderFragmentOfT
        {
            public static readonly string Namespace = "Microsoft.AspNetCore.Blazor";
            public static readonly string FullTypeName = Namespace + ".RenderFragment<>";
        }

        public static class RenderTreeBuilder
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder";

            public static readonly string OpenElement = nameof(OpenElement);

            public static readonly string CloseElement = nameof(CloseElement);

            public static readonly string OpenComponent = nameof(OpenComponent);

            public static readonly string CloseComponent = nameof(CloseComponent);

            public static readonly string AddMarkupContent = nameof(AddMarkupContent);

            public static readonly string AddContent = nameof(AddContent);

            public static readonly string AddAttribute = nameof(AddAttribute);

            public static readonly string AddElementReferenceCapture = nameof(AddElementReferenceCapture);

            public static readonly string AddComponentReferenceCapture = nameof(AddComponentReferenceCapture);

            public static readonly string Clear = nameof(Clear);

            public static readonly string GetFrames = nameof(GetFrames);

            public static readonly string ChildContent = nameof(ChildContent);
        }

        public static class RuntimeHelpers
        {
            public static readonly string TypeCheck = "Microsoft.AspNetCore.Blazor.Components.RuntimeHelpers.TypeCheck";
        }

        public static class RouteAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.RouteAttribute";
        }

        public static class BindElementAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.BindElementAttribute";
        }

        public static class BindInputElementAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.BindInputElementAttribute";
        }

        public static class BindMethods
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.BindMethods";

            public static readonly string GetValue = "Microsoft.AspNetCore.Blazor.Components.BindMethods.GetValue";

            public static readonly string GetEventHandlerValue = "Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue";

            public static readonly string SetValueHandler = "Microsoft.AspNetCore.Blazor.Components.BindMethods.SetValueHandler";
        }

        public static class EventHandlerAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.EventHandlerAttribute";
        }

        public static class ElementRef
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.ElementRef";
        }
    }
}
