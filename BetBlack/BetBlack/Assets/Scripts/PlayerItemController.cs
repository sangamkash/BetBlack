using System;
using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using InventorySystem.Items;
using UnityEngine;

namespace InventorySystem.PlayerSystem
{
    public class PlayerItemController : MonoBehaviour
    {
        [SerializeField] private PlayerBodyPartController partController;
        private UIManager uiManager=>UIManager.Instance;
        private HashSet<GameObject> hs = new HashSet<GameObject>();    
        private List<GameObject> activeGameObjs= new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            var currentObj = other.gameObject;
            if(hs.Contains(currentObj))
                return;
            Debug.Log(currentObj.name+"::"+other.gameObject.tag);
            switch (other.gameObject.tag)
            {
                case GameConstant.TAG_FOOD:
                    ShowEquipping(2, Color.green, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        t.pickUp(partController.GetBodyPart(BodyPart.handRight), null);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEARABLE:
                    ShowEquipping(2, Color.green, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        t.pickUp(partController.GetBodyPart(BodyPart.handRight), null);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEAPON:
                    ShowEquipping(2, Color.green, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        Debug.Log($"{t!=null} || ");
                        t.pickUp(partController.GetBodyPart(BodyPart.handRight), null);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                default:
                    activeGameObjs.Remove(currentObj);
                    currentObj = null;
                    break;
            }
        }

        private void ShowEquipping(float time, Color color,Action OnEquipped)
        {
            StopAllCoroutines();
            StartCoroutine(ShowEquippingAnim(time, color,OnEquipped));
        }

        private IEnumerator ShowEquippingAnim(float time,Color color,Action OnEquipped)
        {
            var startTime = Time.time;
            while (Time.time-startTime<time)
            {
                var fillAmount = (Time.time - startTime) / time;
                uiManager.ShowEquippingImg(fillAmount, color);
                yield return new WaitForEndOfFrame();
            }
            uiManager.ShowEquippingImg(0f, color);
            OnEquipped?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            var currentObj = other.gameObject;
            if (hs.Contains(currentObj))
            {
                activeGameObjs.Remove(currentObj);
                hs.Remove(currentObj);
                uiManager.ShowEquippingImg(0, Color.white);
            }
        }

        public void EquipItem(GameObject gameObj)
        {
            partController.AssigneObjToBodyPart(BodyPart.handRight, gameObj);
        }
    }
}
