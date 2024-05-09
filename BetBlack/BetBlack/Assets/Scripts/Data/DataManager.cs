using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BulletEcho.Util;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;


namespace BulletEcho.DataSystem
{
    public static class DataUtil
    {
        
    }

    [Serializable]
    public class GamePlayData
    {
       
    }
   
    public class DataManager 
    {
       private static DataManager _Instance;
        public static DataManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataManager();
                    _Instance.LoadData();
                }
                return _Instance;
            }
        }
        private GamePlayData data;
        private const string JsonFilePath = "MyData";
        private const string FileName = "GameData.json";

        public GamePlayData GetLevelData() => data;

        private string GetPath()
        {
#if UNITY_EDITOR
            return  Application.dataPath + "/" + JsonFilePath;
#else
            return   Application.persistentDataPath;
#endif
            
        }

        public void Save()
        {
            Debug.Log("<color=green>TAG::LevelDataManager========================Saving Data ====================================</color>");
#if UNITY_EDITOR
            var path = GetPath();
            var directoryPath = path.Replace("/", "\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            var filepath = path + "/" + FileName;
            filepath.WriteFile(JsonConvert.SerializeObject(data,Formatting.Indented));
#else
            var path= GetPath() + "/" + FileName;
            path.WriteFile(JsonConvert.SerializeObject(data,Formatting.Indented));
#endif
        }

        private void LoadData()
        {
            data = new GamePlayData(); 
            var filepath = GetPath()+ "/" + FileName;
            var json = string.Empty;
            if (filepath.CheckAndReadFile(out json))
            {
                try
                {
                    data = JsonConvert.DeserializeObject<GamePlayData>(json);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"TAG::LevelDataManager fail to parse Json {ex}");
                }
            }
        }
    }
}
