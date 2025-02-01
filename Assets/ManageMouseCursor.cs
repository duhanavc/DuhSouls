using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMouseCursor : MonoBehaviour
{

    private static ManageMouseCursor instance;
    private bool isCursorVisible = true; // Baþlangýçta fare gizli olacak

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahneler arasý korunmasýný saðlar
        }
        else
        {
            Destroy(gameObject); // Eðer bir kopya varsa, yok et
            return;
        }
    }

    void Start()
    {
        UpdateCursorState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // H tuþuna basýnca aç/kapat
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
