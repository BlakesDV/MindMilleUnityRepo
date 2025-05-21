using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    #region Parameters

    public float maxHealth = 100f;
    public float currentHealth;
    public float damageRate = 10f;
    public float detectionRadius = 3f;

    #endregion

    public Slider healthSlider;
    public bool isInsane = false;
    public GameObject losePanel;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (isInsane) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);

        bool nearEnemy = false;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Ghost"))
            {
                nearEnemy = true;
                break;
            }
        }

        if (nearEnemy)
        {
            currentHealth -= damageRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
            if (currentHealth <= 0)
            {
                LoseGame();
            }
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
    void LoseGame()
    {
        isInsane = true;
        Time.timeScale = 0f;

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        Debug.Log("YOU DIED");
    }
}
