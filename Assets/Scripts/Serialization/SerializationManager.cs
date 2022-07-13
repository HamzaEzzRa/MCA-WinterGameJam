using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    #region Methods
    public static bool Save(object data, string fileName)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/Data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Data");

        string path = Application.persistentDataPath + "/Data/" + fileName + ".gd";
        FileStream file = File.Create(path);
        formatter.Serialize(file, data);
        file.Close();
        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
            return null;

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        try
        {
            object saveObject = formatter.Deserialize(file);
            file.Close();
            return saveObject;
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Failed to load file at {0}.", path);
            Debug.LogException(exception);
            file.Close();
            return null;
        }
    }

    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
    #endregion
}