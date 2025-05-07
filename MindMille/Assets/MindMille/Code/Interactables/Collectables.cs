using UnityEngine;
using UnityEngine.InputSystem;

public class Collectables : MonoBehaviour
{
    public float collectRadius = 2f;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.OtherInputs.Interact.performed += ctx => TryCollect();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void TryCollect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collectRadius);

        foreach (Collider colliderr in colliders)
        {
            if (colliderr.CompareTag("Collectable"))
            {
                Debug.Log("Objeto recogido: " + colliderr.name);
                Destroy(colliderr.gameObject);
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
}
