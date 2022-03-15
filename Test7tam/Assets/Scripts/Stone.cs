using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer stoneSpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stoneSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (transform.position.y > player.transform.position.y)
        {
            stoneSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
        }
        else
        {
            stoneSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
        }
    }
}
