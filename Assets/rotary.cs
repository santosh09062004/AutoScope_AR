using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.Text;

public class rotaryvideomovement : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Assign the VideoPlayer in the Inspector

    private string serverIP = "192.168.4.1"; // ESP32 AP Mode IP
    private int serverPort = 8080;           // ESP32 TCP Server Port
    private TcpClient tcpClient;
    private NetworkStream stream;
    private byte[] data;
    private int lastPosition = 0;
    private bool buttonPressed = false;

    private int totalScenes = 4;  // Total number of scenes
    public int currentSceneIndex;  // Starting scene index

    void Start()
    {
        ConnectToServer();
    }

    void Update()
    {
        if (tcpClient != null && tcpClient.Connected)
        {
            ReceiveData();
        }
    }

    void ConnectToServer()
    {
        try
        {
            tcpClient = new TcpClient(serverIP, serverPort);
            stream = tcpClient.GetStream();
            Debug.Log("Connected to ESP32 TCP Server");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Connection Error: " + e.Message);
        }
    }

    void ReceiveData()
    {
        if (stream.DataAvailable)
        {
            data = new byte[tcpClient.Available];
            stream.Read(data, 0, data.Length);
            string receivedData = Encoding.ASCII.GetString(data).Trim();

            // Handle rotary encoder data
            if (int.TryParse(receivedData, out int currentPosition))
            {
                if (currentPosition > lastPosition)
                {
                    MoveVideoForward();
                }
                else if (currentPosition < lastPosition)
                {
                    MoveVideoBackward();
                }

                lastPosition = currentPosition;
            }
            // Handle button press/release data for video
            else if (receivedData == "Button Pressed")
            {
                buttonPressed = true;
                TogglePlayPause();
            }
            else if (receivedData == "Button Released")
            {
                buttonPressed = false;
            }
            // Handle external button press for scene change
            else if (receivedData == "Change Scene")
            {
                ChangeScene();
            }
            else
            {
                Debug.LogWarning("Invalid Data Received: " + receivedData);
            }
        }
    }

    void MoveVideoForward()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.time += 10; // Adjust speed as needed
        }
    }

    void MoveVideoBackward()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.time -= 10; // Adjust speed as needed
        }
    }

    void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    void ChangeScene()
    {

        SceneManager.LoadScene(currentSceneIndex);  // Load the next scene
    }

    void OnApplicationQuit()
    {
        if (tcpClient != null && tcpClient.Connected)
        {
            stream.Close();
            tcpClient.Close();
        }
    }
}
