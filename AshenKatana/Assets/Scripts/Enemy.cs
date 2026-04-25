using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour {
    public int maxHealth = 5;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    private CinemachineImpulseSource impulseSource;




    void Start() {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        impulseSource = GetComponent<CinemachineImpulseSource>();   
    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(int damage) {

        CameraShakeManager.instance.CameraShake(impulseSource);
        
        currentHealth -= damage;
        StartCoroutine(FlashRed());

        Debug.Log("Enemy HP: " + currentHealth);

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        IEnumerator FlashRed() {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }
}