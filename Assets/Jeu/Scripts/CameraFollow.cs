using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Cible à suivre")]
    public Transform target; // Glisse ton objet Player ici

    [Header("Paramètres de la caméra")]
    public Vector3 offset = new Vector3(0f, 3f, -10f); // Décalage de la caméra par rapport au joueur
    public float smoothSpeed = 5f; // Vitesse de lissage (plus c'est bas, plus c'est flottant)

    void LateUpdate()
    {
        // Si on n'a pas assigné de joueur, on ne fait rien
        if (target == null) return;

        // 1. On calcule la position idéale de la caméra (position du joueur + le décalage)
        Vector3 desiredPosition = target.position + offset;

        // 2. On adoucit le mouvement pour que ce soit plus agréable à l'œil (effet de rattrapage)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. On applique la position à la caméra
        transform.position = smoothedPosition;
    }
}