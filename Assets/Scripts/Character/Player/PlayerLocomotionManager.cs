using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    public float moveAmount;
    public float verticalMovement;
    public float horizontalMovement;

    private Vector3 movementDir;

    [SerializeField] float runningSpeed;
    [SerializeField] float walkingSpeed;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    private void GetVerticalAndHorizontalInput()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        //daha sonra clamplayaca��z
    }
    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        //Aerial 
    }

    private void HandleGroundedMovement()
    {
        //haraket y�n�m�z kamera perspektifi ve inputlar�m�za g�re belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.forward * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vekt�r�n b�y�kl��� 1den b�y�k oluyor buda oyunda �apraz gidildi�innde daha h�zl� olmas�na neden oluyordu ancak normalize bu vekt�r�n b�y�kl���n� 1 yap�yor.
        movementDir.y = 0;
        
        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            player.characterController.Move(movementDir * runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(movementDir * walkingSpeed * Time.deltaTime);

        }
    }
}
