using UnityEngine;
using TMPro;

namespace KAG.Menu
{
    public class ServerEntry : MonoBehaviour
    {
        public Toast toast;

        public TextMeshProUGUI label;
        public ServerInfo serverInfo;

        private void Awake()
        {

        }

        private void Start()
        {
            label.text = string.Format("{0} ({1})", serverInfo.Name, serverInfo.IP);
        }

        public void Join()
        {
            //gameManager.networkAddress = serverInfo.IP;
            //gameManager.StartClient();

            toast.Show("Trying to connect to " + serverInfo.IP + "...");
        }
    }
}
