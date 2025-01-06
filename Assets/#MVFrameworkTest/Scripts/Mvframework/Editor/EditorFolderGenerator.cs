#if UNITY_EDITOR
using System.Collections.Generic;
using MVFramework.Editor.FolderRepositories;
using UnityEditor;

namespace MVFramework.Editor
{
    public class EditorFolderGenerator
    {
        private readonly string _startingPath;

        private readonly List<FolderHierarchy> _folderHierarchy;

        public EditorFolderGenerator(IFolderRepository folderRepository, string startingPath)
        {
            _startingPath = startingPath;
            _folderHierarchy = folderRepository.GetFolders();
        }

        public void GenerateFolders()
        {
            foreach (FolderHierarchy parentFolder in _folderHierarchy)
            {
                GenerateFolders(parentFolder, _startingPath);
            }
        }

        private void GenerateFolders(FolderHierarchy parentFolder, string path)
        {
            GenerateFolder(path, parentFolder.Name);
            foreach (FolderHierarchy childFolder in parentFolder.Folders)
            {
                GenerateFolders(childFolder, $"{path}/{parentFolder.Name}");
            }
        }

        private void GenerateFolder(string path, string folderName)
        {
            AssetDatabase.CreateFolder(path, folderName);
        }
    }
}
#endif