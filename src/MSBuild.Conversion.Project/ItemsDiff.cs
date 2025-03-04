﻿using System.Collections.Immutable;
using System.Linq;

using MSBuild.Abstractions;

namespace MSBuild.Conversion.Project
{
    public struct ItemsDiff
    {
        public readonly string ItemType;
        public readonly ImmutableArray<IProjectItem> DefaultedItems;
        public readonly ImmutableArray<IProjectItem> NotDefaultedItems;
        public readonly ImmutableArray<IProjectItem> IntroducedItems;
        public readonly ImmutableArray<IProjectItem> ChangedItems;
        public readonly ImmutableArray<IProjectItem> AbsentItems;

        public ItemsDiff(string itemType, ImmutableArray<IProjectItem> defaultedItems, ImmutableArray<IProjectItem> notDefaultedItems, ImmutableArray<IProjectItem> introducedItems, ImmutableArray<IProjectItem> changedItems, ImmutableArray<IProjectItem> absentItems) : this()
        {
            ItemType = itemType;
            DefaultedItems = defaultedItems;
            NotDefaultedItems = notDefaultedItems;
            IntroducedItems = introducedItems;
            ChangedItems = changedItems;
            AbsentItems = absentItems;
        }

        public ImmutableArray<string> GetDiffLines()
        {
            var lines = ImmutableArray.CreateBuilder<string>();

            if (!DefaultedItems.IsEmpty || !NotDefaultedItems.IsEmpty || !IntroducedItems.IsEmpty || !ChangedItems.IsEmpty || !AbsentItems.IsEmpty)
            {
                lines.Add($"{ ItemType} items:");
                if (!DefaultedItems.IsEmpty)
                {
                    lines.AddRange(DefaultedItems.Select(s => $"- {s.EvaluatedInclude}"));
                }

                if (!NotDefaultedItems.IsEmpty)
                {
                    lines.AddRange(NotDefaultedItems.Select(s => $"= {s.EvaluatedInclude}"));
                }

                if (!IntroducedItems.IsEmpty)
                {
                    lines.AddRange(IntroducedItems.Select(s => $"+ {s.EvaluatedInclude}"));
                }
                if (!AbsentItems.IsEmpty)
                {
                    lines.AddRange(AbsentItems.Select(s => $"+ {s.EvaluatedInclude}"));
                }

                lines.Add("");
            }

            return lines.ToImmutable();
        }
    }
}
