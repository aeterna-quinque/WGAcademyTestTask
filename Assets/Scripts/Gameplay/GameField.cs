using Assets.Scripts.Factory;
using Assets.Scripts.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Gameplay
{
    public class GameField : MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup _tilesContainer;
        [SerializeField]
        private TileContentFactory _contentFactory;
        [SerializeField]
        private WinConditionBorderFactory _borderFactory;

        public event Action GameEnded;

        private Vector2 _size;
        private int _winTilesCount = 0;
        private List<Tile> _tiles = new List<Tile>();

        #region MonoBehaviour

        private void OnDisable()
        {
            foreach (Tile t in _tiles)
            {
                t.ContentChanged -= ChangeWinTilesCount;
                t.Dispose();
            }

            _tiles.Clear();
        }

        #endregion

        public void Initialize(FieldConfiguration config)
        {
            _size = config.Size;
            List<TileData> tilesData = config.TilesData;
            List<ContentType> contentList = new List<ContentType>();

            foreach (var tile in tilesData)
            {
                if (tile.Type == ContentType.Blocked || tile.Type == ContentType.Empty) continue;
                contentList.Add(tile.Type);
            }

            for (int y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++)
                {
                    Vector2 position = new Vector2(x, y);

                    TileData tileData = tilesData.Find(t => t.Position == position);
                    tilesData.Remove(tileData);

                    TileContent content;
                    if (tileData.Type == ContentType.Blocked || tileData.Type == ContentType.Empty)
                    {
                        content = _contentFactory.Get(tileData.Type);
                    }
                    else
                    {
                        int index = Random.Range(0, contentList.Count);
                        ContentType randomContent = contentList[index];
                        contentList.Remove(randomContent);
                        content = _contentFactory.Get(randomContent);
                    }

                    Tile tile = new Tile(position, content, tileData.Type, _tilesContainer.gameObject.transform);
                    tile.ContentChanged += ChangeWinTilesCount;
                    if (tile.WinConditionReached == true) _winTilesCount++;

                    if (tile.WinCondition != ContentType.Empty && tile.WinCondition != ContentType.Blocked)
                    {
                        TileContent border = _borderFactory.Get(tile.WinCondition);
                        border.gameObject.transform.SetParent(tile.TileObject, false);
                        border.transform.position = tile.TileObject.transform.position;
                        border.transform.SetAsFirstSibling();
                    }

                    _tiles.Add(tile);

                    if (x != 0)
                    {
                        int index = (int)(y * _size.x + x - 1);
                        Tile.ConnectHorizontalTiles(_tiles[index], tile);
                    }
                    if (y != 0)
                    {
                        int index = (int)((y - 1) * _size.x + x);
                        Tile.ConnectVerticalTiles(tile, _tiles[index]);
                    }
                }
            }

            CheckForWin();
        }

        private void ChangeWinTilesCount(bool winConditionReached)
        {
            if (winConditionReached) _winTilesCount++;
            else _winTilesCount--;
            CheckForWin();
        }

        private void CheckForWin()
        {
            bool result = _tiles.Count == _winTilesCount ? true : false;
            if (result) GameEnded?.Invoke();
        }
    }
}