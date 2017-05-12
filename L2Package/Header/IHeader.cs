using System.Collections.Generic;

namespace L2Package
{
    public interface IHeader
    {
        int ExportCount { get; }
        int ExportOffset { get; }
        int GenerationCount { get; }
        List<Header.GenerationInfo> Generations { get; }
        Header.GUID Guid { get; }
        int ImportCount { get; }
        int ImportOffset { get; }
        short LicenseMode { get; }
        int NameCount { get; }
        int NameOffset { get; }
        int PackageFlags { get; }
        short PackageVersion { get; }
        int Signature { get; }
    }
}