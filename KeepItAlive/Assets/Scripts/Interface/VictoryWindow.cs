using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : Window
{
    //---- Attributes ----
    [Space][SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private TMP_Text newRecordText;

    //---- Functions ----
    public override void Initialize()
    {
        continueButton.onClick.AddListener(ContinueButtonClickHandler);
    }

    private void ContinueButtonClickHandler()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        recordText.text = GameManager.Instance.ScoreManager.GameScore.ToString();
        newRecordText.gameObject.SetActive(GameManager.Instance.ScoreManager.IsNewScoreRecord);
    }
}
