using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gameplay
{
    public class Arrow : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Arrow> ArrowPressed;

        public Tile ConnectedTile { get; set; }

        public void OnPointerClick(PointerEventData eventData) => ArrowPressed?.Invoke(this);
    }
}
