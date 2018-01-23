using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float waypointThresDist = 1.5f;      // Warning! Parameter should change in Unity during executing Unity

    private List<Transform> nodes;
    private int currentNode = 0;    // index of currentNode

    private CarController m_Car;    // from CarUserControl.cs, for controll car's steering, accel, footbrake, handbrake

    private float maxSteerAngle = 25f;      // this must be same as CarController setting

    private float targetSteerAngle = 0;
    private float turnSpeed = 22f;       // this is the parameter for smoothing steer
    private float targetSteerParam = 1.3f;

    // Use this for initialization
    private void Start () {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();      // get nodes
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        m_Car = GetComponent<CarController>();
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
        // real steer: -25 < < +25, Move parameter: -1 < < +1
        //m_Car.Move(, Accel(), 0f, 0f);       // steering, accel, footbrake, handbrake
        CalTargetSteer();
        LerpToSteerAngle();
        CheckWaypointDistance();
    }

    /**
     * algorithm that decide the steer
     * 
     * vehicle cordinate is always same: 'z' is a front of vehicle and 'x' is a right of vehicle.
     * 
     */
    private void CalTargetSteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);      // vector from vehicle to next waypoint
        //print(relativeVector);        // check relativeVector
        targetSteerAngle = relativeVector.x / relativeVector.magnitude * targetSteerParam;
        print(currentNode);
    }

    private void LerpToSteerAngle()
    {
        float smoothSteer = Mathf.Lerp(m_Car.CurrentSteerAngle / maxSteerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);        // smoothing steer angle
        m_Car.Move(smoothSteer, Accel(), 0f, 0f);       // steering, accel, footbrake, handbrake
    }

    private float Accel()
    {
        return 0.2f;
    }

    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < waypointThresDist) {      // 'transform' is an object's transform that include this script
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            } else
            {
                currentNode++;
            }
        }
    }
}
