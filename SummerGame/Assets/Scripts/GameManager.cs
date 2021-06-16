using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public float playerHealth;
    public float playerMana;
    private float currentPlayerHealth;
    private float currentPlayerMana;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentPlayerHealth = playerHealth;
        currentPlayerMana = playerMana;
    }

    void Update()
    {
        
    }
    public void GiveStats()
    {
        Debug.Log("Player Health: " + currentPlayerHealth);
        Debug.Log("Player Mana: " + currentPlayerMana);
    }
    public bool enoughMana(float cost)
    {
        if(currentPlayerMana >= cost)
        {
            return true;
        }
        Debug.Log("You have no mana");
        return false;
    }
    public void decreaseMana(float manaCost)
    {
        currentPlayerMana -= manaCost;
    }
    public void decreaseHealth(float damage)
    {
        currentPlayerHealth -= damage;
    }
    //SCENE MANAGEMENT
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void transitionScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
