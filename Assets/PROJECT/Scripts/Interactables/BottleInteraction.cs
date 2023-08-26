using UnityEngine;

public class BottleInteraction : InteractableBase
{
    [field: SerializeField] private float sliderIncreaseAmount;
    [field: SerializeField] private int moneyAmount;
    [field: SerializeField] private ParticleSystem particle;
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnPlayerBarUpdate, sliderIncreaseAmount);
        EventManager.Broadcast(GameEvent.OnUpdateMoney, moneyAmount,"down");
        EventManager.Broadcast(GameEvent.OnTextAnimPlay, "down");
        Vibration.Vibrate(100);
        
        ParticleSystem particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        particleObj.Play();

        Destroy(this.gameObject);
    }
}
