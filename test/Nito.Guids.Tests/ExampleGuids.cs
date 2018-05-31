using System;
using System.Collections.Generic;
using System.Text;

// See https://web.archive.org/web/20120920085750/http://stephencleary.com/#applications_GuidDecoder

namespace Nito.Guids.Tests
{
    public static class ExampleGuids
    {
        /// <summary>
        /// The GUID used to track down the author of the Melissa virus.
        /// </summary>
        public static readonly Guid Melissa = Guid.Parse("572858EA-36DD-11D2-885F-004033E0078E");

        /// <summary>
        /// The ETW provider for ASP.NET events.
        /// </summary>
        public static readonly Guid EtwAspNet = Guid.Parse("AFF081FE-0247-4275-9C4E-021F3DC1DA35");

        /// <summary>
        /// The Windows device class for floppy disk drives.
        /// </summary>
        public static readonly Guid FloppyDiskDeviceClass = Guid.Parse("4d36e980-e325-11ce-bfc1-08002be10318");

        /// <summary>
        /// The GPT system partition on UEFI computers.
        /// </summary>
        public static readonly Guid GptSystemPartition = Guid.Parse("C12A7328-F81F-11D2-BA4B-00A0C93EC93B");
    }
}
