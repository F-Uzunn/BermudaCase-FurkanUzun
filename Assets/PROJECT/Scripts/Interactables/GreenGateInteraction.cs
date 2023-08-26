using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GreenGateInteraction : InteractableBase
{
    [field: SerializeField] private GateTypeEnum gateType;

    [field: SerializeField] private float sliderIncreaseAmount;
    [field: SerializeField] private int moneyAmount;

    [field: SerializeField] private ParticleSystem particle;

    [field: SerializeField] private TextMeshPro gateText;

    private string[] greenGateStrings = { "STUDY", "WORK", "EXERCISE" };
    [field: SerializeField] private GameObject[] gateSprites;
    private void Awake()
    {
        int val = Random.Range(0, greenGateStrings.Length);
        gateText.text = greenGateStrings[val];
        gateSprites[val].SetActive(true);
    }
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnPlayerBarUpdate, sliderIncreaseAmount);
        EventManager.Broadcast(GameEvent.OnUpdateMoney, moneyAmount, "up");
        EventManager.Broadcast(GameEvent.OnTextAnimPlay, "up");
        Vibration.Vibrate(100);

        ParticleSystem particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        particleObj.Play();

        Destroy(this.gameObject);
    }
}
