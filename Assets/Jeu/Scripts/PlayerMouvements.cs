using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Android;

[RequireComponent(typeof(CharacterController))]
public class PlayerMouvements : MonoBehaviour
{
    [Header("Paramètres de déplacement")]
    public float speed = 7f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    [Header("Visuel (Le Sprite de Mario)")]
    public Transform spriteTransform;

    [Header("Particules de marche")]
    public ParticleSystem walkParticles;
    public float particleRateOnGround = 3f;

    void Start()
    {
        // On récupère le composant CharacterController attaché au joueur
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }

        // 1. Déplacement au sol et Saut
        if (controller.isGrounded)
        {
            // Récupère les entrées du clavier/manette
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            // On crée le vecteur de direction (X pour gauche/droite, Z pour haut/bas en 3D)
            moveDirection = new Vector3(-moveVertical, 0.0f, moveHorizontal).normalized;
            moveDirection *= speed;

            if (walkParticles != null)
            {
                var emission = walkParticles.emission;
                emission.rateOverDistance = particleRateOnGround;
            }

            // Gestion du saut
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            
        }
        else
        {
            if (walkParticles != null)
            {
                var emission = walkParticles.emission;
                emission.rateOverDistance = 0f;
            }
        }

        // 2. Gestion de l'orientation du "papier" (gauche/droite)
        FlipSprite(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 3. Application de la gravité
        moveDirection.y -= gravity * Time.deltaTime;

        // 4. On déplace le personnage
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void FlipSprite(float horizontalInput, float verticalInput)
    {
        // Dans Paper Mario, le personnage se retourne comme une feuille de papier
        if (horizontalInput > 0) // Va vers la droite
        {
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0) // Va vers la gauche
        {
            spriteTransform.localScale = new Vector3(1,1,-1);
        }
        if (verticalInput > 0)
        {
            spriteTransform.rotation = Quaternion.Euler(0, 180, 0); // On retourne à 180 degrés !
        }
        else if (verticalInput < 0)
        {
            spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}