using UnityEngine;
using System.Collections.Generic;
using System;
using UnityStandardAssets.CrossPlatformInput;

namespace Ardunity
{
    [RequireComponent(typeof(ThirdPersonCharacter))] //ThirdPersonUserControl 복사

    [AddComponentMenu("ARDUnity/Reactor/Effect/JoyStickReactor")]
    public class JoyStickReactor : ArdunityReactor
    {

        private ThirdPersonCharacter m_Character;
        private Transform m_Cam;
        public Vector3 m_CamForward;
        public Vector3 m_Move;
        private bool m_Jump;

        public float h = 0;
        public float v = 0;

        private bool start;


        // 초기화
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // ThirdPersonCharacter객체와 m_Character을 연결
            m_Character = GetComponent<ThirdPersonCharacter>();

            start = false;
        }



        public enum Direction
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT,
            STOP
        }

        //[Range(0f, 8f)]
        [Range(0f, 8f)]
        public float maxIntensity = 1f;
        public float moveRate = 1f;
        public int sensitivity = 10;
<<<<<<< HEAD
        
        //[Range(0f, 8f)]
=======
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
        //public float cutoffIntensity = 0.5f;

        private IWireInput<float> _analogInput_x;
        private IWireInput<float> _analogInput_y;
        private IWireOutput<float> _analogOutput;
        private float _x = 0;
        private float _y = 0;

<<<<<<< HEAD
        private bool start;

        public static float _h;
        public static float _v;
=======
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06

        protected override void Awake()
        {
            base.Awake();
        }

<<<<<<< HEAD
        // Use this for initialization
        void Start()
        {
            start = false;
        }
=======
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06

        void OnEnable()
        {
            if (_analogInput_x != null && _analogInput_y != null)
            {
                _x = maxIntensity * Mathf.Clamp(_analogInput_x.input, 0f, 1f);
                _y = maxIntensity * Mathf.Clamp(_analogInput_y.input, 0f, 1f);
            }
        }

<<<<<<< HEAD
        // Update is called once per frame
