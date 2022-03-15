using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [Header("Sprites Normal")]
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;

    [Header("Sprites Agry")]
    [SerializeField] private Sprite spriteAgryLeft;
    [SerializeField] private Sprite spriteAgryRight;
    [SerializeField] private Sprite spriteAgryUp;
    [SerializeField] private Sprite spriteAgryDown;

    [Header("Sprites Dirt")]
    [SerializeField] private Sprite spriteDirtLeft;
    [SerializeField] private Sprite spriteDirtRight;
    [SerializeField] private Sprite spriteDirtUp;
    [SerializeField] private Sprite spriteDirtDown;

    [Space]
    [SerializeField] private float speed;

    [Space]
    [SerializeField] private Transform[] moveSpots;

    private BoxCollider2D dogCollider;

    private SpriteRenderer dogSpriteRenderer;

    private int countSpot = 0;

    private bool isForwardMove = true;
    private bool isAgry = false;
    private bool isDirt = false;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (speed <= 0)
        {
            speed = 1;
        }
    }
    private void Start()
    {
        dogSpriteRenderer = GetComponent<SpriteRenderer>();
        dogCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[countSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[countSpot].position) < 0.01f)
        {
            if (isForwardMove)
            {
                countSpot++;
                if (countSpot == moveSpots.Length)
                {
                    countSpot = moveSpots.Length - 1;
                    isForwardMove = false;
                }
            }
            else
            {
                countSpot--;
                if (countSpot == 0)
                {
                    countSpot = 0;
                    isForwardMove = true;
                }
            }
        }
        ChangeSprite(posX, posY);
        ChangeColliderSize();



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isDirt && !isAgry)
            {

                isAgry = true;
                StartCoroutine(ChangeCondition(speed));
                speed *= 2;
            }
        }
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (!isDirt)
            {
                Destroy(collision.gameObject);
                isAgry = false;
                isDirt = true;
                speed = 1.8f;
                GameController.score++;
            }
        }
    }
    #endregion
    void ChangeSprite(float posX, float posY)
    {
        if (isAgry)
        {
            if (posX > transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteAgryLeft;
            }
            if (posX < transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteAgryRight;
            }
            if (posY > transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteAgryDown;
            }
            if (posY < transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteAgryUp;
            }
        }
        else if (isDirt)
        {
            if (posX > transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteDirtLeft;
            }
            if (posX < transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteDirtRight;
            }
            if (posY > transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteDirtDown;
            }
            if (posY < transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteDirtUp;
            }
        }
        else
        {

            if (posX > transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteLeft;
            }
            if (posX < transform.position.x)
            {
                dogSpriteRenderer.sprite = spriteRight;
            }
            if (posY > transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteDown;
            }
            if (posY < transform.position.y)
            {
                dogSpriteRenderer.sprite = spriteUp;
            }
        }

    }
    void ChangeColliderSize()
    {
        dogCollider.size = dogSpriteRenderer.sprite.bounds.size / 2f;
    }
    IEnumerator ChangeCondition(float normalSpeed)
    {
        yield return new WaitForSeconds(3);
        if (!isDirt)
        {
            speed = normalSpeed;
            isAgry = false;
        }
    }
}
