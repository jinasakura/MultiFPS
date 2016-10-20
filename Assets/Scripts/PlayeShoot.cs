using UnityEngine;
using UnityEngine.Networking;

//简单的都没什么好讲的
public class PlayeShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "player";

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
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot (string _playerID,int _damage)
    {
        Debug.Log(_playerID + " has been shot");

        //客户端发来的伤害，会在这里通过服务端统一到各个客户端
        //死亡和重生是下个视频内容
        Player _player = GameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);
    }
}
