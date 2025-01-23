using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldSaveManager : MonoBehaviour
{
    public static WorldSaveManager instance = null;//singleton yap�s�

    [SerializeField] int worldSceneIndex = 1;
    private void Awake()
    {

        if (instance == null)//bu bu scriptin instance '�n� ayn� anda kullanan sadece 1 tane obje olabilir.
        {
            instance = this;
        }
        else//e�er hali haz�rda varsa yok et.
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject); //bu instanceyi scene de�i�tirsekte yok olmamas� i�in.
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        yield return null;
    }
    
}
