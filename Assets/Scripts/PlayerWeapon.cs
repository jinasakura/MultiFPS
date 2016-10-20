using UnityEngine;
using System.Collections;

//这个地方写的不好在哪？
//没有顶层抽象类，将武器的共同点提出来
//没有业务接口，有的话，外部可以通过调用接口来避免接触
//具体的武器类型，这样耦合性会降低
//这位作者估计是想通过枚举的方式来调用
[System.Serializable]
public class PlayerWeapon{

    public string weaponName = "Glock";

    public int damage = 10;
    public float range = 100f;

}
