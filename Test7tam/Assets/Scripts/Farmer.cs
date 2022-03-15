using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [Header("Sprites Normal")]
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;

    [Header("Sprites Dirt")]
    [SerializeField] private Sprite spriteDirtLeft;
    [SerializeField] private Sprite spriteDirtRight;
    [SerializeField] private Sprite spriteDirtUp;
    [SerializeField] private Sprite spriteDirtDown;

    [Space]
    [SerializeField] private float speed;

    [Space]
    [SerializeField] private Transform[] moveSpots;

    private BoxCollider2D farmerCollider;

    private SpriteRenderer farmerSpriteRenderer;

    private int countSpot = 0;

    private bool isForwardMove = true;
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
        farmerSpriteRenderer = GetComponent<SpriteRenderer>();
        farmerCollider = GetComponent<BoxCollider2D>();
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
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (!isDirt)
            {
                farmerSpriteRenderer.sprite = spriteDirtUp;
                Destroy(collision.gameObject);
                isDirt = true;
                speed = 0f;
                GameController.score++;

            }
        }
    }
    #endregion

    void ChangeSprite(float posX, float posY)
    {
        if (isDirt)
        {
            if (posX > transform.position.x)
            {
                farmerSpriteRenderer.sprite = spriteDirtLeft;
            }
            if (posX < transform.position.x)
            {
                farmerSpriteRenderer.sprite = spriteDirtRight;
            }
            if (posY > transform.position.y)
            {
                farmerSpriteRenderer.sprite = spriteDirtDown;
            }
            if (posY < transform.position.y)
            {
                farmerSpriteRenderer.sprite = spriteDirtUp;
            }
        }
        else
        {

            if (posX > transform.position.x)
            {
                farmerSpriteRenderer.sprite = spriteLeft;
            }
            if (posX < transform.position.x)
            {
                farmerSpriteRenderer.sprite = spriteRight;
            }
            if (posY > transform.position.y)
            {
                farmerSpriteRenderer.sprite = spriteDown;
            }
            if (posY < transform.position.y)
            {
                farmerSpriteRenderer.sprite = spriteUp;
            }
        }

    }
    void ChangeColliderSize()
    {
        farmerCollider.size = farmerSpriteRenderer.sprite.bounds.size / 2f;
    }
   

}
