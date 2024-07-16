using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public FloatValue healthPoints;
    public FloatValue playerHealth;
    public BoolValue chestAState;
    public BoolValue chestBState;
    public Inventory playerInventory;
    public VectorValue playerPosition;
    public static GameSaveManager gameSave;
    public InventoryManager inventoryManager;
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public void ResetScriptableObjects()
    {
        healthPoints.runtimeValue = healthPoints.initialValue;
        playerHealth.runtimeValue = playerHealth.initialValue;
        chestAState.runtimeValue = chestAState.initialValue;
        chestBState.runtimeValue = chestBState.initialValue;
        playerPosition.runtimeValue = playerPosition.initialValue;
        playerInventory.items.Clear();
        playerInventory.numberOfKeys = 0;
        playerInventory.coin = 0;
        playerInventory.arrow = 0;

        for(int i = 0; i < objects.Count; i++)
        {
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }

    // Makes files for saving and storing data
    public void SaveScriptableObjects()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    // Loading saves from files
    public void LoadScriptableObjects()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        for (int i = 0; i < objects.Count; i++)
        {
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }
}
