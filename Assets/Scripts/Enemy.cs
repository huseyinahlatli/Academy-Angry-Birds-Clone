using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 4f;
    [SerializeField] private GameObject deathEffect;

    public static int EnemiesAlive = 0;

    private void Start()
    {
        EnemiesAlive += 1; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > health)
            Die();            
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
        EnemiesAlive -= 1;
        if (EnemiesAlive <= 0)
            StartCoroutine(LevelWin());
    }

    private IEnumerator LevelWin()
    {
        UIManager.Instance.gameWinText.text = "You Win!";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
