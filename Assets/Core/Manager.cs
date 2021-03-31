using Mirror;
using UnityEngine;

public class Manager : NetworkManager
{
    public Map map;

    public override void Awake()
    {
        base.Start();

        map.loadingFinished.AddListener(() => {
            StartHost();
        });
    }
}
