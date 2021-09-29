using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct FieldConfiguration
    {
        public Vector2 Size { get; private set; }
        public List<TileData> TilesData { get; private set; }

        public FieldConfiguration(Vector2 size, List<TileData> tilesData)
        {
            Size = size;
            TilesData = tilesData;
        }
    }
}