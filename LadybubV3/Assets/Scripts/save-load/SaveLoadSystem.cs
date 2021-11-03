using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


//https://youtu.be/f5GvfZfy3yk
namespace LadyBug.SaveSystem
{
    public class SaveLoadSystem : MonoBehaviour
    {
        //the file extension is very much important, do not change 
        //where the savefile is stored
        private string SavePath => $"{Application.persistentDataPath}/saveData.CoolFileExtension";

        //can be saved from the context menu. We will do this by code as soon as possible;
        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }


        //loads the save file;
        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }


        //loads the file
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
        //saves the file

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create)) 
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        //captures the current save state to an object which will be saved
        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }


        //loads the project from the saved object
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
