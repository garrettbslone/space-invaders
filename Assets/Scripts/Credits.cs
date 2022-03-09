using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private GameObject gameManager;
    private Button _button;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManagerObj");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 5f)
        {
            gameManager.GetComponent<GameManager>().LoadScene("MainMenuScene");
        }
    }
}
