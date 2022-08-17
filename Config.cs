using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace CassieBreaker
{
    public sealed class Config : IConfig
    {
        [Description("Enable")] 
        public bool IsEnabled { get; set; } = true;

        [Description("Enable voice cassie messages")]
        public bool CassieSpeakingEnabled { get; set; } = true;

        [Description("Cassie subtitles(lyrics)")]
        public bool CassieLyricsEnabled { get; set; } = true;
    }
}