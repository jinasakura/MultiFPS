using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    //注意：（摘自Manual）
    //SyncVar会把值从服务端同步到客户端在游戏就绪状态
    //应该在当前帧末把值传至客户端，不然会出问题
    //只有简单值才可以被赋予这个特性
    [SyncVar]
    private int currentHealth;

    void Awake()
    {
        setDefaults();
    }

    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " now has " + currentHealth);
    }

    private void setDefaults()
    {
        currentHealth = maxHealth;
    }
}
