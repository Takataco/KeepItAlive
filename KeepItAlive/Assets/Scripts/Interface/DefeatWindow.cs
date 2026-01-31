using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatWindow : Window
{
    //---- Attributes ----
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMainMenuButton;

    //---- Functions ----
    public override void Initialize()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClicked); 
    }

    private void OnReturnToMainMenuButtonClicked()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    private void OnRestartButtonClicked()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
        GameManager.Instance.StartGame();
    }
    
}
