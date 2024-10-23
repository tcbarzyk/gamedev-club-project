using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private bool flashOnHit;
    [SerializeField] private float flashTime = 0.2f;
    [SerializeField] Color flashcolor = Color.white;
    private int health;

    [Header("Invincibility")]
    [SerializeField] private bool hasIFrames = false;
    public bool invincible = false;
    [SerializeField] private int numberOfFlashes = 5;
    [SerializeField] private float invincibilityTime = 1.5f;

    private SpriteRenderer[] entitySprites;
    private Material[] entityMaterials;

    // Start is called before the first frame update
    void Start()
    {
        entitySprites = GetComponentsInChildren<SpriteRenderer>();

        initMaterials();
    }

    private void initMaterials()
    {
        entityMaterials = new Material[entitySprites.Length];

        for (int i = 0; i < entitySprites.Length; i++)
        {
            entityMaterials[i] = entitySprites[i].material;
        }
    }
    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeHit(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            if (hasIFrames)
            {
                StartCoroutine("FlashInvincible");
            }
            else if (flashOnHit)
            {
                StartCoroutine("DamageFlash");
            }
        }
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        if (gameObject.CompareTag("Player"))
        {
            GetComponent<PlayerCombat>().die();
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<Enemy>().die();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashInvincible()
    {
        setFlashColor();

        float waitTime = (invincibilityTime / numberOfFlashes) / 2f;
        invincible = true;
        for (int temp = 0; temp < numberOfFlashes; temp++)
        {
            setFlashAmount(0.5f);
            yield return new WaitForSeconds(waitTime);
            setFlashAmount(0f);
            yield return new WaitForSeconds(waitTime);
        }
        invincible = false;
    }

    private IEnumerator DamageFlash()
    {
        setFlashColor();

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            setFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void setFlashColor()
    {
        for (int i = 0; i < entityMaterials.Length; i++)
        {
            entityMaterials[i].SetColor("_FlashColor", flashcolor);
        }
    }

    private void setFlashAmount(float amount)
    {
        for (int i =0; i < entityMaterials.Length;i++)
        {
            entityMaterials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
