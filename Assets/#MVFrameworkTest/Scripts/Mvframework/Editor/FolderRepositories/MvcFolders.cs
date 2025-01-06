#if UNITY_EDITOR
using System.Collections.Generic;

namespace MVFramework.Editor.FolderRepositories
{
    public class MvcFolders : IFolderRepository
    {
        public List<FolderHierarchy> GetFolders()
        {
            return new List<FolderHierarchy>
            {
                new FolderHierarchy("Controllers"),
                new FolderHierarchy("Installers"),
                new FolderHierarchy("Models"),
                new FolderHierarchy("Signals"),
                new FolderHierarchy("Views"),
                new FolderHierarchy("Utils")
            };
        }
    }
}
#endif