using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;// sahneleri yönetmek için gerekli.
public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance; //singleton setup

    PlayerControls playerControls; //new input sisteminden class oluþturma
                                   //iþlemi yaptýktan sonra classa eriþebildik
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
        DontDestroyOnLoad(gameObject);//sahne geçileri arasýnda varlýðýný korusun. bu kodun yeri önemli alttaki kodla yer deðiþtirseydi scripti önce deaktif etceðimizden yok olurdu.
       
        SceneManager.activeSceneChanged += OnSceneChange; //basitçe sahne geçiþlerinde bu mantýðý çalýþmasýný saðlýyan bir evente subscribe oluyoruz
        instance.enabled = false; //sadece world scene gidildiðinde kontrollerin aktif olmasýný istiyoruz.
    }
    private void OnSceneChange(Scene _oldScene, Scene _newScene)//args0 ve args1 bizim önceki ve sonraki sahnemiz
    {
        if(_newScene.buildIndex == WorldSaveManager.instance.GetWorldSceneIndex())//eðer world sahnesine gidiyorsak input scriptini enable et.
        {
            instance.enabled = true;//burada gameObj den bahsetmiyoruz scriptten bahsediyoruz.
        }
        else//menüye gidiyorsak player controlü deaktif et.
        {
            instance.enabled = false;
        }
    }

    private void OnEnable() // SetActive olduðu zaman çalýþýr.
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

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;//bu game object yokedildiðinde eventimizden unSubscribe olmasý için.
        //bu memory leaks olmamasý için 
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
