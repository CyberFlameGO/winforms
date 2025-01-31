﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    /// <summary>
    ///  This is a rendering class for the ProgressBar control.
    /// </summary>
    public static class ProgressBarRenderer
    {
        //Make this per-thread, so that different threads can safely use these methods.
        [ThreadStatic]
        private static VisualStyleRenderer? t_visualStyleRenderer;

        /// <summary>
        ///  Returns true if this class is supported for the current OS and user/application settings,
        ///  otherwise returns false.
        /// </summary>
        public static bool IsSupported => VisualStyleRenderer.IsSupported; // no downlevel support

        /// <summary>
        ///  Renders a horizontal bar.
        /// </summary>
        public static void DrawHorizontalBar(Graphics g, Rectangle bounds)
        {
            InitializeRenderer(VisualStyleElement.ProgressBar.Bar.Normal);

            t_visualStyleRenderer.DrawBackground(g, bounds);
        }

        /// <summary>
        ///  Renders a vertical bar.
        /// </summary>
        public static void DrawVerticalBar(Graphics g, Rectangle bounds)
        {
            InitializeRenderer(VisualStyleElement.ProgressBar.BarVertical.Normal);

            t_visualStyleRenderer.DrawBackground(g, bounds);
        }

        /// <summary>
        ///  Renders a number of constant size horizontal chunks in the given bounds.
        /// </summary>
        public static void DrawHorizontalChunks(Graphics g, Rectangle bounds)
        {
            InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);

            t_visualStyleRenderer.DrawBackground(g, bounds);
        }

        /// <summary>
        ///  Renders a number of constant size vertical chunks in the given bounds.
        /// </summary>
        public static void DrawVerticalChunks(Graphics g, Rectangle bounds)
        {
            InitializeRenderer(VisualStyleElement.ProgressBar.ChunkVertical.Normal);

            t_visualStyleRenderer.DrawBackground(g, bounds);
        }

        /// <summary>
        ///  Returns the  width/height of a single horizontal/vertical progress bar chunk.
        /// </summary>
        public static int ChunkThickness
        {
            get
            {
                InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);

                return t_visualStyleRenderer.GetInteger(IntegerProperty.ProgressChunkSize);
            }
        }

        /// <summary>
        ///  Returns the  width/height of the space between horizontal/vertical progress bar chunks.
        /// </summary>
        public static int ChunkSpaceThickness
        {
            get
            {
                InitializeRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);

                return t_visualStyleRenderer.GetInteger(IntegerProperty.ProgressSpaceSize);
            }
        }

        [MemberNotNull(nameof(t_visualStyleRenderer))]
        private static void InitializeRenderer(VisualStyleElement element)
        {
            if (t_visualStyleRenderer is null)
            {
                t_visualStyleRenderer = new VisualStyleRenderer(element);
            }
            else
            {
                t_visualStyleRenderer.SetParameters(element);
            }
        }
    }
}
