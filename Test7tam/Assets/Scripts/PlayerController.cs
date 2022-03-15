using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite spritePlayerLeft;
    [SerializeField] private Sprite spritePlayerRight;
    [SerializeField] private Sprite spritePlayerDown;
    [SerializeField] private Sprite spritePlayerUp;

    [Header("Variables")]
    [SerializeField] private float speed;

    [Space]
    [SerializeField] private GameObject bomb;

    public static int countBomb;

    private bool isLeft;
    private bool isRight;
    private bool isDown;
    private bool isUp;

    private BoxCollider2D playerCollider;

    private GameController gameController;

    #region MonoBehaviour

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerCollider = GetComponent<BoxCollider2D>();
        countBomb = 0;
    }
    private void LateUpdate()
    {
        if (isLeft)
        {
            PlayerMove(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (isRight)
        {

            PlayerMove(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (isDown)
        {
            PlayerMove(new Vector3(0, -speed * Time.deltaTime, 0));

        }
        if (isUp)
        {
            PlayerMove(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dog") || collision.gameObject.CompareTag("Farmer"))
        {
            gameController.Lose();
        }
    }
    private void OnValidate()
    {
        if (speed <= 0)
        {
            speed = 1;
        }
    }
    #endregion

    public void Left(bool isOn)
    {
        if (isOn)
        {
            ChangeSprite(spritePlayerLeft);
            ChangeColliderSize();
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
    }
    public void Right(bool isOn)
    {
        if (isOn)
        {
            ChangeSprite(spritePlayerRight);
            ChangeColliderSize();
            isRight = true;
        }
        else
        {
            isRight = false;
        }
    }
    public void Down(bool isOn)
    {
        if (isOn)
        {
            ChangeSprite(spritePlayerDown);
            ChangeColliderSize();
            isDown = true;
        }
        else
        {
            isDown = false;
        }
    }
    public void Up(bool isOn)
    {
        if (isOn)
        {
            ChangeSprite(spritePlayerUp);
            ChangeColliderSize();
            isUp = true;
        }
        else
        {
            isUp = false;
        }

    }
    void PlayerMove(Vector3 vector3)
    {
        transform.Translate(vector3);
    }
    public void PlantBomb()
    {
        if (countBomb < 3)
        {
            var newBomb = Instantiate(bomb, transform);
            countBomb++;
            newBomb.transform.SetParent(GameObject.Find("Bombs").transform);
        }
    }
    void ChangeColliderSize()
    {
        playerCollider.size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size / 1.4f;
    }
    void ChangeSprite(Sprite sprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
