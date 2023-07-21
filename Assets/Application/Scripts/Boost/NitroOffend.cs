using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NitroOffend : MonoBehaviour
{
    [SerializeField] private List<Nitro> _nitros;

    private void OnEnable()
    {
        _nitros = FindObjectsOfType<Nitro>().ToList();

        foreach (Nitro nitro in _nitros)
        {
            nitro.Offend += OnNitroOffend;
        }
    }

    private void OnDisable()
    {
        foreach (Nitro nitro in _nitros)
        {
            nitro.Offend -= OnNitroOffend;
        }
    }

    private void OnNitroOffend(Nitro nitro)
    {
        PlayerMove playerMove = FindObjectOfType<PlayerMove>();

        if (playerMove)
        {
            playerMove.ApplyNitro(nitro.TimeApplyNitro, nitro.NitroMultiplier);
        }
    }
}
