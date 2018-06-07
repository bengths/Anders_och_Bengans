using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroManager : MonoBehaviour {
    public GameObject timeline;
    public GameObject thunder;

    private void Update()
    {
        if(PlayerPrefs.GetInt("PedoBamseIsDefeated") == 1)
        {
            OnPedoBamseDefeated();
        }
    }

    private void OnPedoBamseDefeated()
    {
        timeline.SetActive(true);
        thunder.SetActive(false);
    }

}
