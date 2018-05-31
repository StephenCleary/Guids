using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Guids
{
    /// <summary>
    /// Known values for the <see cref="Guid"/> Version field.
    /// </summary>
    public enum GuidVersion
    {
        /// <summary>
        /// Time-based (sequential) GUID.
        /// </summary>
        TimeBased = 1,

        /// <summary>
        /// DCE Security GUID with embedded POSIX UID/GID. See "DCE 1.1: Authentication and Security Services", Chapter 5 and "DCE 1.1: RPC", Appendix A.
        /// </summary>
        DceSecurity = 2,

        /// <summary>
        /// Name-based GUID using the MD5 hashing algorithm.
        /// </summary>
        NameBasedMd5 = 3,

        /// <summary>
        /// Random GUID.
        /// </summary>
        Random = 4,

        /// <summary>
        /// Name-based GUID using the SHA-1 hashing algorithm.
        /// </summary>
        NameBasedSha1 = 5,
    }
}
