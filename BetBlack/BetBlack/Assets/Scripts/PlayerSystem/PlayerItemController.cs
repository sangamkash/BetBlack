using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.Items;
using BulletEcho.UI;
using UnityEngine;

namespace BulletEcho.PlayerSystem
{
    public class PlayerItemController : MonoBehaviour
    {
        [SerializeField] private PlayerBodyPartController partController;
        private UIManager uiManager=>UIManager.Instance;
        private HashSet<GameObject> hs = new HashSet<GameObject>();    
        private List<GameObject> activeGameObjs= new List<GameObject>();
        private GameObject currentEqpObj = null;
        private void OnTriggerEnter(Collider other)
        {
            var currentObj = other.gameObject;
            if(hs.Contains(currentObj))
                return;
            hs.Add(currentObj);
            switch (other.gameObject.tag)
            {
                case GameConstant.TAG_FOOD:
                    ShowEquipping(1, Color.green,currentObj, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        partController.AssignObjToBodyPart(BodyPart.handRight, currentObj);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEARABLE:
                    ShowEquipping(1, Color.green,currentObj, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        partController.AssignObjToBodyPart(BodyPart.handRight, currentObj);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                case GameConstant.TAG_WEAPON:
                    ShowEquipping(1, Color.green,currentObj, () =>
                    {
                        var t=currentObj.GetComponent<GenericCarryableItem>();
                        partController.AssignObjToBodyPart(BodyPart.handRight, currentObj);
                    });
                    activeGameObjs.Add(currentObj);
                    break;
                default:
                    activeGameObjs.Remove(currentObj);
                    currentObj = null;
                    break;
            }
        }

        private void ShowEquipping(float time, Color color,GameObject currentObj,Action OnEquipped)
        {
            currentEqpObj = currentObj;
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
                if (currentObj == currentEqpObj)
                {
                    currentEqpObj = null;
                    StopAllCoroutines();
                    uiManager.ShowEquippingImg(0, Color.white);
                }
                activeGameObjs.Remove(currentObj);
                hs.Remove(currentObj);
            }
        }

        public void EquipItem(GameObject gameObj)
        {
            partController.AssignObjToBodyPart(BodyPart.handRight, gameObj);
        }
    }
}
