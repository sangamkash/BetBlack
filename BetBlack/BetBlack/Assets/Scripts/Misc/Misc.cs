using BulletEcho.DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.Items
{

    /// <summary>
    /// Items which be picked and thrown
    /// </summary>
    public interface ICarryable
    {
        void pickUp(Transform container,Action onThrowDone);
        void ThrowAway();
    }

    /// <summary>
    /// Items which can be stored in bag
    /// </summary>
    public interface IStorable 
    {
        void Store();
    }

    /// <summary>
    /// Items which can be consumed to boost health
    /// </summary>
    public interface IConsumable
    {
        void Consume(Transform consumer);
    }

    public interface IWearable
    {
        void Wear();
    }
    
}
