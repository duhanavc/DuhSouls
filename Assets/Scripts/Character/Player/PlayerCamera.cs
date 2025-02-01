using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Camera cameraObject;

    public PlayerManager player;

    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1;
    [SerializeField] float rightAndLeftRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minPivot = -30;
    [SerializeField] float maxPivot = 60;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private float rightAndLeftLookAngle;
    private float upAndDownLookAngle;
    

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


    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowPlayer();
            //collide with walls and objs
            //rotate around player
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

        //rightAndLeftLookAngle += PlayerInputManager.instance.cameraHorizontalInput;


    }




}
