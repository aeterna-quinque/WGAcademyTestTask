using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Factory
{
    public abstract class GameObjectFactory : ScriptableObject
    {
        private Scene _scene;

        public void Reclaim<T>(T instance) where T : MonoBehaviour
        {
            Destroy(instance.gameObject);
        }

        protected T Get<T>(T prefab) where T : MonoBehaviour
        {
            LoadScene();
            T instance = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
            return instance;
        }

        private void LoadScene()
        {
            if (!_scene.isLoaded)
            {
                if (Application.isEditor)
                {
                    _scene = SceneManager.GetSceneByName(name);
                    if (!_scene.isLoaded)
                    {
                        _scene = SceneManager.CreateScene(name);
                    }
                }
                else
                {
                    _scene = SceneManager.CreateScene(name);
                }
            }
        }
    }
}
