#region Copyright
// Copyright 2021, Jouaid Technologies, All rights reserved.
// Permission is hereby granted, to the person obtaining buying a copy of this software and associated documentation 
// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, 
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace JT.Utils
{
    public class EditorBuildScenesWindow : EditorWindow
    {
        [MenuItem("Scenes/Build Scenes Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(EditorBuildScenesWindow), false, "Scenes");
        }

        int selected;
        int changedSelected;
        List<string> scenes = new List<string>();

        void OnGUI()
        {
            EditorGUILayout.Space();

            scenes.Clear();

            string[] dropOptions = new string[EditorBuildSettings.scenes.Length];

            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];

                string sceneName = Path.GetFileNameWithoutExtension(scene.path);

                scenes.Add(sceneName);

                dropOptions[i] = scenes[i];

                if (scene.path == EditorSceneManager.GetActiveScene().path)
                    selected = changedSelected = i;
            }

            // Selection
            changedSelected = EditorGUILayout.Popup(selected, dropOptions);

            if (selected != changedSelected)
            {
                selected = changedSelected;

                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[changedSelected].path);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Build Scenes", EditorStyles.boldLabel);

            for (int i = 0; i < scenes.Count; i++)
            {
                if (GUILayout.Button(scenes[i], GUILayout.Height(20)))
                {
                    selected = changedSelected = i;

                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                }

                dropOptions[i] = scenes[i];
            }
        }
    }
}