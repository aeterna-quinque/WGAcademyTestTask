using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    [CreateAssetMenu(menuName = "Factory/TileContent")]
    public class TileContentFactory : GameObjectFactory
    {
        [SerializeField]
        private TileContent _emptyTilePrefab;
        [SerializeField]
        private TileContent _blockedTilePrefab;
        [SerializeField]
        private TileContent _redTilePrefab;
        [SerializeField]
        private TileContent _greenTilePrefab;
        [SerializeField]
        private TileContent _blueTilePrefab;

        public TileContent Get(ContentType type)
        {
            switch (type)
            {
                case ContentType.Empty:
                    return Get(_emptyTilePrefab);
                case ContentType.Blocked:
                    return Get(_blockedTilePrefab);
                case ContentType.Red:
                    return Get(_redTilePrefab);
                case ContentType.Green:
                    return Get(_greenTilePrefab);
                case ContentType.Blue:
                    return Get(_blueTilePrefab);
            }

            return null;
        }
    }
}