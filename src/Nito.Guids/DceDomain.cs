namespace Nito.Guids
{
    /// <summary>
    /// Known values of DCE domains.
    /// </summary>
    public enum DceDomain
    {
        /// <summary>
        /// The principal domain. On POSIX machines, this is the POSIX UID domain.
        /// </summary>
        Person = 0,

        /// <summary>
        /// The group domain. On POSIX machines, this is the POSIX GID domain.
        /// </summary>
        Group = 1,

        /// <summary>
        /// The organization domain.
        /// </summary>
        Organization = 2,
    }
}
