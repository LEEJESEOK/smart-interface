using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ardunity
{
    [AddComponentMenu("ARDUnity/Reactor/Physics/PushButtonReactor")]
    public class PushButtonRector : ArdunityReactor
    {
        private IWireInput<Trigger> _triggerInput;
        public DialSlider dialSlider; // 회전시킬 오브젝트
        //방향 결정
        public bool ToRight = false;
        public bool ToLeft = false;

        public void ResetOneShot()
        {
        }

        //오브젝트 이동 함수
        private void MoveObject(Trigger value)
        {
            if (value.value)
            {
                if (ToLeft)
                {
                    dialSlider.angle += Time.deltaTime * 1000f;
                }
                else if (ToRight)
                {
                    dialSlider.angle -= Time.deltaTime * 1000f;
                }
                else
                    return;
            }
        }

        protected override void AddNode(List<Node> nodes)
        {
            base.AddNode(nodes);

            nodes.Add(new Node("active", "Active", typeof(IWireInput<Trigger>), NodeType.WireFrom, "Input<Trigger>"));
        }

        protected override void UpdateNode(Node node)
        {
            if (node.name.Equals("active"))
            {
                node.updated = true;
                if (node.objectTarget == null && _triggerInput == null)
                    return;

                if (node.objectTarget != null)
                {
                    if (node.objectTarget.Equals(_triggerInput))
                        return;
                }

                if (_triggerInput != null)
                    _triggerInput.OnWireInputChanged -= MoveObject;

                _triggerInput = node.objectTarget as IWireInput<Trigger>;
                if (_triggerInput != null)
                    _triggerInput.OnWireInputChanged += MoveObject;
                else
                    node.objectTarget = null;

                return;
            }
            base.UpdateNode(node);
        }
    }



}
