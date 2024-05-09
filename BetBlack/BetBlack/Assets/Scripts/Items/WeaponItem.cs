using System;
using System.Collections;
using BulletEcho.DataSystem;
using BulletEcho.Items.WeaponeSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace BulletEcho.Items
{
    public class WeaponItem : GenericStorableItem
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float firePeriod=2;
        [SerializeField] private float speed = 25;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int bulletLife = 7;
        GenericMonoPool<Bullet> bulletPool;
        private float lastFireTime=-100f;
        private Coroutine Shooting;

        private void Awake()
        {
            bulletPool = new GenericMonoPool<Bullet>(bulletPrefab);
        }

        public override ItemType GetItemType()
        {
            return itemType;
        }

        public override int GetSubType()
        {
            return (int)(weaponType);
        }

        public override void pickUp(Transform container,Action onThrowDone)
        {
            base.pickUp(container,onThrowDone);
            Shooting=StartCoroutine(EnableShoot());
        }
        public override void ThrowAway()
        {
            base.ThrowAway();
            if (Shooting != null)
            {
                StopCoroutine(Shooting);
                Shooting = null;
            }
        }


        private IEnumerator EnableShoot()
        {
            while (true)
            {
                var triggerPressed = Input.GetMouseButton(0);
                if(triggerPressed && lastFireTime<Time.time)
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
}