using System;
using UnityEngine;

public class SentenceArranger : MonoBehaviour
{
    private Word currentWord;
    public PlayerMovement playerMovement;
    private int _slotCount;
    [SerializeField] private GameObject _rail;
    private Vector3[] slotsPositions;
    private void SetSlotCount(float railSize)
    {
        _slotCount = Convert.ToInt32(railSize);
    }

    private void Start()
    {
        slotsPositions = new Vector3[4];
        DivideIntSlots();
    }

    public void ArrangeSentenceInOrder()
    {
        if (currentWord != null)
        {
            float holderScale = playerMovement.GetWordHolderScale();
            SetSlotCount(holderScale);

            Vector3 newPosition = playerMovement.transform.position - playerMovement.transform.forward + new Vector3(holderScale, 1f, 0f);

            currentWord.transform.position = newPosition;
        }
    }
    private void DivideIntSlots()
    {
        float holderScale = playerMovement.GetWordHolderScale();

        for (int i = 0; i < slotsPositions.Length; i++)
        {
            Vector3 position = Vector3.right * holderScale * (i * 1f / 5f - 0.4f);

            slotsPositions[i] = position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var slot in slotsPositions)
        {
            Gizmos.DrawSphere(slot, 0.2f);
        }
    }
}
