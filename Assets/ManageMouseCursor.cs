using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMouseCursor : MonoBehaviour
{

    private static ManageMouseCursor instance;
    private bool isCursorVisible = true; // Ba�lang��ta fare gizli olacak

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahneler aras� korunmas�n� sa�lar
        }
        else
        {
            Destroy(gameObject); // E�er bir kopya varsa, yok et
            return;
        }
    }

    void Start()
    {
        UpdateCursorState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // H tu�una bas�nca a�/kapat
        {
            isCursorVisible = !isCursorVisible;
            UpdateCursorState();
        }
    }

    void UpdateCursorState()
    {
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
