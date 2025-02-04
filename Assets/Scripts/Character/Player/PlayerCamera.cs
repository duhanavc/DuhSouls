using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Camera cameraObject;
    public PlayerManager player;


    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1;
    [SerializeField] float rightAndLeftRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minPivot = -30;
    [SerializeField] float maxPivot = 60;
    [SerializeField] float cameraColliderRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    private float rightAndLeftLookAngle;
    private float upAndDownLookAngle;
    private float cameraZPosition;
    private float targetCameraZPosition;

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // menüler arasý varlýðýný koruyabilsin diye
        cameraZPosition = cameraObject.transform.localPosition.z;

    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowPlayer();
            HandleRotations();
            HandleCameraCollisions();
        }   
    }

    private void HandleFollowPlayer()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity ,cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;

    }
    
    private void HandleRotations()
    {

        //ilerdie eðer kamera kitliyse, targete bakmasýný zorlamalýyýz
        //kitli deðilse normal þekilde rotatele

        rightAndLeftLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * rightAndLeftRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle += (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle,minPivot,maxPivot);

        Vector3 cameraRotations;
        Quaternion targetRotation;

        //rotate this object in right and left axis.
        cameraRotations = Vector3.zero;
        cameraRotations.y = rightAndLeftLookAngle;
        targetRotation = Quaternion.Euler(cameraRotations);
        transform.rotation = targetRotation;    


        //rotate pivot obj up and down axis.
        cameraRotations = Vector3.zero;
        cameraRotations.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(-cameraRotations);
        cameraPivotTransform.localRotation = targetRotation;

    }
    

    private void HandleCameraCollisions()
    {
        //bir spherecast gönderilecek çarpýþma olursa hit pointle oyuncu arasýndaki mesafeyi hesaplaycak ve bu mesafeden sphere castýmýzýn radiusunu çýkarýcak bu bizim targettimiz olacak
        targetCameraZPosition = cameraZPosition;

        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position,cameraColliderRadius,direction,out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))//bir çarpýþma gerçekleþirse
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position,hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraColliderRadius);//cameramýz parentine göre -3 z de olacak þekilde olduðu için negatif düþünecez 

            if(Mathf.Abs(targetCameraZPosition) < cameraColliderRadius)
            {
                targetCameraZPosition = -cameraColliderRadius;
            }

        }
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
