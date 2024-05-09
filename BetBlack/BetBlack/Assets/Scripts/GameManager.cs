using System.Collections.Generic;
using UnityEngine;
using System;
using BulletEcho.DataSystem;
using BulletEcho.Items;

namespace BulletEcho
{ 
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public void Play()
        {
            Time.timeScale = 1;
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void GameOver()
        {
            
        }
    }
}
