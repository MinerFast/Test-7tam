using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;

    [Space]
    [SerializeField] private float speed;

    [Space]
    [SerializeField] private Transform[] moveSpots;

    private BoxCollider2D pigCollider;

    private SpriteRenderer pigSpriteRenderer;

    private int countSpot = 0;

    private bool isForwardMove = true;

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
        pigSpriteRenderer = GetComponent<SpriteRenderer>();
        pigCollider = GetComponent<BoxCollider2D>();
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
    #endregion

    void ChangeSprite(float posX, float posY)
    {
        if (posX > transform.position.x)
        {
            pigSpriteRenderer.sprite = spriteLeft;
        }
        if (posX < transform.position.x)
        {
            pigSpriteRenderer.sprite = spriteRight;
        }
        if (posY > transform.position.y)
        {
            pigSpriteRenderer.sprite = spriteDown;
        }
        if (posY < transform.position.y)
        {
            pigSpriteRenderer.sprite = spriteUp;
        }

    }
    void ChangeColliderSize()
    {
        pigCollider.size = pigSpriteRenderer.sprite.bounds.size / 1.4f;
    }
}
