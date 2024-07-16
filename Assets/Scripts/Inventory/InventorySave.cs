using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class InventorySave : MonoBehaviour
{
    [SerializeField] private PlayerInventory thisInventory;


    private void OnEnable()
    {
        //thisInventory.thisInventory.Clear();
        //LoadScriptableObjects();
    }

    private void OnDisable()
    {
        //SaveScriptableObjects();
    }


    public void ResetScriptableObjects()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
            i++;
        }
    }

    // Makes files for saving and storing data
    public void SaveScriptableObjects()
    {
        ResetScriptableObjects();
        for (int i = 0; i < thisInventory.thisInventory.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(thisInventory.thisInventory[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    // Loading saves from files
    public void LoadScriptableObjects()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<ItemInventory>();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), temp);  
            file.Close();
            thisInventory.thisInventory.Add(temp);
            i++;
        }
    }
}
