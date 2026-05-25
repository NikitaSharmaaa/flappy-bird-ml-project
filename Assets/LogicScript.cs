using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int playerscore;
    public Text scoretext;
    public GameObject gameover;
    public Text scoredisplay;
    public Text highestscored;
    public int highscore;
    public Birdscript Birdscript;
    public GameObject pausescreen;
    Sound_Manager soundmanager;
    public PipeSpawn PipeSpawn;

    private void Awake()
    {
        soundmanager = GameObject.FindGameObjectWithTag("Soundfx").GetComponent<Sound_Manager>();
    }

    [ContextMenu("Increase Score")]
    public void addscore(int Scoretoadd)
    {
        //soundmanager.PlaySFX(soundmanager.point);
        playerscore++;
        scoretext.text = playerscore.ToString();
    }

    public void restartgame()
    {
        playerscore = 0;
        scoretext.text = "0";

        // Reset bird physics
        Birdscript.myRigidbody2D.linearVelocity = Vector2.zero;
        Birdscript.myRigidbody2D.angularVelocity = 0f;

        // Reset bird transform relative to environment root
        Birdscript.transform.localPosition = Vector3.zero;
        Birdscript.transform.localRotation = Quaternion.identity;

        // Mark bird alive
        Birdscript.isBirdAlive = true;

        // Reset pipes
        PipeSpawn.ResetPipes();
    }

    public void gameoverscreen()
    {
        loadhighscore();
        scoredisplay.text = "Current Score : " + playerscore.ToString();
        if (playerscore > highscore)
        {
            Savehighscore();
            highscore = playerscore;
        }
        highestscored.text = "Highest Score : " + highscore.ToString();
        gameover.SetActive(true);
    }

    private void loadhighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    private void Savehighscore()
    {
        PlayerPrefs.SetInt("Highscore",playerscore);
        PlayerPrefs.Save();
    }

    public void pause()
    {
        if (!gameover.activeSelf)
        {
            pausescreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void resume()
    {
        pausescreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void exit()
    {
        Application.Quit();
    }
}
