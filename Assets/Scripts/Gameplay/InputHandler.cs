using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gameplay
{
    public class InputHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TileContent _content;
        [SerializeField]
        private Arrow _arrow;

        public bool Active { get; private set; } = false;

        private static bool _inputAllowed = true;

        private List<Tile> _values = new List<Tile>();
        private List<Arrow> _arrows = new List<Arrow>();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Active)
            {
                DestroyArrows();
            }
            else
            {
                if (!_inputAllowed) return;

                Tile originTile = _content.CurrentTile;
                Dictionary<Neighbour, Tile> dictionary = originTile.Neigbours;
                _values = dictionary.Values.Where(t => t.Content.Type == ContentType.Empty).ToList();
                if (_values.Count == 0) return;

                _inputAllowed = false;
                Active = true;
                foreach (Tile tile in _values)
                {
                    Arrow arrow = Instantiate(_arrow, originTile.Content.gameObject.transform, false);
                    arrow.ConnectedTile = tile;
                    int rotation = 90 * (int)dictionary.First(t => t.Value == tile).Key;
                    arrow.transform.Rotate(Vector3.forward, rotation);
                    _arrows.Add(arrow);
                    arrow.ArrowPressed += ExchangeTilesContent;
                }
            }
        }

        private void DestroyArrows()
        {
            Active = false;
            foreach (Arrow a in _arrows)
            {
                a.ArrowPressed -= ExchangeTilesContent;
                Destroy(a.gameObject);
            }
            _arrows.Clear();
            _inputAllowed = true;
        }

        private void ExchangeTilesContent(Arrow arrow)
        {
            Tile.ExchangeContent(_content.CurrentTile, arrow.ConnectedTile);
            DestroyArrows();
        }
    }
}
