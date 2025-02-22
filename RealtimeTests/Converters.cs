﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Supabase.Realtime;
using Supabase.Realtime.Converters;

namespace RealtimeTests
{
    internal class TestJson
    {
        [JsonProperty("intArray")]
        public List<int> intArray { get; set; }

        [JsonProperty("stringArray")]
        public List<string> stringArray { get; set; }
    }

    [TestClass]
    public class Converters
    {
        [TestMethod("Support Array Conversions (WALRUS + Backwards Compat.)")]
        public void SupportArrayConversions()
        {
            var testJson = "{\"intArray\":[9999,99,99999], \"stringArray\": [\"testing\",\"1\",\"2\"]}";

            var parsed = JsonConvert.DeserializeObject<TestJson>(testJson, new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver()
            });

            CollectionAssert.AreEqual(new List<int> { 9999, 99, 99999 }, parsed.intArray);
            CollectionAssert.AreEqual(new List<string> { "testing", "1", "2" }, parsed.stringArray);

            var intConverter = new IntArrayConverter();
            CollectionAssert.AreEqual(new List<int>(), intConverter.Parse("{}"));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, intConverter.Parse("{1,2,3}"));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, intConverter.Parse("[1,2,3]"));
            CollectionAssert.AreEqual(new List<int> { 99, 999, 9999, 999999 }, intConverter.Parse("[99, 999, 9999, 999999]"));

            var strConverter = new StringArrayConverter();
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c" }, strConverter.Parse("{a,b,c}"));
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c" }, strConverter.Parse("[a,b,c]"));
        }
    }
}
