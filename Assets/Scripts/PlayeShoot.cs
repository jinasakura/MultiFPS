using UnityEngine;
using UnityEngine.Networking;

public class PlayeShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "player";

    //设计优点：也许可以通过代码换武器，前提武器统一属性
    //设计缺点：耦合性太高，改成接口形式更好
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

	void Start ()
    {
	    if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
	}
	
	void Update ()
    {
        //这里需要设置一下Project Setting--Input--Fire1下的Positive Button
        //设置为空，就是说只能点击鼠标左键来发射子弹
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    //Unity设计的这种把客户端服务器放在一起的写法，我是不能接受
    //我认为违背了业务逻辑单一职责原则
    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        //这个地方，我晕了很久，晕在为什么我始终无法检测到鼠标点到的对象？
        //原因非常简单，这里的跟踪光线是从摄像机发出来的，而摄像机位于屏幕的正中间，
        //所以，子弹是从屏幕正中间发出来的！不要以为鼠标指哪打哪！
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShot (string _ID)
    {
        Debug.Log(_ID + " has been shot");

        Destroy(GameObject.Find(_ID));
    }
}
