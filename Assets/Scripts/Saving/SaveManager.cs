using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private ISavable[] savables; //a list of what should be saved.
    private string savePath; //the path to save to (JSON)

    private void Awake()
    {
        savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>().ToArray(); //Find what should be saved

        savePath = Path.Combine(Application.persistentDataPath, "save.json"); //Find the folder to save to.
    }

    public void SaveGame() //Save the game
    {
        List<string> jsonList = new List<string>();
        foreach (var s in savables) //for each item that should be saved, save said thing in that specific code.
        {
            string json = JsonUtility.ToJson(new ObjectWrapper { json = JsonUtility.ToJson(s.save()) });
            jsonList.Add(json);
        }

        saveWrapper wrapper = new saveWrapper { objects = jsonList }; //function
        string finalJson = JsonUtility.ToJson(wrapper, true); //See if all of the objects that should be saved is saved.
        File.WriteAllText(savePath, finalJson);
    }


    public void LoadGame()
    {
        if (!File.Exists(savePath)) //See if the savePath exist, if not do a print.
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string finalJson = File.ReadAllText(savePath); //Look at the JSON file to see whan is stored.
        saveWrapper wrapper = JsonUtility.FromJson<saveWrapper>(finalJson);

        if (wrapper.objects.Count != savables.Length)
        {
            Debug.LogWarning("mismatch between saved objects and scene objects");
        }

        for (int i = 0; i < savables.Length; i++) //Unload each package from the JSON file.
        {
            string objectJson = wrapper.objects[i];
            ObjectWrapper wrapperObj = JsonUtility.FromJson < ObjectWrapper>(objectJson);

            savables[i].load(JsonUtility.FromJson(wrapperObj.json, savables[i].save().GetType())); //THIS DOES THE ACUALL UNLOADING.
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