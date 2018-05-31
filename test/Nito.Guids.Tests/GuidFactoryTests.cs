using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nito.Guids.Tests
{
    public class GuidFactoryTests
    {
        [Fact]
        public void FromBigEndianByteArray_ReturnsGuidInOrder()
        {
            var bytes = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var guid = GuidFactory.FromBigEndianByteArray(bytes);
            Assert.Equal("00112233-4455-6677-8899-aabbccddeeff", guid.ToString("D"));
        }

        [Fact]
        public void FromLittleEndianByteArray_ReturnsGuidInMicrosoftOrder()
        {
            var bytes = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var guid = GuidFactory.FromLittleEndianByteArray(bytes);
            Assert.Equal("33221100-5544-7766-8899-aabbccddeeff", guid.ToString("D"));
        }

        [Fact]
        public void BigEndianByteArray_Roundtrip()
        {
            var bytes = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var guid = GuidFactory.FromBigEndianByteArray(bytes);
            Assert.Equal(bytes, guid.ToBigEndianByteArray());
        }

        [Fact]
        public void LittleEndianByteArray_Roundtrip()
        {
            var bytes = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var guid = GuidFactory.FromLittleEndianByteArray(bytes);
            Assert.Equal(bytes, guid.ToByteArray());
        }

        [Fact]
        public void CreateRandom_CreatesRandomRfc4122Guid()
        {
            var guid = GuidFactory.CreateRandom().Decode();
            Assert.Equal(GuidVariant.Rfc4122, guid.Variant);
            Assert.Equal(GuidVersion.Random, guid.Version);
        }

        [Fact]
        public void CreateMd5_RfcExample_EncodesAsExpected()
        {
            // See https://www.rfc-editor.org/errata/eid1352
            var guid = GuidFactory.CreateMd5(GuidNamespaces.Dns,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes("www.widgets.com"));
            Assert.Equal("3d813cbb-47fb-32ba-91df-831e1593ac29", guid.ToString("D"));
        }

        [Fact]
        public void CreateMd5_PythonExample_EncodesAsExpected()
        {
            // See https://docs.python.org/3/library/uuid.html#example
            var guid = GuidFactory.CreateMd5(GuidNamespaces.Dns,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes("python.org"));
            Assert.Equal("6fa459ea-ee8a-3ca4-894e-db77e160355e", guid.ToString("D"));
        }

        [Fact]
        public void CreateSha1_PythonExample_EncodesAsExpected()
        {
            // See https://docs.python.org/3/library/uuid.html#example
            var guid = GuidFactory.CreateSha1(GuidNamespaces.Dns,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes("python.org"));
            Assert.Equal("886313e1-3b8a-5372-9b90-0c9aee199e5d", guid.ToString("D"));
        }

        [Fact]
        public void CreateMd5_OsspExample_EncodesAsExpected()
        {
            // See https://github.com/sean-/ossp-uuid/blob/master/perl/uuid.ts#L74
            var guid = GuidFactory.CreateMd5(GuidNamespaces.Url,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes("http://www.ossp.org/"));
            Assert.Equal("02d9e6d5-9467-382e-8f9b-9300a64ac3cd", guid.ToString("D"));
        }
    }
}
