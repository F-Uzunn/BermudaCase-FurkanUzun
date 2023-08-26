using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RedGateInteraction : InteractableBase
{
    [field: SerializeField]private GateTypeEnum gateType;

    [field: SerializeField] private float sliderIncreaseAmount;
    [field: SerializeField] private int moneyAmount;

    [field: SerializeField] private ParticleSystem particle;

    [field: SerializeField] private TextMeshPro gateText;

    private string[] redGateStrings = { "GAMES", "DRINK", "SLEEP" };
    [field: SerializeField] GameObject[] gateSprites;

    private void Awake()
    {
        int val = Random.Range(0, redGateStrings.Length);
        gateText.text = redGateStrings[val];
        gateSprites[val].SetActive(true);
    }
    public override void Interact()
    {
        EventManager.Broadcast(GameEvent.OnPlayerBarUpdate, sliderIncreaseAmount);
        EventManager.Broadcast(GameEvent.OnUpdateMoney, moneyAmount, "down");
        EventManager.Broadcast(GameEvent.OnTextAnimPlay, "down");
        Vibration.Vibrate(100);

        ParticleSystem particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        particleObj.Play();

        foreach (Transform child in transform.parent)
        {
            child.GetComponent<Collider>().enabled = false;
        }

        Destroy(this.gameObject);
    }
}
