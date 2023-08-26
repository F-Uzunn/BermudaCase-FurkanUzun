using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinishInteraction : InteractableBase
{
    [field: SerializeField] private GameManager gameManager;
    [field: SerializeField] private List<ParticleSystem> confettiParticles;
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnFinish);
        EventManager.Broadcast(GameEvent.OnWin);
        Vibration.Vibrate(100);

        foreach (var item in confettiParticles)
        {
            item.Play();
        }
    }
}
