using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ardunity
{
    public class FlameRector : ArdunityReactor
    {

        public ParticleSystem flamePaticle;

        [Range(0f, 8f)]
        public float maxIntensity = 1f;
        public int sensitivity = 10;

        private IWireInput<float> _analogInput;
        private IWireOutput<bool> _digitalOutput;

        protected override void Awake()
        {
            base.Awake();
            
        }

        void OnEnable()
        {
            if (_analogInput != null)
            {
               // _y = maxIntensity * Mathf.Clamp(_analogInput_y.input, 0f, 1f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_analogInput != null)
            {
                if(_analogInput.input < 0.1)
                {
                    flamePaticle.Stop();
                    _digitalOutput.output = false;
                }else if(_analogInput.input > 0.5)
                {
                    flamePaticle.Play();
                    _digitalOutput.output = true;
                }
              
            }
        }


        private void OnAnalogInputChanged(float value)
        {
            if (!this.enabled)
                return;

            Debug.Log("Value : " + _analogInput.input.ToString());

        }


        protected override void AddNode(List<Node> nodes)
        {
            base.AddNode(nodes);

            nodes.Add(new Node("FlameValue", "GetValue", typeof(IWireInput<float>), NodeType.WireFrom, "Input<float>"));
            nodes.Add(new Node("Buzzer", "SetValue", typeof(IWireOutput<bool>), NodeType.WireFrom, "Output<bool>"));
        }

        protected override void UpdateNode(Node node)
        {

            if (node.name.Equals("FlameValue"))
            {
                node.updated = true;
                if (node.objectTarget == null && _analogInput == null)
                    return;

                if (node.objectTarget != null)
                {
                    if (node.objectTarget.Equals(_analogInput))
                        return;
                }

                if (_analogInput != null)
                    _analogInput.OnWireInputChanged -= OnAnalogInputChanged;

                _analogInput = node.objectTarget as IWireInput<float>;
                if (_analogInput != null)
                    _analogInput.OnWireInputChanged += OnAnalogInputChanged;
                else
                    node.objectTarget = null;

                return;
            }
            else if (node.name.Equals("Buzzer"))
            {
                node.updated = true;
                if (node.objectTarget == null && _digitalOutput == null)
                    return;

                if (node.objectTarget != null)
                {
                    if (node.objectTarget.Equals(_digitalOutput))
                        return;
                }

                _digitalOutput = node.objectTarget as IWireOutput<bool>;
                if (_digitalOutput == null)
                    node.objectTarget = null;

                return;
            }

            base.UpdateNode(node);
        }
    }
}



