﻿using System.Windows;
using System.Windows.Controls;

namespace Pokedex.Pokerole.Controls
{
    // Watches a task and raises property-changed notifications when the task completes.

    public class SelectableTextBlock : TextBlock
    {
        static SelectableTextBlock()
        {
            FocusableProperty.OverrideMetadata(typeof(SelectableTextBlock), new FrameworkPropertyMetadata(true));
            TextEditorWrapper.RegisterCommandHandlers(typeof(SelectableTextBlock), true, true, true);

            // remove the focus rectangle around the control
            FocusVisualStyleProperty.OverrideMetadata(typeof(SelectableTextBlock), new FrameworkPropertyMetadata((object)null));
        }

        private readonly TextEditorWrapper _editor;

        public SelectableTextBlock()
        {
            _editor = TextEditorWrapper.CreateFor(this);
        }
    }
}