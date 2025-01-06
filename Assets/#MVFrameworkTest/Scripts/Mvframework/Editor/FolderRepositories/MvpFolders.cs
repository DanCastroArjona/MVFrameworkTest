using System.Collections.Generic;

namespace MVFramework.Editor.FolderRepositories
{
    public class MvpFolders : IFolderRepository
    {
        public List<FolderHierarchy> GetFolders()
        {
            return new List<FolderHierarchy>
            {
                new FolderHierarchy("Presenters"),
                new FolderHierarchy("Installers"),
                new FolderHierarchy("Models"),
                new FolderHierarchy("Signals"),
                new FolderHierarchy("Views"),
                new FolderHierarchy("Utils")
            };
        }
    }
}