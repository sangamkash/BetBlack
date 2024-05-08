using InventorySystem.DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private Image equippingImg;
        [SerializeField] private Button bagPackBtn;
        [SerializeField] private Button backBtn;
        [SerializeField] private CategoryCell categoryCellPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private TextMeshProUGUI toastMsgTxt;
        //private GenericMonoPool<CategoryCell> categoryCellPool;
       
        private GameManager gameManager=> GameManager.Instance;
        private GamePlayData data => DataManager.Instance.GetLevelData();

        private void Awake()
        {
            Instance = this;
            //categoryCellPool = new GenericMonoPool<CategoryCell>(categoryCellPrefab);
            // backBtn.onClick.AddListener(HideUI);
            // bagPackBtn.onClick.AddListener(ShowUI);
        }

        private void HideUI()
        {
            // inventoryUI.gameObject.SetActive(false);
        }

        private void ShowUI()
        {
            // inventoryUI.gameObject.SetActive(true);
            //categoryCellPool.ResetAllPool();
            // var i = 0;
            // if (data == null)
            //     return;
            // foreach (var item in data.bagPackItems)
            // {
            //     var cell = categoryCellPool.GetObject(container);
            //     cell.Init(item.Key, item.Value, gameManager.EquipItem, HideUI);
            //     cell.GetComponent<RectTransform>().SetSiblingIndex(i);
            //     i++;
            // }
        }

        public void ShowEquippingImg(float value,Color color)
        {
            equippingImg.fillAmount = value;
            equippingImg.color = color;
        }

        public void ShowMToastMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) 
                return;
            StopAllCoroutines();
            StartCoroutine(ShowToast(msg));

        }

        private IEnumerator ShowToast(string msg,float time=1.5f)
        {
            toastMsgTxt.text = msg;
            toastMsgTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
            toastMsgTxt.gameObject.SetActive(false);
        }
    }
}
