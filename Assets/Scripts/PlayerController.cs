using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring setting:")]
    [SerializeField]
    private float jointSpring = 20f;
    //我把下面变量由视频里的JointDriveMode改成了Damper
    [SerializeField]
    private float positionDamper = 3f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        //SetJointSettings(jointSpring);//加不加都行
    }

    //为什么监听鼠标键盘要写在Update里？
    //FixUpdate是针对物理引擎的，而Update针对的游戏逻辑，键盘鼠标事件明显不属于物理部分
    void Update()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        //这里修改了
        float _cameraRotationX = _xRot * lookSensitivity;

        motor.RotateCamera(_cameraRotationX);

        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            //注意：这里传过去的是个只作用于Y轴的力
            _thrusterForce = Vector3.up * thrusterForce;
            //当按下键时，对物体施加力，这个时候必须取消Y轴上的弹力
            SetJointSettings(0f);
        }
        else
        {
            //松开键盘且物体不在初始位置，就施加弹力
            SetJointSettings(jointSpring);
        }
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        //注意以下代码已经跟5.3以下版本不同了
        joint.yDrive = new JointDrive {
            maximumForce = jointMaxForce,
            positionDamper = positionDamper,
            positionSpring = _jointSpring
        };
    }
}
