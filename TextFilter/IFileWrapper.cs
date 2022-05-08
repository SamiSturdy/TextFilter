namespace TextFilter
{
    public interface IFileWrapper
    {
        bool DoesFileExist(string filePath);
        string GetTextFromFilePath(string filePath);
    }
}