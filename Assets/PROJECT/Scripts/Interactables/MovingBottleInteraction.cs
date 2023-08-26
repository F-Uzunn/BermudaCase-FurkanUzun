using UnityEngine;

public class MovingBottleInteraction : InteractableBase
{
    [field: SerializeField] private float sliderIncreaseAmount;
    [field: SerializeField] private int moneyAmount;
    [field: SerializeField] private ParticleSystem particle;

    public override void Start()
    {
        base.Start();
        startPointX = transform.localPosition.x;
    }
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnPlayerBarUpdate, sliderIncreaseAmount);
        EventManager.Broadcast(GameEvent.OnUpdateMoney, moneyAmount, "down");
        EventManager.Broadcast(GameEvent.OnTextAnimPlay, "down");
        Vibration.Vibrate(100);

        ParticleSystem particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        particleObj.Play();

        Destroy(this.gameObject);
    }
    public override void Movement()
    {
        base.Movement();
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.localPosition.x > startPointX + 2)
            moveSpeed *= -1;
        else if (transform.localPosition.x < startPointX - 2)
            moveSpeed *= -1;
    }
}
