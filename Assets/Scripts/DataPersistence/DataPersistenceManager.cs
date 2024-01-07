using Assets.Scripts.DataPersistence.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        private const string FileName = "data.json";

        public int? Scene;

        public static DataPersistenceManager Instance { get; private set; }
        private FileDataHandler _dataHandler;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            this._dataHandler = new FileDataHandler(Application.persistentDataPath, FileName);
            DontDestroyOnLoad(this.gameObject);

            LoadGame();
        }
        public void NewGame()
        {
            Scene = 1;
        }

        public void LoadGame()
        {
            var data = _dataHandler.Load();
            if(data == null)
                 return;

            Scene = data.Scene;
        }

        public void SaveGame(int scene)
        {
            var data = new GameData()
            {
                Scene = scene
            }; 
            _dataHandler.Save(data);
            Scene = scene;
        }
    }
}