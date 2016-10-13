using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    private Camera sceneCamera;

    void OnEnabled()
    {
        sceneCamera = Camera.main;
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera.gameObject.SetActive(false);
        }
    }


    void OnDisable()
    {
        sceneCamera.gameObject.SetActive(true);
    }

}
