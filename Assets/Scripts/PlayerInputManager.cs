using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance; //singleton setup

    PlayerControls playerControls; //new input sisteminden class oluþturma
                                   //iþlemi yaptýktan sonra classa eriþebildik
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
            //new input sisteminde önceden kurduðumuz pmovement movemente eriþip eðer bir input girdisi olursaa i olarak alýp 
            //vector2 olan movementInput deðerini bu deðer yapýyoruz ve bu sayede yeni input giriþi olduðunda sürekli güncelleniyor.

        }
        playerControls.Enable();
    }








}
