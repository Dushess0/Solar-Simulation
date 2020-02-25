
using UnityEngine;
using CSVWriter;
using System.IO;
using Unity;
using UnityEngine.SceneManagement;
enum State
{
    Simulate=0,
    newdata=1,
    action=2,
    reset=100,
    done_reset=102
}
public class Loop : MonoBehaviour
{

    public GameObject Boat;
    BoatAgent boatAgent;
    DataRecorder recorder;
  
    public bool paused = false;
    public bool enabled;
    float[] actions;
    long iteration=0;

    FileStream oStream;
    FileStream iStream;
    StreamWriter sw;
    StreamReader sr;

    FileStream lidar_writer;
    FileStream ultrasound_writer;
    FileStream[] images_writers;
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
        if (enabled)
        {


            Time.timeScale = 0;

            sw = new System.IO.StreamWriter(oStream);
            sr = new System.IO.StreamReader(iStream);





            lidar_writer = new FileStream("lidar.txt", FileMode.Create, FileAccess.Write, FileShare.Read);

            ultrasound_writer = new FileStream("ultrasound.txt", FileMode.Create, FileAccess.Write, FileShare.Read);
            images_writers = new FileStream[recorder.Cameras.Length];
            for (int i = 0; i < recorder.Cameras.Length; i++)
            {
                images_writers[i] = new FileStream(i + ".png", FileMode.Create, FileAccess.Write, FileShare.Read);
            }
        }


       


    }


    void Update()
    {
        if (enabled)
        { 
            State current = check_state();
        if (current == State.Simulate)
        {
            Debug.Log("simulating");
            var images = recorder.currentImages();
            for (int i = 0; i < images.Length; i++)
            {
                byte[] _bytes = images[i].EncodeToPNG();
                images_writers[i].Write(_bytes, 0, _bytes.Length);

            }
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(Converter.VectorsToString(recorder.currentLidar()));
            lidar_writer.Write(info, 0, info.Length);
            info = new System.Text.UTF8Encoding(true).GetBytes(Converter.FloatsToString(recorder.currentUltrasound()));
            ultrasound_writer.Write(info, 0, info.Length);
            Time.timeScale = 0;
            change_state(State.newdata);
            Debug.Log("waiting for action");


        }

        else if (current == State.action)
        {

            using (TextReader reader = System.IO.File.OpenText("actions.txt"))
            {
                float left_engine = float.Parse(reader.ReadLine());
                float right_engine = float.Parse(reader.ReadLine());
                boatAgent.Action(new float[] { left_engine, right_engine }, "move");

            }

            Debug.Log("doing actions");
            Time.timeScale = 1;
            change_state(State.Simulate);



        }
        else if (current == State.reset)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            change_state(State.done_reset);


        }
        Debug.Log(current);
    }










    }


}
