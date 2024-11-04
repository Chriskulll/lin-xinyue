/*Create By JesonHumber_f4*/
/*2023.3.10*/
/*Unity3D Digtal Twin Project*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(组件类型))]的意义是：
//为当前挂载该脚本的游戏物体添加需要的组件(这属于保险做法)
//该操作不需要引入其他命名空间
[RequireComponent(typeof(CharacterController))]

//引入播放动画的组件
[RequireComponent(typeof(Animator))]
public class HandControlCharacter : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    //人物移动速度（可自行调节）
    public float MoveSpeed;

    //获取键盘水平方向和垂直方向值用（GetAxis()方法）
    public float horizontal;
    public float vertical;

    //用于改变动画状态的变量
    public int move_var;

    //目标朝向
    public Vector3 target_dir = Vector3.zero;  //初始化为(0,0,0)，可自行调节

    // Start is called before the first frame update
    void Start()
    {
        //获取“角色控制器”组件（如果你要考虑物理碰撞等操作，就必须有这个组件）
        controller = GetComponent<CharacterController>();

        //获取“动画制作者组件”
        animator = GetComponent<Animator>();

        //初始化人物移动速度
        MoveSpeed = 0;

        //初始化动画状态变量为0（人物静止动画播放）
        move_var = 0;

        //校准“角色控制器”的胶囊体参数(物理碰撞体积)
        controller.center = new Vector3(0, 1, 0);
        controller.radius = 0.5f;
        controller.height = 2;
    }

    // Update is called once per frame
    void Update()
    {
        HandControl_Move();
    }

    public void HandControl_Move()
    {
        //GetAxis("Horizontal");对应的是键盘上的A和D键（水平键）
        horizontal = Input.GetAxis("Horizontal");
        //GetAxis("Vertical");对应的是键盘上的W和S键（垂直键）    
        vertical = Input.GetAxis("Vertical");

        //注：这上面的“水平垂直键”所映射的键位实际上可以更改，
        //它们只不过是默认规则,它们的最大值均为1
        if (horizontal != 0 || vertical != 0) //按键(默认：WASD)触发时就进行判断
        {
            //前进
            if (Input.GetKey(KeyCode.W))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                transform.rotation = Quaternion.LookRotation(target_dir);
                //Quaternion.LookRotation()：
                //传入一个向量值使物体朝向向量方向，
                //使物体朝向另一个物体只需要传入两个物体Position之间的Vector3差值即可


                //疾跑判断
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                {
                    move_var = 2;
                    MoveSpeed = 3.5f;
                }

            }

            //向后走
            else if (Input.GetKey(KeyCode.S))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                transform.rotation = Quaternion.LookRotation(target_dir);

                //疾跑判断
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
                {
                    move_var = 2;
                    MoveSpeed = 3.5f;
                }
            }

            //向左走
            else if (Input.GetKey(KeyCode.A))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                transform.rotation = Quaternion.LookRotation(target_dir);

                //疾跑判断
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
                {
                    move_var = 2;
                    MoveSpeed = 3.5f;
                }
                //transform.Translate(Vector3.forward * Time.deltaTime);
            }

            //向右走
            else if (Input.GetKey(KeyCode.D))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                transform.rotation = Quaternion.LookRotation(target_dir);

                //疾跑判断
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
                {
                    move_var = 2;
                    MoveSpeed = 3.5f;
                }
            }


            //动画更新
            //这个"BasicMotion"的名字，如果你们的Parameter名字不和我一致
            //你们就得改！
            animator.SetInteger("BasicMotion", move_var);
            //三维坐标方向坐标值更新
            //更新坐标值运用轴坐标参数
            target_dir = new Vector3(horizontal, 0, vertical);
            //人物移动更新
            controller.Move(target_dir * MoveSpeed * Time.deltaTime);

            //controller.Move()方法负责实现人物的移动，它会在给定的方向上移动游戏物体
            //给定方向需要：绝对运动增量值（方向与速度）和帧刷新时间（Time.deltaTime）
            //注：controller.Move()是不使用“重力”的，如果已经上坡，再回来时无法下坡！
            //    如果需要使用重力就必须自己手动写出“模拟重力”的代码。

            #region
            //也可以使用该方法代替controller.Move();
            //this.transform.Translate(dir * MoveSpeed * Time.deltaTime);
            #endregion
        }

        else
        {
            //默认为静止不动
            move_var = 0;

            //动画状态更新
            //这个"BasicMotion"的名字，如果你们的Parameter名字不和我一致
            //你们就得改！
            animator.SetInteger("BasicMotion", 0);
            MoveSpeed = 0;
        }
    }
}