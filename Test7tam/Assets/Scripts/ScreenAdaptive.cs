using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdaptive : MonoBehaviour
{
    private void Awake()
    {
        AddaptiveGameZone();
    }
    void AddaptiveGameZone()
    {
        float height = Camera.main.pixelHeight;

        var bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        var topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, height));

        var visota = new Vector2(topLeft.x - bottomLeft.x, topLeft.y - bottomLeft.y);

        var scaleY = visota.magnitude;
        var scaleX = scaleY * Screen.width / Screen.height;

        transform.localScale = new Vector3(scaleX / 23f, scaleY / 10f, 1f);
    }
}
