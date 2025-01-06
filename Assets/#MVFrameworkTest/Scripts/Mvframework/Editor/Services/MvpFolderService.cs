using MVFramework.Editor.FolderRepositories;
using UnityEditor;

namespace MVFramework.Editor.Services
{
    public class MvpFolderService : FolderGeneratorService<MvpFolders>
    {
        [MenuItem("Assets/Create/MvFramework/Generate MVP Folders")]
        private static void GenerateAppletFoldersInPath()
        {
            GenerateFoldersInSelectedGuidPath();
        }
    }
}