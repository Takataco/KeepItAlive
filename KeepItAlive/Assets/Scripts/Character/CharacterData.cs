using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    // ---- Attributes ----
    [SerializeField] private float speed;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    // ---- Properties ----
    public float DefaultSpeed => speed;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController { 
        get 
        {
            return characterController; 
        } 
    }
}
