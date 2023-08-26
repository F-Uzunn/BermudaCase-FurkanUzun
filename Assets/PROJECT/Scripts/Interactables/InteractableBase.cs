using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class InteractableBase : MonoBehaviour,IMovingBottle
{
    private bool _isInteractable;
    public float moveSpeed { get; set; } = 2f;
    public float startPointX { get; set; }

    public virtual void Start()
    {

    }
    private void Update()
    {
        if (_isInteractable)
        {
            Interact();
            _isInteractable = false;
        }
        Movement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInteractable = true;
        }
    }
    public abstract void Interact();

    public virtual void Movement()
    {

    }
}
