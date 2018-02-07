using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Radius = 3f;
    public Transform InteractionTransform;

    private bool isFocus = false;
    private bool hasInteracted = false;

    private Transform player;

    public virtual void Interact()
    {
       // Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, InteractionTransform.position);

            if (distance <= Radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (InteractionTransform == null)
        {
            InteractionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position, Radius);
    }
}
