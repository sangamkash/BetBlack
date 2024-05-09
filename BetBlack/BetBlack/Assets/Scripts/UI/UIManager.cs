using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.DataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private Camera uICamera;
        [SerializeField] private Image equippingImg;
        [SerializeField] private RectTransform container;
        [SerializeField] private TextMeshProUGUI toastMsgTxt;
        [SerializeField] private GameObject healthIconPrefab;
        private GenericMonoPool<HealthIcon> healthIconPool;
        public Camera GetUICamera => uICamera;
       
        private GameManager gameManager=> GameManager.Instance;
        private GamePlayData data => DataManager.Instance.GetLevelData();

        private void Awake()
        {
            Instance = this;
            healthIconPool = new GenericMonoPool<HealthIcon>(healthIconPrefab);
        }

        public HealthIcon GetHealthUI()
        {
            return healthIconPool.GetObject(container);
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
