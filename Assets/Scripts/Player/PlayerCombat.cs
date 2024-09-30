using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int health;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        print("Player hit!");
    }
}
