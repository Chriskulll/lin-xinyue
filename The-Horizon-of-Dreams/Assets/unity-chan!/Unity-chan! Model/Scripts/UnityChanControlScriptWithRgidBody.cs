using UnityEngine;
using System.Collections;

namespace UnityChan
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class UnityChanControlScriptWithRgidBody : MonoBehaviour
    {
        public float animSpeed = 1.5f;              // アニメーション再生速度設定
        public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
        public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
        public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

        // 以下キャラクターコントローラ用パラメタ
        public float forwardSpeed = 7.0f;
        public float backwardSpeed = 2.0f;
        public float rotateSpeed = 2.0f;
        public float jumpPower = 10.0f;

        private CapsuleCollider col;
        private Rigidbody rb;
        private Vector3 velocity;
        private float orgColHight;
        private Vector3 orgVectColCenter;
        private Animator animator;
        private AnimatorStateInfo currentBaseState;
        private GameObject cameraObject;

        public float MoveSpeed;
        public float horizontal;
        public float vertical;
        public int move_var;
        public Vector3 target_dir = Vector3.zero;

        void Start()
        {
            animator = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();
            cameraObject = GameObject.FindWithTag("MainCamera");
            orgColHight = col.height;
            orgVectColCenter = col.center;
        }

        void FixedUpdate()
        {
            animator.SetBool("Jump", false);

            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            animator.SetFloat("Speed", vertical);
            animator.SetFloat("Direction", horizontal);
            animator.speed = animSpeed;
            currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            Vector3 velocity = new Vector3(0, 0, vertical);
            velocity = transform.TransformDirection(velocity);

            float MoveSpeed = 0;
            move_var = 0;

            if (Input.GetKey(KeyCode.W))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                velocity *= MoveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move_var = 3;
                    MoveSpeed = 3.5f;
                    velocity *= MoveSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                move_var = 2;
                MoveSpeed = 1.5f;
                velocity *= MoveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move_var = 2;
                    MoveSpeed = 3.5f;
                    velocity *= MoveSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                velocity *= MoveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move_var = 3;
                    MoveSpeed = 3.5f;
                    velocity *= MoveSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                move_var = 1;
                MoveSpeed = 1.5f;
                velocity *= MoveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move_var = 3;
                    MoveSpeed = 3.5f;
                    velocity *= MoveSpeed;
                }
            }
            else
            {
                move_var = 0;
                animator.SetInteger("BasicMotion", 0);
                MoveSpeed = 0;
            }

            animator.SetInteger("BasicMotion", move_var);

            transform.localPosition += velocity * Time.fixedDeltaTime;

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                transform.Rotate(0, horizontal * rotateSpeed, 0);
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (!animator.IsInTransition(0))
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    animator.SetBool("Jump", true);
                }
            }

            if (cameraObject != null)
            {
                Vector3 cameraOffset = new Vector3(0, 2, -5);
                cameraObject.transform.position = transform.position + transform.TransformDirection(cameraOffset);
                cameraObject.transform.LookAt(transform.position);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = new Vector3(143, 8, 34);
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
        }

        void resetCollider()
        {
            col.height = orgColHight;
            col.center = orgVectColCenter;
        }
    }
}