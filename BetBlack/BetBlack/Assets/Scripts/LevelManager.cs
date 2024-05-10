using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletEcho
{
    public class LevelManager : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1;
        }

        public void PlayEasyLevel()
        {
            SceneManager.LoadScene(GameConstant.SCENE_GAMEPLAYEASY);
        }

        public void PlayMidLevel()
        {
            SceneManager.LoadScene(GameConstant.SCENE_GAMEPLAYMID);
        }

        public void PlayHardLevel()
        {
            SceneManager.LoadScene(GameConstant.SCENE_GAMEPLAYHARD);
        }
    }
}