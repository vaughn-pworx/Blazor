// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Blazor.Razor;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.AspNetCore.Blazor.Test.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Microsoft.AspNetCore.Blazor.Build.Test
{
    public class ChildContentRazorIntegrationTest : RazorIntegrationTestBase
    {
        private readonly CSharpSyntaxTree RenderChildContentComponent = Parse(@"
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
namespace Test
{
    public class RenderChildContent : BlazorComponent
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }

        [Parameter]
        RenderFragment ChildContent { get; set; }
    }
}
");

        private readonly CSharpSyntaxTree RenderMultipleChildContent = Parse(@"
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
namespace Test
{
    public class RenderMultipleChildContent : BlazorComponent
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, Header, Name);
            builder.AddContent(1, ChildContent);
            builder.AddContent(2, Footer);
        }

        [Parameter]
        string Name { get; set; }

        [Parameter]
        RenderFragment<string> Header { get; set; }

        [Parameter]
        RenderFragment ChildContent { get; set; }

        [Parameter]
        RenderFragment Footer { get; set; }
    }
}
");

        internal override bool UseTwoPhaseCompilation => true;

        [Fact]
        public void Render_BodyChildContent()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
<RenderChildContent>
  <div></div>
</RenderChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 0),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 1),
                frame => AssertFrame.Markup(frame, "\n  <div></div>\n", 2));
        }

        [Fact]
        public void Render_ExplicitChildContent()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
<RenderChildContent>
  <ChildContent>
    <div></div>
  </ChildContent>
</RenderChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 0),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 1),
                frame => AssertFrame.Markup(frame, "\n    <div></div>\n  ", 2));
        }

        [Fact]
        public void Render_BodyChildContent_Recursive()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
<RenderChildContent>
  <RenderChildContent>
    <div></div>
  </RenderChildContent>
</RenderChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 0),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 1),
                frame => AssertFrame.Whitespace(frame, 2),
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 3),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 4),
                frame => AssertFrame.Whitespace(frame, 6),
                frame => AssertFrame.Markup(frame, "\n    <div></div>\n  ", 5));
        }

        [Fact]
        public void Render_AttributeChildContent()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> template = @<div>@context.ToLowerInvariant()</div>; }
<RenderChildContent ChildContent=""@template.WithValue(""HI"")"" />");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 2),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 3),
                frame => AssertFrame.Element(frame, "div", 2, 0),
                frame => AssertFrame.Text(frame, "hi", 1));
        }

        [Fact]
        public void Render_AttributeChildContent_IgnoresEmptyBody()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> template = @<div>@context.ToLowerInvariant()</div>; }
<RenderChildContent ChildContent=""@template.WithValue(""HI"")""></RenderChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 2),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 3),
                frame => AssertFrame.Element(frame, "div", 2, 0),
                frame => AssertFrame.Text(frame, "hi", 1));
        }

        [Fact]
        public void Render_AttributeChildContent_IgnoresWhitespaceBody()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> template = @<div>@context.ToLowerInvariant()</div>; }
<RenderChildContent ChildContent=""@template.WithValue(""HI"")"">
       
</RenderChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderChildContent", 2, 2),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, 3),
                frame => AssertFrame.Element(frame, "div", 2, 0),
                frame => AssertFrame.Text(frame, "hi", 1));
        }

        [Fact]
        public void Render_MultipleChildContent()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderMultipleChildContent);

            var component = CompileToComponent(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> header = @<div>@context.ToLowerInvariant()</div>; }
<RenderMultipleChildContent Name=""billg"" Header=@header>
  <ChildContent>Some @(0) Content</ChildContent>
  <Footer>Bye!</Footer>
</RenderMultipleChildContent>");

            // Act
            var frames = GetRenderTree(component);

            // Assert
            Assert.Collection(
                frames,
                frame => AssertFrame.Component(frame, "Test.RenderMultipleChildContent", 5, 2),
                frame => AssertFrame.Attribute(frame, "Name", "billg", 3),
                frame => AssertFrame.Attribute(frame, "Header", typeof(RenderFragment<string>), 4),
                frame => AssertFrame.Attribute(frame, RenderTreeBuilder.ChildContent, typeof(RenderFragment), 5),
                frame => AssertFrame.Attribute(frame, "Footer", typeof(RenderFragment), 9),
                frame => AssertFrame.Element(frame, "div", 2, 0),
                frame => AssertFrame.Text(frame, "billg", 1),
                frame => AssertFrame.Text(frame, "Some ", 6),
                frame => AssertFrame.Text(frame, "0", 7),
                frame => AssertFrame.Text(frame, " Content", 8),
                frame => AssertFrame.Text(frame, "Bye!", 10));
        }

        [Fact]
        public void Render_ChildContent_AttributeAndBody_ProducesDiagnostic()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            // Act
            var generated = CompileToCSharp(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> template = @<div>@context.ToLowerInvariant()</div>; }
<RenderChildContent ChildContent=""@template.WithValue(""HI"")"">
Some Content
</RenderChildContent>");

            // Assert
            var diagnostic = Assert.Single(generated.Diagnostics);
            Assert.Same(BlazorDiagnosticFactory.ChildContentSetByAttributeAndBody.Id, diagnostic.Id);
        }

        [Fact]
        public void Render_ChildContent_AttributeAndExplicitChildContent_ProducesDiagnostic()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            // Act
            var generated = CompileToCSharp(@"
@addTagHelper *, TestAssembly
@{ RenderFragment<string> template = @<div>@context.ToLowerInvariant()</div>; }
<RenderChildContent ChildContent=""@template.WithValue(""HI"")"">
<ChildContent>
Some Content
</ChildContent>
</RenderChildContent>");

            // Assert
            var diagnostic = Assert.Single(generated.Diagnostics);
            Assert.Same(BlazorDiagnosticFactory.ChildContentSetByAttributeAndBody.Id, diagnostic.Id);
        }

        [Fact]
        public void Render_ChildContent_ExplicitChildContent_UnrecogizedContent_ProducesDiagnostic()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            // Act
            var generated = CompileToCSharp(@"
@addTagHelper *, TestAssembly
<RenderChildContent>
<ChildContent>
</ChildContent>
@somethingElse
</RenderChildContent>");

            // Assert
            var diagnostic = Assert.Single(generated.Diagnostics);
            Assert.Same(BlazorDiagnosticFactory.ChildContentMixedWithExplicitChildContent.Id, diagnostic.Id);
        }

        [Fact]
        public void Render_ChildContent_ExplicitChildContent_UnrecogizedElement_ProducesDiagnostic()
        {
            // Arrange
            AdditionalSyntaxTrees.Add(RenderChildContentComponent);

            // Act
            var generated = CompileToCSharp(@"
@addTagHelper *, TestAssembly
<RenderChildContent>
<ChildContent>
</ChildContent>
<UnrecognizedChildContent></UnrecognizedChildContent>
</RenderChildContent>");

            // Assert
            var diagnostic = Assert.Single(generated.Diagnostics);
            Assert.Same(BlazorDiagnosticFactory.ChildContentMixedWithExplicitChildContent.Id, diagnostic.Id);
        }
    }
}
