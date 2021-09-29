using Assets.Scripts.Gameplay;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [CreateAssetMenu(menuName = "DataSerializer")]
    public class DataSerializer : ScriptableObject
    {
        [SerializeField]
        private string _inAssetsPath = "Configs";
        [SerializeField]
        private string _fileName = "configuration";

        private string _path;

        private void OnEnable()
        {
            _path = Path.Combine(Application.streamingAssetsPath, _inAssetsPath, _fileName + ".json");
        }

        public FieldConfiguration Deserialize()
        {
            if (!File.Exists(_path)) throw new FileNotFoundException();

            string config = "";
            using (StreamReader reader = new StreamReader(_path))
            {
                config = reader.ReadToEnd();
            }

            JObject configObject = JObject.Parse(config);

            List<JToken> tileDataObjects = configObject["TilesData"].Children().ToList();
            List<TileData> tilesData = new List<TileData>();
            foreach (var tileDataObject in tileDataObjects)
            {
                Vector2 position = tileDataObject["Position"].ToObject<Vector2>();
                ContentType winCondition = tileDataObject["Type"].ToObject<ContentType>();
                TileData data = new TileData(position, winCondition);
                tilesData.Add(data);
            }

            Vector2 fieldSize = configObject["Size"].ToObject<Vector2>();

            FieldConfiguration configInstance = new FieldConfiguration(fieldSize, tilesData);
            return configInstance;
        }
    }
}
