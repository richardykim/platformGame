using UnityEngine;

public class GroundAppearance : MonoBehaviour
{
    public Transform player;
    public MeshRenderer ground;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist > 3.5f)
        {
            ground.enabled = false;
        } 
        else 
        {
            ground.enabled = true;
        }
    }
}
