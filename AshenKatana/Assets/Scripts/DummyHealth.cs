using UnityEngine;
using System.Collections;

public class DummyHealth : MonoBehaviour {
    public int maxHealth = 5;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;

    void Start() {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        StartCoroutine(FlashRed());

        Debug.Log("Dummy HP: " + currentHealth);

        if (currentHealth <= 0) {
            StartCoroutine(ResetDummy());
        }
    }

    IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator ResetDummy() {
        yield return new WaitForSeconds(1f);
        currentHealth = maxHealth;
        Debug.Log("Dummy resetou!");
    }
}
