using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInteraction : InteractableBase
{
    [field: SerializeField] private float sliderIncreaseAmount;
    [field: SerializeField] private int moneyAmount;
    [field: SerializeField] private float rotationSpeed;
    [field: SerializeField] private ParticleSystem particle;
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnPlayerBarUpdate, sliderIncreaseAmount);
        EventManager.Broadcast(GameEvent.OnUpdateMoney, moneyAmount,"up");
        EventManager.Broadcast(GameEvent.OnTextAnimPlay, "up");
        Vibration.Vibrate(100);

        ParticleSystem particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        particleObj.Play();

        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
    }
}
