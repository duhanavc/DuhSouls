using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance; //singleton setup

    PlayerControls playerControls; //new input sisteminden class olu�turma
                                   //i�lemi yapt�ktan sonra classa eri�ebildik
    [SerializeField] Vector2 movementInput; //inputu data


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); 
            //new input sisteminde �nceden kurdu�umuz pmovement movemente eri�ip e�er bir input girdisi olursaa i olarak al�p 
            //vector2 olan movementInput de�erini bu de�er yap�yoruz ve bu sayede yeni input giri�i oldu�unda s�rekli g�ncelleniyor.

        }
        playerControls.Enable();
    }








}
