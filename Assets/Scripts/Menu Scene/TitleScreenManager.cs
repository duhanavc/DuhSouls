using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void StartNewGame()
    {
     
        StartCoroutine(WorldSaveManager.instance.LoadNewGame());

    }
}
