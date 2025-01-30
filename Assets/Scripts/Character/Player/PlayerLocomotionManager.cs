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
        //daha sonra clamplayacaðýz
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

        //haraket yönümüz kamera perspektifi ve inputlarýmýza göre belirleniyor.
        movementDir = PlayerCamera.instance.transform.forward * verticalMovement;
        movementDir = movementDir + PlayerCamera.instance.transform.right * horizontalMovement;
        movementDir.Normalize(); // iki axiste de 1 ken vektörün büyüklüðü 1den büyük oluyor buda oyunda çapraz gidildiðinnde daha hýzlý olmasýna neden oluyordu ancak normalize bu vektörün büyüklüðünü 1 yapýyor.
        movementDir.y = 0;
        
        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            player.characterController.Move(runningSpeed * Time.deltaTime * movementDir);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(walkingSpeed * Time.deltaTime * movementDir); //çok ufak bir optimzasyon olsa da vector3*float yapmak float* float yapmaktan daha maliyetli
                                                                                          //oyüzden float ile floati önce carpýp en son vector3 ile çarpmak daha mantýklý.
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

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDir); // baktýðým yönün rotasyonunu
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime); //yeni rotasyonum yapýp bunu slerplenmiþ halini kullanýyoruz.
        transform.rotation = targetRotation;
    }
}
