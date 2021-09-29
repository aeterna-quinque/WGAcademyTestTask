using Assets.Scripts.Serialization;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameField _field;
        [SerializeField]
        private DataSerializer _configSerializer;

        private void OnEnable()
        {
            FieldConfiguration config = _configSerializer.Deserialize();
            _field.Initialize(config);
            _field.GameEnded += () => Application.Quit();
        }

        private void OnDisable()
        {
            _field.GameEnded -= () => Application.Quit();
        }
    }
}
