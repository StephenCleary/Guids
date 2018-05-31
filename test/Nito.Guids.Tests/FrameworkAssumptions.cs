using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Nito.Guids.Tests
{
    public class FrameworkAssumptions
    {
        [Fact]
        public void ToByteArray_IsLittleEndian()
        {
            var guid = new Guid(0x00010203, 0x0405, 0x0607, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F);
            var result = guid.ToByteArray();
            Assert.Equal(new byte[] { 0x03, 0x02, 0x01, 0x00, 0x05, 0x04, 0x07, 0x06, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F }, result);
        }

        [Fact]
        public void ToString_N_IsBigEndian()
        {
            var guid = new Guid(0x00010203, 0x0405, 0x0607, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F);
            var result = guid.ToString("N");
            Assert.Equal("000102030405060708090a0b0c0d0e0f", result);
        }

        [Fact]
        public void ToString_D_IsBigEndian()
        {
            var guid = new Guid(0x00010203, 0x0405, 0x0607, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F);
            var result = guid.ToString("D");
            Assert.Equal("00010203-0405-0607-0809-0a0b0c0d0e0f", result);
        }
    }
}
