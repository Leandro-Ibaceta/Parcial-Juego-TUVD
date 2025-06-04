using System;
using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{
    public static FragmentManager Instance { get; private set; }

    //Lista de fragmentos ya recogidos (para evitar recoger dos veces el mismo)
    private HashSet<string> collectedFragmentIDs = new HashSet<string>();

    public int FragmentsCollected => collectedFragmentIDs.Count;

    // Notifica a los suscriptos que cambio el conteo
    public event Action<int> OnFragmentCountChanged;

    // Evento específico cuando se alcanza X fragmentos
    public event Action OnAllFragmentsCollected;

    // Total que se necesitan para completar el hito
    [SerializeField] private int totalFragmentsNeeded = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Se lo llama cuando el player colisione o agarre un fragmento
    // fragmentID es el id de ese fragmento
    public void CollectFragment(string fragmentID)
    {
        // Si ya tiene ese fragmento lo ignoramos
        if (collectedFragmentIDs.Contains(fragmentID))
            return;

        // Agregamos el ID a la lista
        collectedFragmentIDs.Add(fragmentID);

        // Avisamos a quien escuche el cambio de la cantidad de fragmentos
        OnFragmentCountChanged?.Invoke(FragmentsCollected);

        // Si alcanzamos el total del hito hacemos evento de “todos recogidos”
        if (FragmentsCollected >= totalFragmentsNeeded)
        {
            OnAllFragmentsCollected?.Invoke();
        }
    }

    // Podriamos usarlo cuando muere el player reiniciar los gragmentos, habria que expandir bien para que sepa cuantos tenia antes de empezar esa parte.
    public void ResetFragments()
    {
        collectedFragmentIDs.Clear();
        OnFragmentCountChanged?.Invoke(FragmentsCollected);
    }

    private void OnDestroy()
    {
        OnFragmentCountChanged = null;
        OnAllFragmentsCollected = null;
    }
}
