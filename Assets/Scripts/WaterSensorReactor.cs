using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ardunity
{
    public class WaterSensorReactor : ArdunityReactor
    {     

        [Range(0f, 8f)]
        public float maxIntensity = 1f;
        public int sensitivity = 10;

        private IWireInput<float> _analogInput;

        protected override void Awake()
        {
            base.Awake();

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (_analogInput != null)
            {
                if (_analogInput.input < 0.1)
                {   
                    
                }
                else if (_analogInput.input > 0.5)
                {
                    
                }

            }
            */
        }

        private void OnAnalogInputChanged(float value)
        {
            if (!this.enabled)
                return;

            Debug.Log("Value : " + _analogInput.input.ToString());

            this.transform.position = new Vector3(0, _analogInput.input * 10, 0);
            //if(_analogInput.input < 0.2)
            //{
            //    this.transform.position = new Vector3(0, 0, 0);
            //}
            //else if (_analogInput.input < 0.3)
            //{
            //    this.transform.position = new Vector3(0, 1, 0);
            //}
            //else if (_analogInput.input < 0.4)
            //{
            //    this.transform.position = new Vector3(0, 2, 0);
            //}
            //else if (_analogInput.input < 0.5)
            //{
            //    this.transform.position = new Vector3(0, 3, 0);
            //}
            //else
            //{
            //    this.transform.position = new Vector3(0, 4, 0);
            //}
        }

        protected override void AddNode(List<Node> nodes)
        {
            base.AddNode(nodes);

            nodes.Add(new Node("signal input", "GetValue", typeof(IWireInput<float>), NodeType.WireFrom, "Input<float>"));
        }

        protected override void UpdateNode(Node node)
        {

            if (node.name.Equals("signal input"))
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

            base.UpdateNode(node);
        }
    }

}