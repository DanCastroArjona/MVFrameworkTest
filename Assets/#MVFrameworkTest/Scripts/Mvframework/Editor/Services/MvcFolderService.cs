#if UNITY_EDITOR
using MVFramework.Editor.FolderRepositories;
using UnityEditor;

namespace MVFramework.Editor.Services
{
    public class MvcFolderService : FolderGeneratorService<MvcFolders>
    {
        [MenuItem("Assets/Create/MvFramework/Generate MVC Folders")]
        private static void GenerateAppletFoldersInPath()
        {
            GenerateFoldersInSelectedGuidPath();
        }
    }
}
#endif