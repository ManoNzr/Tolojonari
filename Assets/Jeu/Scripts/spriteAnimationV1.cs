using UnityEngine;

public class spriteAnimationV1 : MonoBehaviour
{
    [Header("Configuration de l'animation")]
    public Texture[] walkTextures = new Texture[3];
    // Vitesse de l'animation (temps en secondes entre chaque image)
    public float frameRate = 0.15f;

    [Header("Références (Automatique)")]
    private Material targetMaterial;
    private CharacterController playerController;

    // Variables internes pour la logique
    private int currentCycleStep = 0; // On suit l'étape dans la boucle (0, 1, 2, 3)
    private float timer = 0f;
    private bool isWalking = false;

    void Start()
    {
        // 1. On récupère le Renderer de l'objet sur lequel est le script
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            targetMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Le script PaperMarioAnimation doit être sur un objet avec un Renderer (ton Sprite) !");
        }

        // 2. On cherche le CharacterController du parent (ton objet Joueur)
        playerController = GetComponentInParent<CharacterController>();
        if (playerController == null)
        {
            Debug.LogError("Le script ne trouve pas de CharacterController sur le parent !");
        }
    }

    void Update()
    {

        if (GameManager.Instance.IsPaused)
        {
            return;
        }


        if (targetMaterial == null || playerController == null || walkTextures.Length < 3) return;

        // 3. Est-ce que le joueur marche ?
        Vector3 horizontalVelocity = new Vector3(playerController.velocity.x, 0f, playerController.velocity.z);
        isWalking = horizontalVelocity.magnitude > 0.1f;

        // 4. Logique de l'animation (Boucle 0-1-0-2)
        if (isWalking)
        {
            timer += Time.deltaTime;

            if (timer >= frameRate)
            {
                timer = 0f;

                // On passe à l'étape suivante de la boucle (0, 1, 2, 3 -> retour à 0)
                currentCycleStep = (currentCycleStep + 1) % 4;

                // Logique de mapping : Associer l'étape du cycle (0-3) à l'index de texture (0-2)
                int textureIndexToAssign = 0; // Par défaut, on utilise la Frame 0

                if (currentCycleStep == 1)
                {
                    textureIndexToAssign = 1; // Étape 1 -> Frame 1
                }
                else if (currentCycleStep == 3)
                {
                    textureIndexToAssign = 2; // Étape 3 -> Frame 2
                }
                // Les étapes 0 et 2 restent à l'index 0 (Frame 0)

                // On applique la texture correspondante
                targetMaterial.mainTexture = walkTextures[textureIndexToAssign];
            }
        }
        else
        {
            // Le joueur est arrêté -> Force la Frame 0
            if (currentCycleStep != 0 || targetMaterial.mainTexture != walkTextures[0])
            {
                currentCycleStep = 0;
                targetMaterial.mainTexture = walkTextures[0];
                timer = 0f;
            }
        }
    }
}