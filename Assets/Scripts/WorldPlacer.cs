using UnityEngine;

public class WorldPlacer : MonoBehaviour
{
    [SerializeField] private WordSpawner spawner;

    [SerializeField] private string Text;
    public void PlaceWord()
    {
        if (spawner != null)
        {
            spawner.SpawnWord(Text);
        }
    }
}
