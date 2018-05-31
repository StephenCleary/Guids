using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nito.Guids.Tests
{
    public class GuidExtensionsTests
    {
        [Fact]
        public void ToBigEndianByteArray_ReturnsBigEndianArray()
        {
            var guid = new Guid(0x00112233, 0x4455, 0x6677, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF);
            var result = guid.ToBigEndianByteArray();
            Assert.Equal(new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF }, result);
        }

        [Fact]
        public void GuidVariant_NewGuid_IsRfc4122()
        {
            var guid = Guid.NewGuid().Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
        }

        [Fact]
        public void GuidVersion_NewGuid_IsRandom()
        {
            var guid = Guid.NewGuid().Decode();
            Assert.Equal(GuidVersion.Random, guid.Version);
        }

        [Fact]
        public void Melissa_DecodesAsExpected()
        {
            var guid = ExampleGuids.Melissa.Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
            Assert.Equal(GuidVersion.TimeBased, guid.Version);
            Assert.Equal(new DateTime(1998, 8, 18, 20, 52, 22, 510, DateTimeKind.Utc).AddTicks(10), guid.CreateTime);
            Assert.Equal(new byte[] { 0x00, 0x40, 0x33, 0xE0, 0x07, 0x8E }, guid.Node);
            Assert.True(guid.NodeIsMac);
        }

        [Fact]
        public void EtwAspNet_DecodesAsExpected()
        {
            var guid = ExampleGuids.EtwAspNet.Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
            Assert.Equal(GuidVersion.Random, guid.Version);
        }

        [Fact]
        public void FloppyDiskDeviceClass_DecodesAsExpected()
        {
            var guid = ExampleGuids.FloppyDiskDeviceClass.Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
            Assert.Equal(GuidVersion.TimeBased, guid.Version);
            Assert.Equal(new DateTime(1995, 8, 30, 23, 40, 58, 906, DateTimeKind.Utc).AddTicks(2528), guid.CreateTime);
            Assert.Equal(new byte[] { 0x08, 0x00, 0x2B, 0xE1, 0x03, 0x18 }, guid.Node);
            Assert.True(guid.NodeIsMac);
        }

        [Fact]
        public void GptSystemPartition_DecodesAsExpected()
        {
            var guid = ExampleGuids.GptSystemPartition.Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
            Assert.Equal(GuidVersion.TimeBased, guid.Version);
            Assert.Equal(new DateTime(1999, 4, 21, 19, 24, 01, 562, DateTimeKind.Utc).AddTicks(5000), guid.CreateTime);
            Assert.Equal(new byte[] { 0x00, 0xA0, 0xC9, 0x3E, 0xC9, 0x3B }, guid.Node);
            Assert.True(guid.NodeIsMac);
        }
    }
}
