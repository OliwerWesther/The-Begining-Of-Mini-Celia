using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private ISavable[] savables;
    private string savePath;

    private void Awake()
    {
        savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>().ToArray();

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void SaveGame()
    {
        List<string> jsonList = new List<string>();
        foreach (var s in savables)
        {
            string json = JsonUtility.ToJson(new ObjectWrapper { json = JsonUtility.ToJson(s.save()) });
            jsonList.Add(json);
        }

        saveWrapper wrapper = new saveWrapper { objects = jsonList };
        string finalJson = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, finalJson);
    }


    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string finalJson = File.ReadAllText(savePath);
        saveWrapper wrapper = JsonUtility.FromJson<saveWrapper>(finalJson);

        if (wrapper.objects.Count != savables.Length)
        {
            Debug.LogWarning("mismatch between saved objects and scene objects");
        }

        for (int i = 0; i < savables.Length; i++)
        {
            string objectJson = wrapper.objects[i];
            ObjectWrapper wrapperObj = JsonUtility.FromJson < ObjectWrapper>(objectJson);

            savables[i].load(JsonUtility.FromJson(wrapperObj.json, savables[i].save().GetType()));
        }
    }


    [System.Serializable]
    private class ObjectWrapper
    {
        public string json;
    }

    [System.Serializable]
    public class saveWrapper
    {
        public List<string> objects;
    }
}


//https://www.youtube.com/watch?v=wblu2BwC9wk