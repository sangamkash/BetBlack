using System.Collections.Generic;
using UnityEngine;
using System;
using BulletEcho.DataSystem;
using BulletEcho.EnemySystem;
using BulletEcho.Items;
using BulletEcho.UI;

namespace BulletEcho
{ 
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        private UIManager uiManager => UIManager.Instance;
        private List<EnemyMovement> enemyList= new List<EnemyMovement>();

        public void AddEnemy(EnemyMovement enemy)
        {
            enemyList.Add(enemy);
        }
        public void RemoveEnemy(EnemyMovement enemy)
        {
            enemyList.Remove(enemy);
            if (enemyList.Count == 0)
            {
                GameOver(true);
            }
        }
        public void Play()
        {
            Time.timeScale = 1;
        }
        public void Pause()
        {
            Time.timeScale = 0;
        }
        public void GameOver(bool win)
        {
            uiManager.GameOver(win);
        }
    }
}
