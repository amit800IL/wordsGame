using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [Header("Attributtes")]
    [SerializeField] private float _entangleCoolDown;
    [SerializeField] private float _hookSpeed;
    [Header ("Components")]
    [SerializeField] private BoxCollider _boxCollider;
    bool _canShoot = true;
    private float timer;

    private void Update()
    {

        if (timer < Time.deltaTime)
        {
            ChangeShootState(true);
        }

    }
    private void Shoot()
    {
        
        if (!_canShoot) return;

    }
    private void Entangle()
    {

    }
    private void CatchWord()
    {

    }
    private void StartEntangle()
    {
        ChangeShootState(false);
        timer = Time.deltaTime + _entangleCoolDown;
    }
    private void ChangeShootState(bool state)
    {
        _canShoot = state;
        _boxCollider.enabled = state;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Hook")) StartEntangle(); 
    }
}

