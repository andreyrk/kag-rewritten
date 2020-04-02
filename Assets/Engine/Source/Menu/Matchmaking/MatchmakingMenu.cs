﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KAG.Menu
{
    public class MatchmakingMenu : MonoBehaviour
    {
        public TextMeshProUGUI statsText;

        [Space]
        public GameObject listContent;
        public GameObject listItem;

        private void Awake()
        {
            Refresh();
        }

        public void Close()
        {
            SceneManager.LoadScene(GameEngine.Instance.menuScene.name);
        }

        public void Refresh()
        {
            Toast.Instance.Show("Refreshing server list...");

            GameSession.Instance.MatchmakeRefresh((serverList) =>
            {
                foreach (Transform child in listContent.transform)
                {
                    Destroy(child.gameObject);
                }

                serverList.Add(new ServerInfo
                {
                    Name = "Localhost",
                    IP = "127.0.0.1"
                });

                foreach (ServerInfo server in serverList)
                {
                    ServerEntry item = Instantiate(listItem, listContent.transform).GetComponent<ServerEntry>();
                    item.serverInfo = server;
                }
            });
        }
    }
}
