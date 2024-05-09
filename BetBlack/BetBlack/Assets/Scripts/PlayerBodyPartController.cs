using BulletEcho.UI;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.Items;
using UnityEditor;
using UnityEngine;

namespace BulletEcho.PlayerSystem
{
    public enum BodyPart
    { 
        head,
        handLeft,
        handRight,
        legLeft,
        legRight
    }

    public class PlayerBodyPartController : MonoBehaviour
    {
        [Header("Body refs")]
        [SerializeField] private Transform head;
        [SerializeField] private Transform handLeft;
        [SerializeField] private Transform handRight;
        [SerializeField] private Transform legLeft;
        [SerializeField] private Transform legRight;
        private Dictionary<BodyPart, GameObject> bodyPartOccupied= new Dictionary<BodyPart, GameObject>();

        public void AssignObjToBodyPart(BodyPart bodyPart, GameObject gameObj)
        {
            if (gameObj != null)
            {
                ThrowObject(bodyPart);
                if (bodyPartOccupied.ContainsKey(bodyPart))
                {
                    bodyPartOccupied[bodyPart] = gameObj;
                }
                else
                {
                    bodyPartOccupied.Add(bodyPart, gameObj);
                }
                var ci= gameObj.GetComponent<GenericCarryableItem>();
                if(ci != null)
                {
                    ci.pickUp(GetBodyPart(bodyPart), ()=>MarkAsUnAssigned(bodyPart));
                }
            }
        }

        public Transform GetBodyPart(BodyPart bodyPart)
        {
            switch (bodyPart)
            {
                case BodyPart.head:
                    return head;
                case BodyPart.handLeft:
                    return handLeft;
                case BodyPart.handRight:
                    return handRight;
                case BodyPart.legLeft:
                    return legLeft;
                case BodyPart.legRight:
                    return legRight;
                default:
                    return null;
            }
        }

        public void ThrowObject(BodyPart bodyPart)
        {
            if (bodyPartOccupied.ContainsKey(bodyPart))
            {
                ThrowObject(bodyPartOccupied[bodyPart]);
            }
        }

        private void ThrowObject(GameObject gameObj)
        {
            if ((gameObj ==null))
            {
                return;
            }
            var ci = gameObj.GetComponent<GenericCarryableItem>();
            if (ci != null)
            {
                ci.ThrowAway();
            }
        }

        private void MarkAsUnAssigned(BodyPart bodyPart)
        {
            if(bodyPartOccupied.ContainsKey(bodyPart))
            {
                bodyPartOccupied[bodyPart]=null;
            }
        }
    }
}
