using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
   CharacterManager character;

    protected void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    private void UpdateAnimatorMovementParameters(float _horizontal, float _vertical)
    {

        character.animator.SetFloat("Horizontal", _horizontal);
        character.animator.SetFloat("Vertical", _vertical);
        
    }
}
