using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //FixedUpdate方法是固定时间来执行，它不管当前游戏的运行速度等情况，它每秒会调用1/time.fixeddeltatime次。
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            //Time.fixedDeltaTime是固定值，可在Edit->Project Settings->Time下设置
            //（当前位移+速度*时间）
            //如果不乘以Time.fixedDeltaTime，相当于velocity*1，等于物体会移动到一秒后该去的地方，而这里明显是小于1秒的
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            //transform.position= transform.position+ velocity* Time.fixedDeltaTime;
        }
    }

    private void PerformRotation()
    {
        //一定要注意下这个问题，我已经通过Constraints，锁定了旋转，为什么Player还是能动？
        //回答见注3
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //这里要加负号，不然正好和鼠标方向相反
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
