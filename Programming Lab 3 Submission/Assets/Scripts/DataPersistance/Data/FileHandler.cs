using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class FileHandler 
{

    private string dataDirPath = "";
    private string dataFileName = "";

    private bool useEncryption = false;

    private readonly string encryptionCode = "word";

    public FileHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;

        // 
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                     dataToLoad = reader.ReadToEnd();
                    }
                }  

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                } 

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }

            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "/" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        //string fullPath = dataDirPath + "/" + dataFileName;

        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try 
        {
            // Create a directory the filewill be written to if It doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the C# game data object JSON
            string dataToStore = JsonUtility.ToJson(data, true);


            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }


            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "/" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }

        return modifiedData;
    }


}
