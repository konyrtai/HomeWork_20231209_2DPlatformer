using System;
using System.IO;
using Assets.Scripts.DataPersistence.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class FileDataHandler
    {
        private readonly string _dataDirPath;
        private readonly string _dataFileName;

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;
        }

        [CanBeNull]
        public GameData Load()
        {
            var fullPath = Path.Combine(_dataDirPath, _dataFileName);
            if (!File.Exists(fullPath)) return null;

            using var stream = new FileStream(fullPath, FileMode.Open);
            using var reader = new StreamReader(stream);
            var data = reader.ReadToEnd();
            return JsonUtility.FromJson<GameData>(data);
        }

        public void Save(GameData data)
        {
            var fullPath = Path.Combine(_dataDirPath, _dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                var dataToStore = JsonUtility.ToJson(data, true);

                using var stream = new FileStream(fullPath, FileMode.OpenOrCreate);
                using var writer = new StreamWriter(stream);
                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}