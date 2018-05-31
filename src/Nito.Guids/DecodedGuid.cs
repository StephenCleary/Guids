using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Guids
{
    /// <summary>
    /// A <see cref="Guid"/> that has been decoded into its fields.
    /// </summary>
    public sealed class DecodedGuid
    {
        private static readonly DateTime Epoch = new DateTime(1582, 10, 15, 0, 0, 0, DateTimeKind.Utc);

        private readonly byte[] _littleEndianGuidBytes;

        public DecodedGuid(in Guid guid)
        : this(guid.ToByteArray())
        {
        }

        public DecodedGuid(byte[] littleEndianGuidBytes)
        {
            _littleEndianGuidBytes = littleEndianGuidBytes;
        }

        /// <summary>
        /// Converts the decoded GUID back to a <see cref="Guid"/>.
        /// </summary>
        public Guid ToGuid() => new Guid(_littleEndianGuidBytes);

        /// <summary>
        /// Converts the decoded GUID to a binary little-endian representation.
        /// </summary>
        public byte[] ToLittleEndianByteArray() => GuidUtility.Copy(_littleEndianGuidBytes);

        /// <summary>
        /// Converts the decoded GUID to a binary big-endian representation.
        /// </summary>
        public byte[] ToBigEndianByteArray() => GuidUtility.CopyWithEndianSwap(_littleEndianGuidBytes);

        /// <summary>
        /// Gets the 3-bit Variant field of the GUID.
        /// </summary>
        public GuidVariant Variant
        {
            get
            {
                var value = _littleEndianGuidBytes[8]; // big-endian octet 8
                if ((value & 0x80) == 0)
                    return GuidVariant.NcsBackwardCompatibility;
                if ((value & 0x40) == 0)
                    return GuidVariant.Rfc4122;
                return (GuidVariant)(value >> 5);
            }
        }

        /// <summary>
        /// Gets the 4-bit Version field of the GUID. This is only valid if <see cref="Variant"/> returns <see cref="GuidVariant.Rfc4122"/>.
        /// </summary>
        public GuidVersion Version
        {
            get
            {
                var value = _littleEndianGuidBytes[7]; // big-endian octet 6
                return (GuidVersion)(value >> 4);
            }
        }

        /// <summary>
        /// Gets the Domain field of the security GUID. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public DceDomain Domain
        {
            get
            {
                var value = _littleEndianGuidBytes[9]; // big-endian octet 9
                return (DceDomain)value;
            }
        }

        /// <summary>
        /// Gets the Local Identifier field of the security GUID. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.DceSecurity"/>
        /// </summary>
        public uint LocalIdentifier
        {
            get
            {
                uint result = _littleEndianGuidBytes[3]; // big-endian octet 0
                result <<= 8;
                result |= _littleEndianGuidBytes[2]; // big-endian octet 1
                result <<= 8;
                result |= _littleEndianGuidBytes[1]; // big-endian octet 2
                result <<= 8;
                result |= _littleEndianGuidBytes[0]; // big-endian octet 3
                return result;
            }
        }

        /// <summary>
        /// Gets the 60-bit Timestamp field of the GUID. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.TimeBased"/>.
        /// </summary>
        public long Timestamp
        {
            get
            {
                long result = _littleEndianGuidBytes[7] & 0x0F; // big-endian octet 6
                result <<= 8;
                result |= _littleEndianGuidBytes[6]; // big-endian octet 7
                result <<= 8;
                result |= _littleEndianGuidBytes[5]; // big-endian octet 4
                result <<= 8;
                result |= _littleEndianGuidBytes[4]; // big-endian octet 5
                result <<= 8;
                result |= _littleEndianGuidBytes[3]; // big-endian octet 0
                result <<= 8;
                result |= _littleEndianGuidBytes[2]; // big-endian octet 1
                result <<= 8;
                result |= _littleEndianGuidBytes[1]; // big-endian octet 2
                result <<= 8;
                result |= _littleEndianGuidBytes[0]; // big-endian octet 3
                return result;
            }
        }

        /// <summary>
        /// Gets the 28-bit Timestamp field of the GUID; the lowest 32 bits of the returned value are always zero. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public long PartialTimestamp
        {
            get
            {
                long result = _littleEndianGuidBytes[7] & 0x0F; // big-endian octet 6
                result <<= 8;
                result |= _littleEndianGuidBytes[6]; // big-endian octet 7
                result <<= 8;
                result |= _littleEndianGuidBytes[5]; // big-endian octet 4
                result <<= 8;
                result |= _littleEndianGuidBytes[4]; // big-endian octet 5
                result <<= 32;
                return result;
            }
        }

        /// <summary>
        /// Gets the date and time that this GUID was created, in UTC. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.TimeBased"/>.
        /// </summary>
        public DateTime CreateTime => Epoch.AddTicks(Timestamp);

        /// <summary>
        /// Gets the approximate date and time that this GUID was created, in UTC. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public DateTime PartialCreateTime => Epoch.AddTicks(PartialTimestamp);

        /// <summary>
        /// Gets the 14-bit Clock Sequence field of the GUID. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.TimeBased"/>.
        /// </summary>
        public int ClockSequence
        {
            get
            {
                var result = _littleEndianGuidBytes[8] & 0x3F; // big-endian octet 8
                result <<= 8;
                result |= _littleEndianGuidBytes[9]; // big-endian octet 9
                return result;
            }
        }

        /// <summary>
        /// Gets the 6-bit Clock Sequence field of the GUID. The lowest 8 bits of the returned value are always 0. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public int PartialClockSequence => (_littleEndianGuidBytes[8] & 0x3F) << 8; // big-endian octet 8

        /// <summary>
        /// Gets the 6-byte (48-bit) Node field of the GUID. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.TimeBased"/> or <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public ArraySegment<byte> Node => new ArraySegment<byte>(_littleEndianGuidBytes, 10, 6); // big-endian octets 10-16

        /// <summary>
        /// Returns <c>true</c> if the Node field is a MAC address; returns <c>false</c> if the Node field is a random value. This is only valid if <see cref="Version"/> returns <see cref="GuidVersion.TimeBased"/> or <see cref="GuidVersion.DceSecurity"/>.
        /// </summary>
        public bool NodeIsMac => (_littleEndianGuidBytes[10] & 0x80) == 0; // big-endian octet 10
    }
}
