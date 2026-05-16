using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefeatWindow : Window
{
    //---- Attributes ----
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMainMenuButton;
    [SerializeField] private TMP_Text recordText;

    //---- Functions ----
    public override void Initialize()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClicked); 
    }

    private void OnReturnToMainMenuButtonClicked()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(true);
    }

    private void OnRestartButtonClicked()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
        GameManager.Instance.StartGame();
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        recordText.text = GameManager.Instance.ScoreManager.GameScore.ToString();
    }

}
