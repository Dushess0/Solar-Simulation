
using UnityEngine;
using CSVWriter;
using System.IO;
enum State
{
    Simulate=0,
    newdata=1,
    action=2
}
public class Loop : MonoBehaviour
{

    public GameObject Boat;
    BoatAgent boatAgent;
    DataRecorder recorder;
  
    public bool paused = false;
    float[] actions;
    long iteration=0;
    State check_state()
    {
        return (State)int.Parse(System.IO.File.ReadAllText("state.txt"));

    }
    void change_state(State newState)
    {
        System.IO.File.WriteAllText("state.txt", ((int)newState).ToString());
    }
    void Start()
    {
        this.boatAgent = Boat.GetComponent<BoatAgent>();
        this.recorder = Boat.GetComponent<DataRecorder>();
        Time.timeScale = 0;
    }
  
   
    void Update()
    {

        State current = check_state();
        if (current==State.Simulate)
        {
            Debug.Log("simulating");
            var images = recorder.currentImages();
            for (int i = 0; i < images.Length; i++)
            {
                byte[] _bytes = images[i].EncodeToPNG();
                System.IO.File.WriteAllBytes(i + ".png", _bytes);
            }
            System.IO.File.WriteAllText("lidar.txt", Converter.VectorsToString(recorder.currentLidar()));
            System.IO.File.WriteAllText("ultrasound.txt", Converter.FloatsToString(recorder.currentUltrasound()));
            Time.timeScale = 0;
            change_state(State.newdata);
            Debug.Log("waiting for action");


        }
        
        else if (current==State.action)
        {

            using (TextReader reader = System.IO.File.OpenText("actions.txt"))
            {
                float left_engine = float.Parse(reader.ReadLine());
                float right_engine = float.Parse(reader.ReadLine());
                boatAgent.Action(new float[] { left_engine,right_engine }, "move") ;

            }
           
            Debug.Log("doing actions");
            Time.timeScale = 1;
            change_state(State.Simulate);
            


        }
        Debug.Log(current);










    }


}
