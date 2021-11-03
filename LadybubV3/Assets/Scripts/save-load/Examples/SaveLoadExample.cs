using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LadyBug.SaveSystem;
using System;

//make sure to include inherit from the ISaveable interface. Its in the Ladybug.SaveSystem namespace. Look at line 4
public class SaveLoadExample : MonoBehaviour, ISaveable
{
    [SerializeField] private int score = 10;
    public object CaptureState()
    {
        return new saveData { score = score, xPos = transform.position.x, yPos = transform.position.y, zPos = transform.position.z };
    }

    public void RestoreState(object state)
    {
        var saveData = (saveData)state;

        score = saveData.score;
        transform.position = new Vector3(saveData.xPos, saveData.yPos, saveData.zPos);
    }

    [Serializable]
    private struct saveData
    {
        public int score;
        public float xPos, yPos, zPos;
    }
}
