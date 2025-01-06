#if UNITY_EDITOR
using System.Linq;
using MVFramework.Editor.FolderRepositories;
using UnityEditor;

namespace MVFramework.Editor.Services
{
    public abstract class FolderGeneratorService<T> where T : IFolderRepository, new()
    {
        protected static void GenerateFoldersInSelectedGuidPath()
        {
            string path = GetSelectedGuidPath();
            IFolderRepository repository = new T();
            GenerateFoldersInPath(repository, path);
        }
        
        private static void GenerateFoldersInPath(IFolderRepository repository, string path)
        {
            EditorFolderGenerator editorFolderGenerator = new EditorFolderGenerator(repository, path);
            editorFolderGenerator.GenerateFolders();
        }

        private static string GetSelectedGuidPath()
        {
            string guid = Selection.assetGUIDs.First();
            return AssetDatabase.GUIDToAssetPath(guid);
        }
    }
}
#endif