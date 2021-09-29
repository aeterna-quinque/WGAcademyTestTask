using Assets.Scripts.Gameplay;
using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct TileData
    {
        public Vector2 Position { get; private set; }
        public ContentType Type { get; private set; }

        public TileData(Vector2 position, ContentType type)
        {
            Position = position;
            Type = type;
        }
    }
}
