using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho;
using BulletEcho.Items.WeaponeSystem;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
   [SerializeField] private Transform shootPoint;
   [SerializeField] private float firePeriod=0.5f;
   [SerializeField] private float speed = 25;
   [SerializeField] private Bullet bulletPrefab;
   [SerializeField] private int bulletLife = 7;
   GenericMonoPool<Bullet> bulletPool;
   private float lastFireTime=-100f;

   private void Start()
   {
      bulletPool = new GenericMonoPool<Bullet>(bulletPrefab);
   }

   public void StartShooting(Transform target)
   {
      StartCoroutine(EnableShoot(target));
   }

   public void StopShooting()
   {
      StopAllCoroutines();
   }
   
   private IEnumerator EnableShoot(Transform target)
   {
      while (true)
      {
         transform.LookAt(target);
         if(lastFireTime<Time.time)
         {
            ShootBullet();
            lastFireTime = Time.time + firePeriod;
         }
         yield return new WaitForEndOfFrame();
      }
   }

   private void ShootBullet()
   {
      var bullet = bulletPool.GetObject(null);
      bullet.Init(shootPoint.forward,shootPoint.position, speed, bulletLife);
   }
}
