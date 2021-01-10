using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health = 1000;

    public bool isInvulnerable = false;

    public Slider healthBar;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }

    // Method for when Boss takes damage
    public void TakeDamage(int dmg)
    {
        if (isInvulnerable)
        {
            return;
        }

        health -= dmg;

        // Boss enrage phase starts when life is under 500
        if (health <= 500)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        StartCoroutine(JumpToScene());
    }

    private IEnumerator JumpToScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelScape");
    }
}
