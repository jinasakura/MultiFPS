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

















    //问题3：这里个函数的作用让我费解
    //可能会调用这个函数的时候，就是Start里当“我”控制的Player时，禁用主摄像机
    //可是当“我”这个Player死后，才会调用主摄像机，那现在刚禁用了现在又恢复是怎么回事
    //并且这个代码注释掉并没有影响其他功能
    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

}
