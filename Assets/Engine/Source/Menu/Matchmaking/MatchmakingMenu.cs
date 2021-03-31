using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace KAG.Menu
{
    public class MatchmakingMenu : MonoBehaviour
    {
        public Toast toast;

        public GameObject listContent;
        public GameObject listItem;

        [Space]
        public TextMeshProUGUI statsText;

        private void Start()
        {
            Refresh();
        }

        public void Close()
        {
            SceneManager.LoadScene(GameScene.Authentication);
        }

        public void Refresh()
        {
            toast.Show("Refreshing server list...");
        }
    }
}
