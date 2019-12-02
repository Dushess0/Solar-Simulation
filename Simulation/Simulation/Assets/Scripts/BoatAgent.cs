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

    DataRecorder recorder;

    public float timer = 0;

    SocketCommunicator communicator;
    
        

    void Start()
    {
        StartPos = boat.transform.position;
        Rbody = boat.GetComponent<Rigidbody>();
        recorder = GetComponent<DataRecorder>();
        this.brain.brainParameters.vectorObservationSize = recorder.currentLidar.Length * 3 + recorder.currentUltraSound.Count+6;
        this.brain.brainParameters.vectorObservationSize = recorder.lidar.LasersCount * 3 + 7 + recorder.UltraSoundSensors.Length;

        Reward = 0;
       
    }
    public override void CollectObservations()
    {
        recorder.Record();
        
        AddVectorObs(gameObject.transform.rotation);
        AddVectorObs(gameObject.transform.position);
        foreach (var item in recorder.currentUltraSound)
        {
            AddVectorObs(item);
        }
        foreach (var item in recorder.currentLidar)
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
        timer = 0;
        

    }
    
    public override void AgentAction(float[] vectorAction, string textAction)
    {


        LeftEngine.Control = vectorAction[0];
        RightEngine.Control = vectorAction[1];
        LeftEngine.Step();
        RightEngine.Step();
        
        timer += Time.fixedDeltaTime;
        if (timer>10)
        {
            Done();
            timer = 0;
        }
        
        
        

       

        // if collide bouy then reward = -0.1f  
        //need to specify cost of time and cost of 

    }



}
