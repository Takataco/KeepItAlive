using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;
    [SerializeField] private Button aboutGameButton;

    public override void Initialize()
    {
        startGameButton.onClick.AddListener(StartGameHandler);
        optionsGameButton.onClick.AddListener(OpenOptionsHandler);
        aboutGameButton.onClick.AddListener(AboutHandler);
    }

    protected override void OpenEnd()
    {
        base.OpenEnd();
        startGameButton.interactable = true; 
        optionsGameButton.interactable = true;
    }

    protected override void CloseStart()
    {
        base.CloseStart();
        startGameButton.interactable = false;
        optionsGameButton.interactable = false;
    }

    private void StartGameHandler()
    {
        Hide(true);
        GameManager.Instance.StartGame();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(true);
    }

    private void OpenOptionsHandler()
    {
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(false);
    }

    private void AboutHandler()
    {
        //nothing for now
    }
}
