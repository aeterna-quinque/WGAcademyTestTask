using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    [CreateAssetMenu(menuName = "Factory/WinConditionBorder")]
    public class WinConditionBorderFactory : GameObjectFactory
    {
        [SerializeField]
        private TileContent _redBorderPrefab;
        [SerializeField]
        private TileContent _greenBorderPrefab;
        [SerializeField]
        private TileContent _blueBorderPrefab;

        public TileContent Get(ContentType type)
        {
            switch (type)
            {
                case ContentType.Red:
                    return Get(_redBorderPrefab);
                case ContentType.Green:
                    return Get(_greenBorderPrefab);
                case ContentType.Blue:
                    return Get(_blueBorderPrefab);
            }

            return null;
        }
    }
}
