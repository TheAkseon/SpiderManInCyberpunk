using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForceOffend : MonoBehaviour
{
    [SerializeField] private ForceManager _forceManager;

    private List<Force> _forces;

    private void OnEnable()
    {
        _forces = FindObjectsOfType<Force>().ToList();

        foreach (Force force in _forces)
        {
            force.Offend += OnForceOffend;
        }
    }

    private void OnDisable()
    {
        foreach (Force force in _forces)
        {
            force.Offend -= OnForceOffend;
        }
    }

    private void OnForceOffend(Force force)
    {
        PlayerModifier playerModifier = FindObjectOfType<PlayerModifier>();

        if (playerModifier)
        {
            SoundsManager.Instance.PlaySound("CatchBonus");
            playerModifier.AddWidth(force.ForceValue);
            playerModifier.AddHeight(force.ForceValue);
            _forceManager.AddForce(force.ForceValue);
        }

        force.Offend -= OnForceOffend;
        _forces.Remove(force);

        Destroy(force.gameObject.transform.GetChild(0).gameObject);
        force.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}
