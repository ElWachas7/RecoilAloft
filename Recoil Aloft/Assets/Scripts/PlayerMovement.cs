using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float distRay = 2f;
    [SerializeField] private LayerMask whatToDetect;
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector2.down); //crea una linea
        Debug.DrawRay(ray.origin, ray.direction * distRay); //dibuja
        
        if(Physics.Raycast(ray, out hit, distRay)) 
        {
            Debug.Log("Distancia: " + hit.distance);
            Debug.Log("Punto de impacto: " + hit.point);
        }
    }
 
    
    
}
