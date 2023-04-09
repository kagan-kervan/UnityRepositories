using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleticsCameraTracker : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TrackObject();
    }

    private void TrackObject()
	{
        Vector3 new_pos = playerObj.transform.position;
        new_pos.y = this.transform.position.y;
        new_pos.z = -10;
        this.transform.position = new_pos;
	}
}
