using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Nito.Guids
{
    /// <summary>
    /// Methods for creating <see cref="Guid"/> instances.
    /// </summary>
    public static class GuidFactory
    {
        /// <summary>
        /// Converts an array of bytes containing a little-endian binary representation of a GUID to a <see cref="Guid"/>.
        /// </summary>
        /// <param name="littleEndianGuidBytes">The little-endian byte array to convert to a GUID.</param>
        public static Guid FromLittleEndianByteArray(byte[] littleEndianGuidBytes) => new Guid(littleEndianGuidBytes);

        /// <summary>
        /// Converts an array of bytes containing a big-endian binary representation of a GUID to a <see cref="Guid"/>.
        /// </summary>
        /// <param name="bigEndianGuidBytes">The big-endian byte array to convert to a GUID.</param>
        public static Guid FromBigEndianByteArray(byte[] bigEndianGuidBytes) => new Guid(GuidUtility.CopyWithEndianSwap(bigEndianGuidBytes));

        /// <summary>
        /// Creates a random (version 4) GUID.
        /// </summary>
        public static Guid CreateRandom() => Guid.NewGuid();

        /// <summary>
        /// Creates a named, SHA1-based (version 5) GUID.
        /// </summary>
        /// <param name="namespace">The GUID that defines the namespace.</param>
        /// <param name="name">The name within that namespace.</param>
        public static Guid CreateSha1(Guid @namespace, byte[] name) => CreateNamed(@namespace, name, GuidVersion.NameBasedSha1);

        /// <summary>
        /// Creates a named, MD5-based (version 3) GUID.
        /// </summary>
        /// <param name="namespace">The GUID that defines the namespace.</param>
        /// <param name="name">The name within that namespace.</param>
        public static Guid CreateMd5(Guid @namespace, byte[] name) => CreateNamed(@namespace, name, GuidVersion.NameBasedMd5);

        /// <summary>
        /// Creates a named, MD5-based (version 3) GUID.
        /// </summary>
        /// <param name="namespace">The GUID that defines the namespace.</param>
        /// <param name="name">The name within that namespace.</param>
        /// <param name="version">The version of GUID to create.</param>
        private static Guid CreateNamed(Guid @namespace, byte[] name, GuidVersion version)
        {
            var namespaceBytes = @namespace.ToBigEndianByteArray();

            byte[] hash;
            using (var algorithm = version == GuidVersion.NameBasedMd5 ? MD5.Create() : SHA1.Create() as HashAlgorithm)
            {
                algorithm.TransformBlock(namespaceBytes, 0, namespaceBytes.Length, null, 0);
                algorithm.TransformFinalBlock(name, 0, name.Length);
                hash = algorithm.Hash;
            }

            var guidBytes = new byte[16];
            Array.Copy(hash, 0, guidBytes, 0, 16);
            GuidUtility.EndianSwap(guidBytes);

            // Variant RFC4122
            guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80); // big-endian octet 8

            // Version
            guidBytes[7] = (byte)((guidBytes[7] & 0x0F) | ((int)version << 4)); // big-endian octet 6

            return new Guid(guidBytes);
        }
    }
}
