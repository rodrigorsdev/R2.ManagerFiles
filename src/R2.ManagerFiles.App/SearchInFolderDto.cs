namespace R2.ManagerFiles.App
{
    public class SearchInFolderDto
    {
        public int DeletedFoldersQuantity { get; private set; }
        public int DeletedFilesQuantity { get; private set; }

        public SearchInFolderDto()
        { }

        public SearchInFolderDto(int _deletedFoldersQuantity, int _deletedFilesQuantity)
        {
            DeletedFoldersQuantity = _deletedFoldersQuantity;
            DeletedFilesQuantity = _deletedFilesQuantity;
        }
    }
}
