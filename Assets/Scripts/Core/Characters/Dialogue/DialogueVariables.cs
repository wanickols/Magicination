using Ink.Runtime;
using System.Collections.Generic;
using System.IO;
namespace MGCNTN.Core
{
    public class DialogueVariables
    {

        public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

        public DialogueVariables(string globalsFilePath)
        {
            string inkFileContents = File.ReadAllText(globalsFilePath);
            Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
            Story globalVariablesStory = compiler.Compile();

            variables = new Dictionary<string, Ink.Runtime.Object>();

            foreach (string name in globalVariablesStory.variablesState)
            {
                Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
                //Debug.Log($"Initialized global dialogue variable: {name} = value");
            }
        }


        public void StartListening(Story story)
        {
            //Needs to happen before assigning
            VariablesToStory(story);
            story.variablesState.variableChangedEvent += VariableChanged;
        }

        public void StopListening(Story story)
        {
            story.variablesState.variableChangedEvent -= VariableChanged;
        }

        private void VariableChanged(string name, Ink.Runtime.Object value)
        {
            if (variables.ContainsKey(name))
            {
                variables.Remove(name);
                variables.Add(name, value);
            }
        }

        private void VariablesToStory(Story story)
        {
            foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
            {
                story.variablesState.SetGlobal(variable.Key, variable.Value);
            }
        }
    }
}
