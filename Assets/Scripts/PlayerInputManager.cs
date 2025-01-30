using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;// sahneleri y�netmek i�in gerekli.
public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance; //singleton setup

    PlayerControls playerControls; //new input sisteminden class olu�turma
                                   //i�lemi yapt�ktan sonra classa eri�ebildik
    [SerializeField] Vector2 movementInput; //input data

    public float verticalInput;
    public float horizontalInput;

    public float moveAmount;
    
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
        DontDestroyOnLoad(gameObject);//sahne ge�ileri aras�nda varl���n� korusun. bu kodun yeri �nemli alttaki kodla yer de�i�tirseydi scripti �nce deaktif etce�imizden yok olurdu.
       
        SceneManager.activeSceneChanged += OnSceneChange; //basit�e sahne ge�i�lerinde bu mant��� �al��mas�n� sa�l�yan bir evente subscribe oluyoruz
        instance.enabled = false; //sadece world scene gidildi�inde kontrollerin aktif olmas�n� istiyoruz.
    }
    private void OnSceneChange(Scene _oldScene, Scene _newScene)//args0 ve args1 bizim �nceki ve sonraki sahnemiz
    {
        if(_newScene.buildIndex == WorldSaveManager.instance.GetWorldSceneIndex())//e�er world sahnesine gidiyorsak input scriptini enable et.
        {
            instance.enabled = true;//burada gameObj den bahsetmiyoruz scriptten bahsediyoruz.
        }
        else//men�ye gidiyorsak player control� deaktif et.
        {
            instance.enabled = false;
        }
    }

    private void OnEnable() // SetActive oldu�u zaman �al���r.
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

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;//bu game object yokedildi�inde eventimizden unSubscribe olmas� i�in.
        //bu memory leaks olmamas� i�in 
    }

    private void Update()
    {
        HandleMovementInput();    
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput)+Mathf.Abs(horizontalInput));

        //clamping inputs for more souls like feel.
        if(moveAmount > 0 && moveAmount <= 0.5f)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5f && moveAmount <= 1)
        {

            moveAmount = 1;
        }

    }
}
