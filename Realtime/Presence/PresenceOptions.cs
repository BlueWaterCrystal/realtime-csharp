﻿using Newtonsoft.Json;

namespace Supabase.Realtime.Presence
{
    /// <summary>
    /// Options used to initialize Realtime Presence
    /// </summary>
    public class PresenceOptions
    {
        /// <summary>
        /// key option is used to track presence payload across clients
        /// </summary>
        [JsonProperty("key")]
        public string PresenceKey { get; set; }

        public PresenceOptions(string presenceKey)
        {
            PresenceKey = presenceKey;
        }
    }
}
