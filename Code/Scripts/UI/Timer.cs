using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
	public Text end;
    public Text highscoreText;
    public float time;
    public float highscore;
    public string text;


    private void Start()
    {
        timer.text = "";
        end.text = "";
    }


    private void Update()
    {
        time = time + Time.deltaTime;
        text = time.ToString("0.00");
        timer.text = text;
    }
}
