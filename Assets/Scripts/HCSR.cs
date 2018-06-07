using FLOAT32 = System.Single;
using System.Collections.Generic;
using UnityEngine;
namespace Ardunity
{
    [AddComponentMenu("ARDUnity/Controller/Basic/Ultrasonic")]
    [HelpURL("https://sites.google.com/site/ardunitydoc/references/controller/hcsr04/hcsr04-script")]
    public class HCSR : ArdunityController, IWireInput<float>
    {

        public int trigPin = 2;
        public int echoPin = 3;
        public static int measuredDistance = 0;
        public FloatEvent OnDistanceChanged;
        private FLOAT32 _value = 0f;

        

        protected override void OnExecuted()
        {
            if (OnWireInputChanged != null)
                OnWireInputChanged(_value);
            OnDistanceChanged.Invoke(_value);
        }

        protected override void OnPop()
        {
            FLOAT32 newValue = _value;
            Pop(ref newValue);
            if (newValue != _value)
            {
                _value = newValue;
                updated = true;
            }
        }

        public override string GetCodeDeclaration()
        {
            return string.Format("{0} {1}({2:d}, T{3:d});", this.GetType().Name, GetCodeVariable(), id, trigPin);
        }

        public override string GetCodeVariable()
        {
            return string.Format("hcsr{0:d}", id);
        }

        //distacne 부분 수정
        public float distance
        {
            get
            {
                return (float)_value;
            }
        }

        void Update()
        {
            measuredDistance = (int)distance;
            //Debug.Log("Distance : " + distance.ToString() + "cm");

        }

        #region Wire Editor
        public event WireEventHandler<float> OnWireInputChanged;

        float IWireInput<float>.input
        {
            get
            {
                return distance;
            }
        }

        protected override void AddNode(List<Node> nodes)
        {
            base.AddNode(nodes);

            nodes.Add(new Node("trigger pin", "", null, NodeType.None, "Trigger Pin"));
            nodes.Add(new Node("echo pin", "", null, NodeType.None, "Echo Pin"));
            nodes.Add(new Node("Distance", "Distance", typeof(IWireInput<float>), NodeType.WireTo, "Input<float>"));
        }

        protected override void UpdateNode(Node node)
        {
            if (node.name.Equals("trigger pin"))
            {
                node.updated = true;
                node.text = string.Format("Trigger Pin: {0:d}", trigPin);
                return;
            }
            else if (node.name.Equals("echo pin"))
            {
                node.updated = true;
                node.text = string.Format("Echo Pin: {0:d}", echoPin);
                return;
            }
            else if (node.name.Equals("Distance"))
            {
                node.updated = true;
                return;
            }

            base.UpdateNode(node);
        }
        #endregion
    }
}