using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPageEventManager : MonoBehaviour
{
    [SerializeField] Button _testGameButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _exitButton;

    void Awake()
    {

        if (_testGameButton != null)
        {
            _testGameButton.onClick.AddListener(OnTestGameButtonClicked);
        }
        if (_optionButton != null)
        {
            _testGameButton.onClick.AddListener(OnOptionButtonClicked);
        }
        if (_exitButton != null)
        {
            _testGameButton.onClick.AddListener(OnExitButtonClicked);
        }
    }

    void Update()
    {
        
    }

    private void OnTestGameButtonClicked()
    {
        Debug.Log("Start Test");
        // Load the main scene
        LoadManager.Instance.SwitchScene("ReadyScene");
    }
    private void OnOptionButtonClicked()
    {
        
    }
    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
