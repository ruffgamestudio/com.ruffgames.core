using _game.Storage;
using com.ruffgames.core.Scripts.Storage;
using Newtonsoft.Json;

namespace com.ruffgames.core.Runtime.Scripts.Storage
{
    public sealed class JsonStorage : IStorage
    {
        private const string FILENAME_DATA = "data_storage.dat";
        
        public override void Save()
        {
            var isSuccessful = FileUtils.Write(FILENAME_DATA, JsonConvert.SerializeObject(StorageData));
            if (!isSuccessful)
            {
                FileUtils.Write(FILENAME_DATA, JsonConvert.SerializeObject(StorageData));
            }
        }

        public override void Load()
        {
            var isSuccessful = FileUtils.Read(FILENAME_DATA, out var rawJson);
         
            if (isSuccessful)
            {
                var settings = new JsonSerializerSettings {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                };
                StorageData = JsonConvert.DeserializeObject<StorageData>(rawJson, settings);
            }
            else if (StorageData == null)
            {
                StorageData = new StorageData();
            }
        }

        public override void Clear()
        {
            FileUtils.Delete(FILENAME_DATA);
            StorageData = new StorageData();
        }
    }
}
