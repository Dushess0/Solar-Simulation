using System;

using UnityEngine;

using RosSharp.RosBridgeClient.Actionlib;
using RosSharp.RosBridgeClient.Protocols;
using RosSharp.RosBridgeClient.MessageTypes.ActionlibTutorials;


namespace RosSharp.RosBridgeClient
{

    public class TestClient : MonoBehaviour
    {
        public string actionName = "fibonacci"; //rostopic
        public Protocol protocol = Protocol.WebSocketNET;
        public string ServerURL = "ws://192.168.137.30:9090";
        public RosSocket.SerializerEnum serializer = RosSocket.SerializerEnum.JSON;
        public int timeout = 10;
        public float timeStep = 0.2f;
        public int fibonacciOrder = 20;
        public string status = "";
        public string feedback = "";
        public string result = "";

        private FibonacciActionClient client;

        private void Awake()
        {
            FibonacciAction action = new FibonacciAction();
            action.action_goal.goal.order = fibonacciOrder;
            client = new FibonacciActionClient();



        }

            



       
    }


}
