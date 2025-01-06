#if UNITY_EDITOR
using System.Collections.Generic;

namespace MVFramework.Editor.FolderRepositories
{
    public interface IFolderRepository
    {
        public List<FolderHierarchy> GetFolders();
    }
}
#endif