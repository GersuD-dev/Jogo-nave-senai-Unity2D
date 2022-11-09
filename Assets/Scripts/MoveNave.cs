using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNave : MonoBehaviour
{
    public float limiteX;
    public float limiteY;
    Vector2 distancia;


    private void Update ()
    {
        Movimento ();
    }

    public void Movimento()
    {
        if (Input.GetMouseButtonDown (0))
            distancia = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);

        if (Input.GetMouseButton (0))
        {
            Vector2 pos = transform.position = Vector2.MoveTowards (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition), 50) + distancia;
            
            if (transform.position.x > limiteX) pos.x = limiteX;
            else if (transform.position.x < -limiteX) pos.x = -limiteX;

            
            if (transform.position.y > limiteY) pos.y = limiteY;
            else if (transform.position.y < -limiteY) pos.y = -limiteY;
            
            transform.position = pos;
        }

    }



}
