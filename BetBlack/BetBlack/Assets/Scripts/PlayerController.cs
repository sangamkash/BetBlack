using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerBodyPartController partController;
        public UIManager uiManager=>UIManager.Instance;
        private HashSet<GameObject> hs = new HashSet<GameObject>();    
        private List<GameObject> activeGameObjs= new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            var currentObj = other.gameObject;
            if(hs.Contains(currentObj))
                return;
            switch (other.gameObject.tag)
            {
                case GameConstant.TAG_STONE:
                    uiManager.ShowMTostMessage("Press F key to pick stone");
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_FOOD:
                    uiManager.ShowMTostMessage("Press F key to pick food");
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEARABLE:
                    uiManager.ShowMTostMessage("Press F key to pick werable");
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEAPON:
                    uiManager.ShowMTostMessage("Press F key to pick weapon");
                    activeGameObjs.Add(currentObj);
                    break;
                default:
                    activeGameObjs.Remove(currentObj);
                    currentObj = null;
                    break;
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (activeGameObjs.Count > 0)
                {
                    var t = activeGameObjs[activeGameObjs.Count - 1];
                    if (t != null)
                    {
                        partController.AssigneObjToBodyPart(BodyPart.handRight, t);
                        uiManager.ShowMTostMessage("Press E key to store item");
                    }
                    activeGameObjs.Remove(t);
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                partController.ThrowObject(BodyPart.handRight);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                partController.StoreInInvontry(BodyPart.handRight);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            var currentObj = other.gameObject;
            if (hs.Contains(currentObj))
            {
                activeGameObjs.Remove(currentObj);
                hs.Remove(currentObj);
            }
        }

        public void EquipItem(GameObject gameObj)
        {
            partController.AssigneObjToBodyPart(BodyPart.handRight, gameObj);
        }
    }
}
