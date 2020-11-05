using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cam;
    public GameObject Projectile;
    public string FireAxis = "Fire1";
    public string SecondAxis = "Fire2";
    public string HealthPrefix = string.Empty;
    public Text HealthText = null;
    public string ScorePrefix = string.Empty;
    public int Score = 0;
    public Text ScoreText = null;
    public string WavePrefix = string.Empty;
    public Text WaveText = null;
    public bool CanFire = true;
    public float ReloadDelay = 0.5f;
    public float ReloadTimer = 0.0f;
    public float RunCooldown = 0.0f;

    public float WaveTimer = 0f;
    public float WaveDelay = 20f;
    public bool WaveWait = false;
    public int wave = 0;

    public Light lt;
    Color white = Color.white;
    Color red = Color.red;
    public Canvas StartingCanvas = null;
    public Canvas NormalCanvas = null;
    public bool HasStarted = false;
    public AudioSource audioData;
    public AudioSource audioSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasStarted == false)
        {
            StartingCanvas.enabled = true;
            NormalCanvas.enabled = false;
            NormalCanvas.enabled = true;
            wave = 0;
        }
        if (HealthText != null)
        {
            Health H = Player.GetComponent<Health>();
            HealthText.text = HealthPrefix + System.Math.Round(H.HealthPoints, 0).ToString();
        }
        if (ScoreText != null)
        {
            ScoreText.text = ScorePrefix + Score.ToString();
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        //fire shots out of the camera
        if (Input.GetButtonDown(FireAxis) && CanFire && HasStarted)
        {
            GameObject shot = GameObject.Instantiate(Projectile, Cam.transform.position, Cam.transform.rotation);
            CanFire = false;
        }
        //speed boost the player when m2 is pressed
        if (Input.GetButtonDown(SecondAxis) && HasStarted && RunCooldown > 15f)
        {
            Player.GetComponent<FPScontroller>().isRunning = true;
            CanFire = false;
            ReloadTimer = -1f;
            RunCooldown = 0f;
            audioSpeed.Play(0);
        }
        else
        {
            ReloadTimer += Time.deltaTime;
            if (CanFire || ReloadTimer > ReloadDelay)
            {
                ReloadTimer = 0f;
                CanFire = true;
            }
        }
        RunCooldown += Time.deltaTime;

        CheckWaveUpdate();

    }

    void CheckWaveUpdate()
    {
        //Updates wave, lets spawners know when to spawn objects.
        if (WaveTimer > WaveDelay && !WaveWait)
        {
            Score += 25;
            WaveWait = true;
            WaveTimer = 0;
            wave++;
            WaveText.text = WavePrefix + (wave.ToString());
            WaveText.gameObject.SetActive(true);

            
        }
        else if (WaveTimer > 10f && WaveWait)
        {
            WaveWait = false;
            WaveTimer = 0;
            WaveText.gameObject.SetActive(false);
        }
        else
        {
            WaveTimer += Time.deltaTime;
        }

        if (wave == 0)
        {
            WaveText.gameObject.SetActive(false);
        }

        if (wave == 10 && !WaveWait)
        {
            lt.color = Color.Lerp(white, red, (WaveTimer/WaveDelay));
        }
    }
    public void BeginGame()
    {
        HasStarted = true;
        StartingCanvas.enabled = false;
        audioData.Play(0);
        Score = 0;
        wave = 0;
        Player.GetComponent<Health>().HealthPoints = 100f;
        lt.color = white;
    }
}
