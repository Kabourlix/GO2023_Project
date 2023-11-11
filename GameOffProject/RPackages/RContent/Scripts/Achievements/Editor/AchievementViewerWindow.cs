// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable


using SDKabu.KCore;
using UnityEditor;
using UnityEngine;

namespace Rezoskour.Content.Editor
{
    public class AchievementViewerWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private static IAchievementService? service;

        [MenuItem("Rezoskour/Achievement Viewer")]
        private static void ShowWindow()
        {
            service = KServiceInjection.Get<IAchievementService>();
            AchievementViewerWindow window = GetWindow<AchievementViewerWindow>();
            window.titleContent = new GUIContent("Achievement Viewer");
            window.Show();
        }

        private void OnFocus()
        {
            if (!KServiceInjection.Has<IAchievementService>())
            {
                return;
            }

            service = KServiceInjection.Get<IAchievementService>();
        }

        private void OnGUI()
        {
            if (service is null)
            {
                return;
            }

            Achievement[] achievements = service.GetAchievements();

            // Scroll view
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            //Create a list view from achievements
            foreach (Achievement achievement in achievements)
            {
                //Draw a horizontal line with the achievement name and if it is unlocked or not
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(achievement.AchievementName, EditorStyles.boldLabel);
                GUI.contentColor = achievement.IsUnlocked ? Color.green : Color.red;
                EditorGUILayout.LabelField(achievement.IsUnlocked ? "Unlocked" : "Locked");
                GUI.contentColor = Color.white;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField("-- Conditions -- ", EditorStyles.boldLabel);
                for (int i = 0; i < achievement.ValueToWatch.Length; i++)
                {
                    if (i >= achievement.valueToReach.Length)
                    {
                        break;
                    }

                    string watchedName = achievement.ValueToWatch[i];
                    int valueCond = achievement.valueToReach[i];
                    if (!service.TryGetValue(watchedName, out int watchedValue))
                    {
                        EditorGUILayout.LabelField($"    ({i + 1}) : {watchedName} = ???/{valueCond}");
                        continue;
                    }

                    EditorGUILayout.LabelField($"    ({i + 1}) : {watchedName} = {watchedValue}/{valueCond}");
                }
            }


            EditorGUILayout.EndScrollView();
        }
    }
}