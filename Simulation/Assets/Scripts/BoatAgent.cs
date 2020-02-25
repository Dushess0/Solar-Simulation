using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Net.Sockets;
public class BoatAgent : Agent
{
    public GameObject boat;
    Rigidbody Rbody;
    Vector3 StartPos;
    [SerializeField]
    Engine LeftEngine;
    [SerializeField]
    Engine RightEngine;
    
    public float Reward;
    public float[] nextaction;
    DataRecorder recorder;

    

    SocketCommunicator communicator;
    
        

    void Start()
    {
        StartPos = boat.transform.position;
        Rbody = this.GetComponent<Rigidbody>();
        recorder = GetComponent<DataRecorder>();
        nextaction = new float[2];
        

        Reward = 0;
       
    }
    public override void CollectObservations()
    {
        recorder.Record();
        
        AddVectorObs(gameObject.transform.rotation);
        AddVectorObs(gameObject.transform.position);


       
        //foreach (var item in recorder.currentLidar())
        //{
        //    AddVectorObs(item);
        //}
        foreach (var item in recorder.currentUltrasound())
        {
            AddVectorObs(item);
        }

    }
    public override void AgentReset()
    {
        gameObject.transform.position = StartPos;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        Rbody.velocity = Vector3.zero;
        recorder.ClearRecordings();     
       // recorder.lidar.transform.rotation= new Quaternion(0, 0, 0, 0);
    }
    public void Action(float[] vectorAction, string textAction)
    {
        LeftEngine.Control = vectorAction[0];
        RightEngine.Control = vectorAction[1];
        LeftEngine.Step();
        RightEngine.Step();
       // recorder.lidar.Rotate();
    }
    
    public override void AgentAction(float[] vectorAction, string textAction)
    {
       // Action(nextaction, textAction);
    }
}
