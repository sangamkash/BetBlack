using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.DataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private GameObject genericIconPrefab;
        [SerializeField] private GameObject GameOverScreen;
        [SerializeField] private TextMeshProUGUI gameOverText;
        
        private GenericMonoPool<HealthIcon> healthIconPool;
        private GenericMonoPool<GenericIcon> genericIconPool;
        public Camera GetUICamera => uICamera;
       
        private GameManager gameManager=> GameManager.Instance;
        private GamePlayData data => DataManager.Instance.GetLevelData();

        private void Awake()
        {
            Instance = this;
            healthIconPool = new GenericMonoPool<HealthIcon>(healthIconPrefab);
            genericIconPool = new GenericMonoPool<GenericIcon>(genericIconPrefab);
        }
        public HealthIcon GetHealthUI()
        {
            return healthIconPool.GetObject(container);
        }
        public GenericIcon GetGenericIconUI()
        {
            return genericIconPool.GetObject(container);
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
        public void BackBtnClick()
        {
            SceneManager.LoadScene(GameConstant.SCENE_LEVEL);
        }

        public void GameOver(bool isWin)
        { 
            gameOverText.text = isWin ? "you Won" : "you Loose";
            GameOverScreen.gameObject.SetActive(true);
            gameManager.Pause();
        }
    }
}
