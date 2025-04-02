using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrabucoMovement : MonoBehaviour
{

    [SerializeField] private Camera mainCam;
    public static Vector3 mousePos;
    [SerializeField] private int bullets = 3;
    public Transform bulletTransform;
    [SerializeField] private float recoilForce = 5f;
    [SerializeField] private Rigidbody2D playerRb;
    private Vector2 recoilDirection;
    private Vector2 recoilVelocity = Vector2.zero; // Para suavizar el recoil
    [SerializeField] private float recoilRecoverySpeed = 2f;
    // Umbral de velocidad para considerar que el objeto está detenido
    [SerializeField] private float velocityThreshold = 0.5f;
    [SerializeField] public bool canfire = true;
    private PlayerMovement playerMovement;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        RaycastHit hit = playerMovement.GetComponent<RaycastHit>();
        //playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (canfire) 
        {
            Shoot();
        }
        RecoverRecoil();
        StopIfSlow();
        reload();
    }

    public void Shoot() // esta funcion solo consume 1 bala
    {
            if (Input.GetMouseButtonDown(0))
            {
                 recoilDirection = -transform.right;
                 ApplyRecoil();
                 bullets--;
                    if (bullets <= 0) 
                    {
                        canfire = false;
                    }
                
            }
    }
    public void ApplyRecoil() // mueve la pelota
    {
        playerRb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
    }

    private void RecoverRecoil() // disminuir el recoil
    {
        if (recoilVelocity.sqrMagnitude > 0.1f)
        {
            recoilVelocity = Vector2.Lerp(recoilVelocity, Vector2.zero, Time.deltaTime * recoilRecoverySpeed);
            playerRb.AddForce(recoilVelocity, ForceMode2D.Force);
        }
    }
    private void StopIfSlow()
    {
        if (playerRb.velocity.sqrMagnitude < velocityThreshold * velocityThreshold)
        {
            playerRb.velocity = Vector2.zero; 
        }
    }

    private void reload() 
    {
        if (playerRb.velocity == Vector2.zero ) 
        {
                bullets = 3;
            canfire = true;
        }
    }
}