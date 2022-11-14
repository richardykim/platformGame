using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDisappear : MonoBehaviour
{
    public MeshRenderer ground;
    public BoxCollider collider;

    // Update is called once per frame
    void Start()
    {
        // every 2 seconds, the ground disappears for 2 seconds..
        InvokeRepeating("TogglePlatform", 2.0f, 3f);
    }

    private void TogglePlatform()
    {
        ground.enabled = !ground.enabled;
        collider.enabled = !collider.enabled;
    }
 
}
