using Assets.Scripts.Factory;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class TileContent : MonoBehaviour
    {
        [SerializeField]
        private ContentType _type;

        public TileContentFactory OriginFactory { get; set; }

        public Tile CurrentTile { get; set; }

        public ContentType Type => _type;

        public void Clear()
        {
            OriginFactory.Reclaim(this);
        }
    }

    public enum ContentType
    {
        Empty,
        Blocked,
        Red,
        Green,
        Blue
    }
}