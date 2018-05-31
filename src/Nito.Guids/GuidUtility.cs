using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Guids
{
    /// <summary>
    /// Utility methods for working with GUIDs as byte arrays.
    /// </summary>
    public static class GuidUtility
    {
        /// <summary>
        /// Creates a new array that is a copy of the specified array.
        /// </summary>
        /// <param name="guid">The GUID, as a byte array.</param>
        public static byte[] Copy(byte[] guid)
        {
            var result = new byte[16];
            Array.Copy(guid, result, 16);
            return result;
        }

        /// <summary>
        /// Creates a new array that is an endian conversion of the specified array, converting big-endian to little-endian or vice versa.
        /// </summary>
        /// <param name="guid">The GUID, as a byte array.</param>
        public static byte[] CopyWithEndianSwap(byte[] guid)
        {
            var result = new byte[16];
            result[0] = guid[3];
            result[1] = guid[2];
            result[2] = guid[1];
            result[3] = guid[0];
            result[4] = guid[5];
            result[5] = guid[4];
            result[6] = guid[7];
            result[7] = guid[6];
            Array.Copy(guid, 8, result, 8, 8);
            return result;
        }

        /// <summary>
        /// Converts a big-endian GUID to a little-endian GUID, or vice versa. This method modifies the array in-place.
        /// </summary>
        /// <param name="guid">The GUID, as a byte array.</param>
        public static void EndianSwap(byte[] guid)
        {
            Swap(guid, 0, 3);
            Swap(guid, 1, 2);

            Swap(guid, 4, 5);

            Swap(guid, 6, 7);
        }

        private static void Swap(byte[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
