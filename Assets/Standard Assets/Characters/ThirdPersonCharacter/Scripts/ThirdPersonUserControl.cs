<<<<<<< HEAD
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        
=======
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

        //ThirdPersonUserControl에 있는 변수 복사-----------------------------------
        private ThirdPersonCharacter m_Character;
        private Transform m_Cam;
        public Vector3 m_CamForward;
        public Vector3 m_Move;
        private bool m_Jump;

        // 좌우 입력에 따라 변화할 값
        public float h = 0;
        public float v = 0;
        //----------------------------------------------------------------------------


        // 초기화
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
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

<<<<<<< HEAD
            // get the third person character ( this should never be null due to require component )
=======
            // ThirdPersonCharacter객체와 m_Character을 연결
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


<<<<<<< HEAD
        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
=======

        public enum Direction
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT,
            STOP
        }

        [Range(0f, 8f)]
        public float maxIntensity = 1f;
        public float moveRate = 1f;
        public int sensitivity = 10;
        //[Range(0f, 8f)]
        //public float cutoffIntensity = 0.5f;
        public static int xxx = 0;
        private Light _light;
        private IWireInput<float> _analogInput_x;
        private IWireInput<float> _analogInput_y;
        private IWireOutput<float> _analogOutput;
        private float _x = 0;
        private float _y = 0;


        protected override void Awake()
        {
            base.Awake();

            _light = GetComponent<Light>();
        }


        void OnEnable()
        {
            if (_analogInput_x != null && _analogInput_y != null)
            {
                _x = maxIntensity * Mathf.Clamp(_analogInput_x.input, 0f, 1f);
                _y = maxIntensity * Mathf.Clamp(_analogInput_y.input, 0f, 1f);
            }
        }

        // 조이스틱Update함수
        void Update()
        {
            if (_analogInput_x != null && _analogInput_y != null)
            {
                //move object
                Debug.Log("X : " + ((int)_x).ToString() + " / Y : " + ((int)_y).ToString());
                switch (GetDirection())
                {
                    //GetDirection함수에서 결정된 경로대로 움직이게 함.
                    //h와 v값을 조이스틱 움직이는 방향에 따라 조정

                    case 1:     // 앞왼
                        h = 10; v = 10; break;

                    case 2:     // 앞
                        h = 0; v = 10; break;

                    case 3:     // 앞오
                        h = -10; v = 10; break;

                    case 4:     // 왼
                        h = 10; v = 0; break;

                    case 5: h = 0; v = 0; break;  //정지

                    case 6:     // 오
                        h = -10; v = 0; break;

                    case 7:     // 뒤왼
                        h = 10; v = -10; break;

                    case 8:     // 뒤
                        h = 0; v = -10; break;

                    case 9:     // 뒤오
                        h = -10; v = -10; break;

                }
            }
        }

        //조이스틱GetDirection
        private int GetDirection()
        {
            //  1 2 3 
            //  4 5 6 
            //  7 8 9   방향으로 움직이게 함 

            if ((int)_x < 4)                             // 왼
            {
                if ((int)_y > 4) { return 3; }        // 앞
                else if ((int)_y < 4) { return 9; }    // 뒤
                else { return 6; }                     // 왼쪽으로만
            }
            else if ((int)_x > 4)                       // 오
            {
                if ((int)_y > 4) { return 1; }          // 앞
                else if ((int)_y < 4) { return 7; }     // 뒤
                else { return 4; }                      // 오른쪽으로만
            }
            else
            {
                if ((int)_y > 4) { return 2; }          // 앞
                else if ((int)_y < 4) { return 8; }     // 뒤
                else { return 5; }                      // 정지
            }
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

>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
<<<<<<< HEAD
                m_Move = v*m_CamForward + h*m_Cam.right;
=======
                m_Move = v * m_CamForward + h * m_Cam.right;
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
            }
            else
            {
                // we use world-relative directions in the case of no main camera
<<<<<<< HEAD
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
=======
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
<<<<<<< HEAD
    }
}
=======


    }
}
>>>>>>> c1ef6a5bfa667e77207be04c0589e05dc4ac4e06
