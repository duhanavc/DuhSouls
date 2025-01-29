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
        //haraket y�n�m�z kamera perspektifi ve inputlar�m�za g�re belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.forward * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vekt�r�n b�y�kl���  1den b�y�k oluyor buda oyunda �apraz gidildi�innde daha h�zl� olmas�na neden oluyordu ancak normalize bu vekt�r�n b�y�kl���n� 1 yap�yor.
        movementDir.y = 0;
    }
}
