using Assets.Scripts.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Tile : IDisposable
    {
        public TileContent Content
        {
            get => _content;
            private set
            {
                _content = value;
                _content.gameObject.transform.SetParent(TileObject, false);
                _content.transform.position = TileObject.transform.position;
                _content.transform.SetAsLastSibling();
                _content.CurrentTile = this;
            }
        }

        public Vector2 Position { get; private set; }
        public Transform TileObject { get; private set; }
        public ContentType WinCondition { get; private set; }
        public bool WinConditionReached => WinCondition == Content.Type;
        public Dictionary<Neighbour, Tile> Neigbours { get; private set; } = new Dictionary<Neighbour, Tile>();

        public event Action<bool> ContentChanged;

        private TileContent _content;

        public Tile(Vector2 position, TileContent content, ContentType winCondition, Transform container)
        {
            TileObject = new GameObject("Tile", typeof(RectTransform)).transform;
            TileObject.SetParent(container, false);
            Position = position;
            Content = content;
            WinCondition = winCondition;
        }

        public TileData GetData() => new TileData(Position, WinCondition);

        public void Dispose()
        {
            UnityEngine.Object.Destroy(TileObject.gameObject);
            GC.SuppressFinalize(this);
        }

        public static void ConnectHorizontalTiles(Tile left, Tile right)
        {
            left.Neigbours.Add(Neighbour.Right, right);
            right.Neigbours.Add(Neighbour.Left, left);
        }

        public static void ConnectVerticalTiles(Tile upper, Tile lower)
        {
            upper.Neigbours.Add(Neighbour.Lower, lower);
            lower.Neigbours.Add(Neighbour.Upper, upper);
        }

        public static void ExchangeContent(Tile first, Tile second)
        {
            bool prevWinCondition = second.WinConditionReached;
            TileContent temp = second.Content;
            second.Content = first.Content;
            if (second.WinConditionReached != prevWinCondition)
                second.ContentChanged?.Invoke(second.WinConditionReached);

            prevWinCondition = first.WinConditionReached;
            first.Content = temp;
            if (first.WinConditionReached != prevWinCondition)
                first.ContentChanged?.Invoke(first.WinConditionReached);

        }
    }

    public enum Neighbour
    {
        Lower,
        Right,
        Upper,
        Left
    }
}
