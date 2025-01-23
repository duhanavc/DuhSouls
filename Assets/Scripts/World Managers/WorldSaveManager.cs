using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldSaveManager : MonoBehaviour
{
    public static WorldSaveManager instance = null;//singleton yapýsý

    [SerializeField] int worldSceneIndex = 1;
    private void Awake()
    {

        if (instance == null)//bu bu scriptin instance 'ýný ayný anda kullanan sadece 1 tane obje olabilir.
        {
            instance = this;
        }
        else//eðer hali hazýrda varsa yok et.
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject); //bu instanceyi scene deðiþtirsekte yok olmamasý için.
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        yield return null;
    }
    
}
