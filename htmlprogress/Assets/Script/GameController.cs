using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;

    Rigidbody2D PlayerRb;

    CameraController cameraController;
    public ParticleController particleController;
    private void Awake()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        PlayerRb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        cameraController.playcameraanimation("WhiteScreen");
        particleController.PlayDieParticle(transform.position);
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        PlayerRb.simulated = false;
        PlayerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        transform.localScale = new Vector3(1, 1, 1);
        PlayerRb.simulated = true;
    }
}
