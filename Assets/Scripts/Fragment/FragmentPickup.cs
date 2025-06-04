using UnityEngine;

public class FragmentPickup : MonoBehaviour
{
    // El id del fragmento
    [SerializeField] private string fragmentID;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Notificamos al Manager
            FragmentManager.Instance.CollectFragment(fragmentID);

            // Aca podriamos poner algun sonido, animacion o feedback owo
            Destroy(gameObject);
        }
    }
}
