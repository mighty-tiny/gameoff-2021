using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LadyBug.SaveSystem;
using System;

//make sure to include inherit from the ISaveable interface. Its in the Ladybug.SaveSystem namespace. Look at line 4
public class SaveLoadExample : MonoBehaviour, ISaveable
{
    //the Gameobjects value
    [SerializeField] private int score = 10;
    //save the data
    public object CaptureState()
    {
        return new saveData { score = score, xPos = transform.position.x, yPos = transform.position.y, zPos = transform.position.z };
    }
    //load the data
    public void RestoreState(object state)
    {
        var saveData = (saveData)state;

        score = saveData.score;
        transform.position = new Vector3(saveData.xPos, saveData.yPos, saveData.zPos);
    }

    //the save data
    [Serializable]
    private struct saveData
    {
        public int score;
        public float xPos, yPos, zPos;
    }
}
