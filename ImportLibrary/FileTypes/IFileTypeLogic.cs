namespace ImportLibrary.FileTypes
{
    public interface IFileTypeLogic
    {
        FileBody ReadData(string fullFileName);
    }
}