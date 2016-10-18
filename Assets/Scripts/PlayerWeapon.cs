using UnityEngine;
using System.Collections;

//注意：不要继承MonoBehaviour,否则检查面板上看不到Weapon里的属性
[System.Serializable]
public class PlayerWeapon{

    public string weaponName = "Glock";

    public float damage = 10f;
    public float range = 100f;

}
