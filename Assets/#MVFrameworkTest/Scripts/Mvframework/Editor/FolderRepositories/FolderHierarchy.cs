#if UNITY_EDITOR
using System.Collections.Generic;

namespace MVFramework.Editor.FolderRepositories
{
    public class FolderHierarchy
    {
        public string Name { get; }
        
        public List<FolderHierarchy> Folders { get; }
        
        public FolderHierarchy(string name)
        {
            Name = name;
            Folders = new List<FolderHierarchy>();
        }
    }
}
#endif