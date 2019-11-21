using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableCollector : MonoBehaviour
{
    Animator animator;

    int coinCount = 0;
    int keyCount = 0;

    bool hasKey;

    //TODO Refactor
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject, 3f);
            coinCount++;
        }

        else if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject, 3f);
            keyCount++;
        }

        else if (collision.gameObject.CompareTag("Chest") && keyCount >= 1)
        {
            hasKey = true;
            collision.gameObject.GetComponent<AudioSource>().Play();
            animator = collision.GetComponent<Animator>();
            animator.SetBool("hasKey", hasKey);
            keyCount--;


            Invoke("ReloadLevel", 3f);
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public int GetKeyCount()
    {
        return keyCount;
    }
}
