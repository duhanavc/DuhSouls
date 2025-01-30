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
        //daha sonra clamplayacaðýz
    }
    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        //Aerial 
    }

    private void HandleGroundedMovement()
    {
        //haraket yönümüz kamera perspektifi ve inputlarýmýza göre belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.forward * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vektörün büyüklüðü 1den büyük oluyor buda oyunda çapraz gidildiðinnde daha hýzlý olmasýna neden oluyordu ancak normalize bu vektörün büyüklüðünü 1 yapýyor.
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
