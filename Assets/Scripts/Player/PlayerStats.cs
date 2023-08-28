using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int reqExp;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    private int currentExp;

    public HealthBar healthBar;
    public ExpBar expBar;

    private int levelConst = 300;
    private float levelBase = 2f;
    private float levelExponent = 7f;

    void Start()
    {
        currentHealth = maxHealth;
        reqExp = SolveReqExp(level);
        currentExp = 0;
        healthBar.SetSliderMax(maxHealth);
        expBar.SetSliderMax(reqExp);
        expBar.SetSlider(currentExp);
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }

        public void Heal(int amount) {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    public void GainExp(int amount) {
        currentExp += amount;
        expBar.SetSlider(currentExp);
    }

    public void LevelUp() {
        level += 1;
        currentExp -= reqExp;
        expBar.SetSlider(currentExp);
        reqExp = SolveReqExp(level);
    }

    private int SolveReqExp(int level) {
        int reqExp = (int)Mathf.Floor(level + levelConst * Mathf.Pow(levelBase, level/levelExponent));
        return reqExp/4;
    }

    void Update()
    {
        if (currentHealth <= 0) {
            GameOver();
        }

        if (currentExp > reqExp) {
            LevelUp();
        }
    }

    public void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}