=======
        // 조이스틱Update함수
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
        void Update()
        {
            if (_analogInput_x != null && _analogInput_y != null)
            {
                //move object
<<<<<<< HEAD
                //Debug.Log("X : " + _x.ToString() + " / Y : " + _y.ToString());

                // GetDirection 함수의 반환값으로 방향을 판별함

                switch (GetDirection())
                {
                    case 0: return;
                    case 1: this.transform.position += Vector3.forward * moveRate * Time.deltaTime;
                        break;
                    case 2: this.transform.position += Vector3.back * moveRate * Time.deltaTime;
                        break;
                    case 3: this.transform.position += Vector3.left * moveRate * Time.deltaTime;
                        break;
                    case 4: this.transform.position += Vector3.right * moveRate * Time.deltaTime;
                        break;

                    // x성분과 y성분 벡터를 더하여 대각선 이동 구현
                    case 5: this.transform.position += Vector3.forward * moveRate * Time.deltaTime + Vector3.left * moveRate * Time.deltaTime;
                        break;
                    case 6: this.transform.position += Vector3.forward * moveRate * Time.deltaTime + Vector3.right * moveRate * Time.deltaTime;
                        break;
                    case 7: this.transform.position += Vector3.back * moveRate * Time.deltaTime + Vector3.left * moveRate * Time.deltaTime;
                        break;
                    case 8: this.transform.position += Vector3.back * moveRate * Time.deltaTime + Vector3.right * moveRate * Time.deltaTime;
                        break;
                        
=======
                Debug.Log("X : " + _x+ " / Y : " + _y + " / Direction : " + GetDirection());
                switch (GetDirection())
                {
                    case 0: // stop
                        h = 0; v = 0;
                        break;
                    case 1: // forward
                        h = 0; v = 10;
                        break;
                    case 2: // back
                        h = 0; v = -10;
                        break;
                    case 3: // left
                        h = -10; v = 0;
                        break;
                    case 4: // right
                        h = 10; v = 0;
                        break;
                    case 5: // left + forward
                        h = -10; v = 10;
                        break;
                    case 6: // right + forward
                        h = 10; v = 10;
                        break;
                    case 7: // left + back
                        h = -10; v = -10;
                        break;
                    case 8: // right + back
                        h = 10; v = -10;
                        break;
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
                }
            }
        }

<<<<<<< HEAD
        // 조이스틱으로 입력받는 값에 따라 각각의 방향을 반환함
=======
        //조이스틱GetDirection
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
        private int GetDirection()
        {
            // connect 전 입력받는 쓰레기값을 무시하기 위해 start 변수를 사용
            if (start == false && (int)_x != 0 && (int)_y != 0)
                start = true;
            if (start == false)
                return 0;

            // 기존 코드는 대각선으로 이동할 때 x와 y 값이 같으면 정지상태로 인식하는 문제가 있어서 
            // 정지상태를 기존 (int)_x == (int)_y에서 변경함
<<<<<<< HEAD
            if ((int)_x == 5 && (int)_y == 5)
                return 0; //Stop

            else if ((int)_x > 5 && (int)_y > 5)
                return 5; // Left forward
            else if ((int)_x < 5 && (int)_y > 5)
                return 6; // Right forward
            else if ((int)_x > 5 && (int)_y < 5)
                return 7; // Left backward
            else if ((int)_x < 5 && (int)_y < 5)
                return 8; // Right backward

            // 대각선 방향 추가
            else if ((int)_x == 5 && (int)_x < (int)_y)
                return 1;    //Forward
            else if ((int)_x == 5 && (int)_x > (int)_y)
                return 2;    //Backward
            else if ((int)_x > (int)_y && (int)_y == 5)
                return 3;    //Left
            else if ((int)_x < (int)_y && (int)_y == 5)
                return 4;    //Right
=======
            // 사용한 조이스틱의 초기값이 5.2, 4.9
            if (_x >= 4.5f && _x <= 5.5f && _y >= 4.5f && _y <= 5.5f)
                return 0; //Stop

            else if (_x >= 4.5f && _x <= 5.5f && _x < _y)
                return 1;    //Forward
            else if (_x >= 4.5f && _x <= 5.5f && _x > _y)
                return 2;    //Backward
            else if (_x > _y && _y >= 4.5f && _y <= 5.5f)
                return 3;    //Left
            else if (_x < _y && _y >= 4.5f && _y <= 5.5f)
                return 4;    //Right

            else if (_x > 5.5f && _y > 5.5f)
                return 5; // Left forward
            else if (_x < 4.5f && _y > 5.5f)
                return 6; // Right forward
            else if (_x > 5.5f && _y < 4.5f)
                return 7; // Left backward
            else if (_x < 5.5f && _y < 4.5f)
                return 8; // Right backward

>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06

            else
                return 0;
        }


        private void OnAnalogInputChanged_X(float value)
        {
            if (!this.enabled)
                return;

            _x = maxIntensity * Mathf.Clamp(_analogInput_x.input, 0f, 1f) * sensitivity;

        }

        private void OnAnalogInputChanged_Y(float value)
        {
            if (!this.enabled)
                return;

            _y = maxIntensity * Mathf.Clamp(_analogInput_y.input, 0f, 1f) * sensitivity;
        }

        protected override void AddNode(List<Node> nodes)
        {
            base.AddNode(nodes);

            nodes.Add(new Node("x_Intensity", "X Intensity", typeof(IWireInput<float>), NodeType.WireFrom, "Input<float>"));
            nodes.Add(new Node("y_Intensity", "Y Intensity", typeof(IWireInput<float>), NodeType.WireFrom, "Input<float>"));
        }

        protected override void UpdateNode(Node node)
        {

            if (node.name.Equals("x_Intensity"))
            {
                node.updated = true;
                if (node.objectTarget == null && _analogInput_x == null)
                    return;

                if (node.objectTarget != null)
                {
                    if (node.objectTarget.Equals(_analogInput_x))
                        return;
                }

                if (_analogInput_x != null)
                    _analogInput_x.OnWireInputChanged -= OnAnalogInputChanged_X;

                _analogInput_x = node.objectTarget as IWireInput<float>;
                if (_analogInput_x != null)
                    _analogInput_x.OnWireInputChanged += OnAnalogInputChanged_X;
                else
                    node.objectTarget = null;

                return;
            }
            else if (node.name.Equals("y_Intensity"))
            {
                node.updated = true;
                if (node.objectTarget == null && _analogInput_y == null)
                    return;

                if (node.objectTarget != null)
                {
                    if (node.objectTarget.Equals(_analogInput_y))
                        return;
                }

                if (_analogInput_y != null)
                    _analogInput_y.OnWireInputChanged -= OnAnalogInputChanged_Y;

                _analogInput_y = node.objectTarget as IWireInput<float>;
                if (_analogInput_y != null)
                    _analogInput_y.OnWireInputChanged += OnAnalogInputChanged_Y;
                else
                    node.objectTarget = null;

                return;
            }

            base.UpdateNode(node);
        }


        //ThirdPersonUserControl의 FixedUpdate함수 변경
        public void FixedUpdate()
        {

            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }


    }
}