using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -2);
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}