using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LadyBug.SaveSystem;
using System;

public class SaveLoadExample : MonoBehaviour, ISaveable
{
    [SerializeField] private int score = 10;
    public object CaptureState()
    {
        return new saveData { score = score};
    }

    public void RestoreState(object state)
    {
        var saveData = (saveData)state;

        score = saveData.score;
    }

    [Serializable]
    private struct saveData
    {
        public int score;
    }
}
