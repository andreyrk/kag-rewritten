using UnityEngine;
using UnityEngine.SceneManagement;

namespace KAG.Menu
{
    public class Menu : MonoBehaviour
    {
        private void Start()
        {

        }

        public void ShowSingleplayer()
        {
            SceneManager.LoadScene(GameScene.Match);
        }

        public void ShowMultiplayer()
        {
            SceneManager.LoadScene(GameScene.Matchmaking);
        }

        public void Settings()
        {

        }

        public void Logout()
        {
            GameSession.Instance.Logout(() =>
            {
                SceneManager.LoadScene(GameScene.Authentication);
            });
        }
    }
}
