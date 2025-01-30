using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    public float moveAmount;
    public float verticalMovement;
    public float horizontalMovement;

    private Vector3 movementDir;
    private Vector3 targetRotationDir;

    [SerializeField] float runningSpeed = 10;
    [SerializeField] float walkingSpeed = 7;

    [SerializeField] float rotationSpeed = 5;
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
        HandleRotationMovement();
        //Aerial 
    }

    private void HandleGroundedMovement()
    {
        GetVerticalAndHorizontalInput();

        //haraket y�n�m�z kamera perspektifi ve inputlar�m�za g�re belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.right * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vekt�r�n b�y�kl��� 1den b�y�k oluyor buda oyunda �apraz gidildi�innde daha h�zl� olmas�na neden oluyordu ancak normalize bu vekt�r�n b�y�kl���n� 1 yap�yor.
        movementDir.y = 0;
        
        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            player.characterController.Move(runningSpeed * Time.deltaTime * movementDir);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(walkingSpeed * Time.deltaTime * movementDir); //�ok ufak bir optimzasyon olsa da vector3*float yapmak float* float yapmaktan daha maliyetli
                                                                                          //oy�zden float ile floati �nce carp�p en son vector3 ile �arpmak daha mant�kl�.
        }
    }

    private void HandleRotationMovement()
    {
        targetRotationDir = Vector3.zero;

        targetRotationDir = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDir = targetRotationDir + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDir.Normalize();
        targetRotationDir.y = 0;

        if(targetRotationDir == Vector3.zero) 
        {
            targetRotationDir = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDir); // bakt���m y�n�n rotasyonunu
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime); //yeni rotasyonum yap�p bunu slerplenmi� halini kullan�yoruz.
        transform.rotation = targetRotation;
    }
}
