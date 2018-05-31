using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Guids
{
    /// <summary>
    /// Extension methods for <see cref="Guid"/>.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Returns a 16-element byte array that contains the value of the GUID, in big-endian format.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        public static byte[] ToBigEndianByteArray(this in Guid guid)
        {
            var result = guid.ToByteArray();
            GuidUtility.EndianSwap(result);
            return result;
        }

        /// <summary>
        /// Decodes a GUID into its fields.
        /// </summary>
        /// <param name="guid">The GUID to decode.</param>
        public static DecodedGuid Decode(this in Guid guid) => new DecodedGuid(guid);
    }
}
