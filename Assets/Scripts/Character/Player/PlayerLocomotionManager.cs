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
    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        //grounded
        //Aerial 
    }

    private void HandleGroundedMovement()
    {
        //haraket yönümüz kamera perspektifi ve inputlarýmýza göre belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.forward * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vektörün büyüklüðü  1den büyük oluyor buda oyunda çapraz gidildiðinnde daha hýzlý olmasýna neden oluyordu ancak normalize bu vektörün büyüklüðünü 1 yapýyor.
        movementDir.y = 0;
    }
}
