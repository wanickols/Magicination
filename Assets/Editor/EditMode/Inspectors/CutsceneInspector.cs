
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MGCNTN
{
    [CustomEditor(typeof(Cutscene))]
    public class CutsceneInspector : Editor
    {
        private VisualElement container;

        private Action openEditor;

        public void OnEnable()
        {
            openEditor = () => CutsceneEditor.ShowWindow(target as Cutscene);
        }

        public override VisualElement CreateInspectorGUI()
        {
            container = new VisualElement();

            SerializedProperty property = serializedObject.FindProperty("trigger");
            PropertyField field = new PropertyField(property);
            container.Add(field);

            property = serializedObject.FindProperty("callOnce");
            field = new PropertyField(property);
            container.Add(field);

            Button button = new Button(openEditor) { text = "Open Editor" };
            container.Add(button);

            DisplaySceneCommands();

            return container;

        }

        private void DisplaySceneCommands()
        {
            foreach (ICutCommand command in (target as Cutscene).Commands)
            {
                container.Add(new Label() { text = command.ToString() });
            }
        }
    }
}
