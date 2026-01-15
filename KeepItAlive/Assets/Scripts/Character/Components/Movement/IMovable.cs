using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    //----Properties----
    public float Speed { get; set; }

    //----Functions----
    public void Initialize(Character character);
    public void Move(Vector3 direction);
    public void Rotation(Vector3 direction);
}
