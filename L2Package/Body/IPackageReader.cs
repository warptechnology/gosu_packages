namespace L2Package
{
    public interface IPackageReader
    {
        byte[] Bytes { get; set; }
        string Path { get; }

        void Read(string FileName);
        void Save(string FileName = "");
    }
}