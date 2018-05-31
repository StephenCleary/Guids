using System;

namespace Nito.Guids
{
    /// <summary>
    /// Known values for the <see cref="Guid"/> Variant field.
    /// </summary>
    public enum GuidVariant
    {
        /// <summary>
        /// Reserved for NCS backward compatibility.
        /// </summary>
        NcsBackwardCompatibility = 0,

        /// <summary>
        /// A GUID conforming to RFC 4122.
        /// </summary>
        Rfc4122 = 4,

        /// <summary>
        /// Reserved for Microsoft backward compatibility.
        /// </summary>
        MicrosoftBackwardCompatibility = 6,

        /// <summary>
        /// Reserved for future definition.
        /// </summary>
        ReservedForFutureDefinition = 7,
    }
}
