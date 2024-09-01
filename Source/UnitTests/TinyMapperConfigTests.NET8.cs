﻿#if !NET452
using System;
using Nelibur.ObjectMapper;
using Xunit;

namespace UnitTests
{
    public class TinyMapperConfigTests
    {
        [Fact(DisplayName = "Checks if the Name-Matching is used.")]
        public void CustomNameMatching_Success()
        {
            TinyMapper.Config(config =>
            {
                config.NameMatching((x, y) => string.Equals(x + "1", y, StringComparison.OrdinalIgnoreCase));
            });

            TinyMapper.Bind<SourceCustom, TargetCustom>();
            var sourceCustom = new SourceCustom { Custom = "Hello", Default = "Default" };
            var targetCustom = TinyMapper.Map<TargetCustom>(sourceCustom);

            Assert.Equal(sourceCustom.Custom, targetCustom.Custom1);
            Assert.True(string.IsNullOrEmpty(targetCustom.Default));

            TinyMapper.Config(config =>
            {
                config.Reset();
            });
            TinyMapper.Bind<SourceDefault, TargetDefault>();

            var sourceDefault = new SourceDefault { Custom = "Hello", Default = "Default" };
            var targetDefault = TinyMapper.Map<TargetDefault>(sourceDefault);

            Assert.True(string.IsNullOrEmpty(targetDefault.Custom1));
            Assert.Equal(sourceDefault.Default, targetDefault.Default);
        }

        [Fact(DisplayName = "Checks if the Name-Matching is used isolated.")]
        public void CustomNameMatching_IsIsolated()
        {
            TinyMapper.Config(config =>
            {
                config.NameMatching((x, y) => string.Equals(x + "1", y, StringComparison.OrdinalIgnoreCase));

                TinyMapper.Bind<SourceCustom, TargetCustom>();
                var sourceCustomOtherDomain = new SourceCustom { Custom = "Hello", Default = "Default" };
                var targetCustomOtherDomain = TinyMapper.Map<TargetCustom>(sourceCustomOtherDomain);

                Assert.Equal(sourceCustomOtherDomain.Custom, targetCustomOtherDomain.Custom1);
                Assert.True(string.IsNullOrEmpty(targetCustomOtherDomain.Default));
            });

            TinyMapper.Bind<SourceCustom, TargetCustom>();
            var sourceCustom = new SourceCustom { Custom = "Hello", Default = "Default" };
            var targetCustom = TinyMapper.Map<TargetCustom>(sourceCustom);

            TinyMapper.Bind<SourceDefault, TargetDefault>();
            var sourceDefault = new SourceDefault { Custom = "Hello", Default = "Default" };
            var targetDefault = TinyMapper.Map<TargetDefault>(sourceDefault);

            Assert.True(string.IsNullOrEmpty(targetDefault.Custom1));
            Assert.Equal(sourceDefault.Default, targetDefault.Default);
        }

        public class SourceCustom
        {
            public string Custom { get; set; }
            public string Default { get; set; }
        }

        public class TargetCustom
        {
            public string Custom1 { get; set; }
            public string Default { get; set; }
        }

        public class SourceDefault
        {
            public string Custom { get; set; }
            public string Default { get; set; }
        }

        public class TargetDefault
        {
            public string Custom1 { get; set; }
            public string Default { get; set; }
        }
    }
}
#endif