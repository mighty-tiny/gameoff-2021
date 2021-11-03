using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LadyBug.SaveSystem
{
    public class SaveLoadSystem : MonoBehaviour
    {
        //the file extension is very much important, do not change 
        private string SavePath => $"{Application.persistentDataPath}/saveData.CoolFileExtension";

        [ContextMenu("Save")]
        private void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        private void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (var stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create)) 
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.ResoreState(value);
                }
            }
        }
    }

}
