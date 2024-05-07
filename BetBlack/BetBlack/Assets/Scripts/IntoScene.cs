using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InventorySystem
{
    public class IntoScene : MonoBehaviour
    {
        [Header("refs")]
        [SerializeField] private Button playBtn;

        private void Awake()
        {
            playBtn.onClick.AddListener(OnPlayBtnClick);
        }

        private void OnPlayBtnClick()
        {
            SceneManager.LoadScene(GameConstant.SCENE_GAMEPLAY);
        }
    }
}