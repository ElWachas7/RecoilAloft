using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maincamera : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 desplazamiento;
    [SerializeField] float velocidadCamara;
    [SerializeField] public float despX = 0;
    [SerializeField] public float despY = 0;


    private void LateUpdate()
    {
        if (TrabucoMovement.mousePos != Vector3.zero)
        {
            Vector3 mouseWorldPos = TrabucoMovement.mousePos;
            despX = objetivo.position.x - transform.position.x;
            despY = objetivo.position.y - transform.position.y;
            if(mouseWorldPos.x - objetivo.position.x > 1) 
            {
                despX = -1;
            }
            if (mouseWorldPos.x - objetivo.position.x < -1)
            {
                despX = 1;
            }
            if (mouseWorldPos.y - objetivo.position.y > 1)
            {
                despY = -1;
            }
            if (mouseWorldPos.y - objetivo.position.y < -1)
            {
                despY = 1;
            }
        }

        

        desplazamiento = new Vector3(despX, despY, desplazamiento.z);
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
        transform.position = posicionSuavizada;
    }

}
