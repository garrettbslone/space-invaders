using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject gameManager;
    private Button _button;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManagerObj");
        _button = GameObject.Find("Start Game Button").GetComponent<Button>();
        _button.onClick.AddListener(call: () =>
        {
            gameManager.GetComponent<GameManager>().LoadScene("MainScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
