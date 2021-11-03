using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace LadyBug.SaveSystem
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string id = string.Empty;

        public string Id => id;


        [ContextMenu("Generate Id")]
        //generates a uniqe id, used for saving
        private void GenerateId() => id = Guid.NewGuid().ToString();

        public object CaptureState()
        {
            var state = new Dictionary<String, object>();

            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return state;
        }

        //restores the save file (loads it)
        public void ResoreState(object state)
        {
            var stateDictionary = (Dictionary<string, object>)state;

            foreach (var saveable in GetComponents<ISaveable>())
            {
                string typeName = saveable.GetType().ToString();

                if (stateDictionary.TryGetValue(typeName, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }

}
