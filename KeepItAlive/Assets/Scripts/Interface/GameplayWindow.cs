using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    //---- Attributes ----
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;

    [Space][SerializeField] private Slider experienceSlider;

    [Space][SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Button pauseButton;

    //---- Functions ----
    public override void Initialize()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }
    private void OnPauseButtonClicked()
    {
        GameManager.Instance.IsGamePaused = true;
        GameManager.Instance.WindowsService.ShowWindow<PauseWindow>(false);
    }


    protected override void OpenStart()
    {
        base.OpenStart();
        var player = GameManager.Instance.CharacterFactory.Player;
       
        UpdateHealthVisual(player);
        player.LiveComponent.OnCharacterHealthChange += UpdateHealthVisual;

        ScoreChangeHandler(GameManager.Instance.ScoreManager.GameScore);
        GameManager.Instance.ScoreManager.OnScoreChanged += ScoreChangeHandler;

        //UpdateExperience(GameManager.Instance.SessionExperienceManager.Experience,
        //GameManager.Instance.SessionExperienceManager.ExperienceMax);

        //GameManager.Instance.SessionExperienceManager.OnExperienceUp += UpdateExperience;


        UpdateTimer();
    }

    protected override void CloseStart()
    {
        base.CloseStart();

        //GameManager.Instance.SessionExperienceManager.OnExperienceUp -= UpdateExperience;
        GameManager.Instance.ScoreManager.OnScoreChanged -= ScoreChangeHandler;

        var player = GameManager.Instance.CharacterFactory.Player;
        if (player == null)
        {
            return;
        }
        player.LiveComponent.OnCharacterHealthChange -= UpdateHealthVisual;
    }

    private void UpdateHealthVisual(Character character)
    {
        int health = (int)character.LiveComponent.Health;
        int healthMax = character.LiveComponent.MaxHealth;

        healthText.text = health + "/" + healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = health; 
    }
    private void ScoreChangeHandler(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateTimer()
    {
        var min = (int)(GameManager.Instance.GameSessionTime / 60);
        var sec = (int)(GameManager.Instance.GameSessionTime % 60);
        timerText.text = GetTime(min) + ":" + GetTime(sec);


        string GetTime(int value)
        {
            return (value < 10) ? "0" + value : value.ToString();
        }
    }

    private void UpdateExperience(int value, int maxValue)
    {
        experienceSlider.maxValue = maxValue;
        experienceSlider.value = value;
    }

    private void Update()
    {
        UpdateTimer();
    }
}
