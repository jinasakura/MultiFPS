using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string REMOTE_LAYER_NAME = "RemotePlayer";

    private Camera sceneCamera;

    
    void OnEnable()
    {
        sceneCamera = Camera.main;
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera.gameObject.SetActive(false);
        }
    }

    //启动客户端，就向游戏控制类里注册当前Player
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(REMOTE_LAYER_NAME);
    }

    void DisableComponents ()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }


    void OnDisable()
    {
        sceneCamera.gameObject.SetActive(true);

        GameManager.UnRegisterPlayer(transform.name);
    }

}
