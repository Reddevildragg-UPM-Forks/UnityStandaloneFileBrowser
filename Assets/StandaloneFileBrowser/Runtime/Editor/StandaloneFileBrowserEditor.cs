#region

using System;
using UnityEditor;

#endregion

namespace SFB.Editor
{
    public class StandaloneFileBrowserEditor : IStandaloneFileBrowser
    {
        public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            string path = "";

            if (extensions == null)
            {
                path = EditorUtility.OpenFilePanel(title, directory, "");
            }
            else
            {
                path = EditorUtility.OpenFilePanelWithFilters(title, directory, GetFilterFromFileExtensionList(extensions));
            }

            return string.IsNullOrEmpty(path) ? new string[0] : new[] {path};
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            string path = EditorUtility.OpenFolderPanel(title, directory, "");
            return string.IsNullOrEmpty(path) ? new string[0] : new[] {path};
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFolderPanel(title, directory, multiselect));
        }

        public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            string ext = extensions != null ? extensions[0].extensions[0] : "";
            string name = string.IsNullOrEmpty(ext) ? defaultName : defaultName + "." + ext;
            return EditorUtility.SaveFilePanel(title, directory, name, ext);
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
        {
            cb.Invoke(SaveFilePanel(title, directory, defaultName, extensions));
        }

        // EditorUtility.OpenFilePanelWithFilters extension filter format
        static string[] GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            string[] filters = new string[extensions.Length * 2];
            for (int i = 0; i < extensions.Length; i++)
            {
                filters[i * 2] = extensions[i].name;
                filters[i * 2 + 1] = string.Join(",", extensions[i].extensions);
            }

            return filters;
        }
    }
}