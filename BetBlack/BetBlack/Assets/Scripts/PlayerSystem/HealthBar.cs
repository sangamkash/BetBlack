using System;
using UnityEngine;
using UnityEngine.Events;

namespace BulletEcho
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private int health=100;
        [SerializeField] private UnityEvent<int> onHealthChange;
        [SerializeField] private UnityEvent onDie;
        
        public void ApplyDamage(int damageValue)
        {
            health -= damageValue;
            onHealthChange?.Invoke(health);
            if (health <= 0)
            {
                onDie?.Invoke();
            }
        }

        public void RepairDamage(int healValue)
        {
            health = Mathf.Clamp(health + healValue, 0, 100);
            onHealthChange?.Invoke(health);
            if (health <= 0)
            {
                onDie?.Invoke();
            }
        }
    }
}
