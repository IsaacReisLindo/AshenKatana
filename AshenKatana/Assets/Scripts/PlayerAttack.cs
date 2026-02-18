using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;

    public void ActivateHitbox() {
        attackHitbox.SetActive(true);
        Invoke("DeactivateHitbox", attackDuration);
    }

    void DeactivateHitbox() {
        attackHitbox.SetActive(false);
    }

}
