using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.Items.WeaponeSystem
{
    public class Bullet : MonoBehaviour, IPoolObj
    {
        private int damage = 5;
        public Action onReset { get; set; }

        public void Init(Vector3 dir,Vector3 startingPoint,float speed,float life,int damage,Action<Transform> hitObj=null)
        {
            this.damage = damage;
            transform.position = startingPoint;
            StartCoroutine(Die(life,dir,speed));
        }

        private IEnumerator Die(float life,Vector3 dir,float speed,Action<Transform> hitObj=null)
        {
            var startTime = Time.time;
            while (Time.time-startTime<life)
            {
                var ray = new Ray(transform.position, dir);
                var hitinfo = new RaycastHit();
                if (Physics.Raycast(ray,out hitinfo, Time.deltaTime * speed))
                {
                    life = 0;
                    var hitTrans = hitinfo.transform;
                    hitObj?.Invoke(hitTrans);
                    ApplyDamage(hitTrans.GetComponent<HealthBar>());
                }
                else
                {
                    var dis = Time.deltaTime * speed;
                    transform.position += dir * dis;
                    yield return new WaitForEndOfFrame();
                }
            }
            onReset?.Invoke();
        }

        private void ApplyDamage(HealthBar healthBar)
        {
            if (healthBar != null)
            {
                healthBar.ApplyDamage(damage);
            }
        }
    }
}
