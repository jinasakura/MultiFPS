using UnityEngine;
using System.Collections;

//因为加上下面这个特性，如果你要删除PlayerMotor，会提示不能删除
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    //注意这个格式，私有+[SerializeField]，不会暴露字段
    //字段和属性是不一样的，我的理解是字段是没有get、set方法
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //计算移动位移
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //标准化移动位移后，就只保留方向，那么乘以速度，就是这个物体在那个方向走的距离了
        //注意是normalized不是Normalize，区别是，前者在不改变原来的数据情况下，返回一个新的数
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        //计算物体绕Y轴旋转
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //计算摄像机绕X轴旋转
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(_cameraRotation);
    }
}
