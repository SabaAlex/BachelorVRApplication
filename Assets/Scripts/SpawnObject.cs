using Assets.Scripts;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject TargetPrefab;

    public GameObject PlayerObject;

    public Vector3 center;
    public Vector3 size;

    Vector3 initialSize;

    public Vector3 lastTargetLocation;

    // Start is called before the first frame update
    void Start()
    {
        initialSize = size;
        lastTargetLocation = new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTarget()
    {
        var lastDistanceCoefficient = EnvironmentVariablesClass.LastDistanceCoefficient;
        var playerClosenessCoefficient = EnvironmentVariablesClass.ClosenessCoefficient;

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        var newPosition = Vector3.Lerp(lastTargetLocation, pos, lastDistanceCoefficient);

        var newPosition2 = Vector3.Lerp(PlayerObject.transform.position, newPosition, playerClosenessCoefficient);

        Instantiate(TargetPrefab, newPosition2, Quaternion.identity);

        lastTargetLocation = pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    public void UpdateCoefficients()
    {
        var newSize = initialSize * EnvironmentVariablesClass.SizeCoefficient;

        size.x = newSize.x;
        size.z = newSize.z;
    }

    public Vector3 GetCoeff()
    {
        return size;
    }
}
