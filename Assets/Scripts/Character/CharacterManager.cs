using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        

    }

    protected virtual void LateUpdate () { 
    
        
    }
}
