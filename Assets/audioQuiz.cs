using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Questione
{
    public AudioClip questionAudioClip;
    public string correctAnswerName;
}

public class audioQuiz : MonoBehaviour
{
    public Text scoreText;
    public Text timerText; // نص التوقيت
    public Questione[] questions;
    public AudioSource audioSource;
    public AudioClip correctAnswerClip;
    public AudioClip backgroundClip;

    private int score = 0;
    private int currentQuestionIndex = -1;

    void Start()
    {
        UpdateScore();
        PlayBackgroundAndStartTimer();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Answer"))
        {
            if (IsCorrectAnswer(other.gameObject))
            {
                score++;
                UpdateScore();
                other.gameObject.SetActive(false);
                PlayCorrectAnswerSound();
            }
        }
    }

    bool IsCorrectAnswer(GameObject answerObject)
    {
        return currentQuestionIndex < questions.Length && answerObject.name == questions[currentQuestionIndex].correctAnswerName;
    }

    void PlayCorrectAnswerSound()
    {
        if (audioSource)
        {
            audioSource.Stop();
            if (correctAnswerClip)
            {
                audioSource.clip = correctAnswerClip;
                audioSource.Play();
                StartCoroutine(StopSoundAfterSeconds(5f));
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void PlayBackgroundAndStartTimer()
    {
        audioSource.Stop();
        if (backgroundClip && currentQuestionIndex + 1 < questions.Length) // يتحقق من وجود أسئلة أخرى
        {
            audioSource.clip = backgroundClip;
            audioSource.Play();
            StartCoroutine(StartCountdown());
        }
        else
        {
            SetNextQuestion();
        }
    }

    void SetNextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            if (audioSource && questions[currentQuestionIndex].questionAudioClip)
            {
                audioSource.clip = questions[currentQuestionIndex].questionAudioClip;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
            timerText.text = "اللعبة انتهت!"; // إخفاء النص التايمر
        }
    }

    IEnumerator StopSoundAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.Stop();
        PlayBackgroundAndStartTimer();
    }

    IEnumerator StartCountdown()
    {
        for (int i = 5; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        timerText.text = ""; // إخفاء النص بعد انتهاء العد التنازلي
        SetNextQuestion();
    }
}
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//[System.Serializable]
//public class Questione
//{
//    public AudioClip questionAudioClip;
//    public string correctAnswerName;
//}

//public class audioQuiz : MonoBehaviour
//{
//    public Text player1ScoreText;
//    public Text player2ScoreText;
//    public Text timerText;
//    public Questione[] questions;
//    public AudioSource audioSource;
//    public AudioClip correctAnswerClip;
//    public AudioClip backgroundClip;

//    private int player1Score = 0;
//    private int player2Score = 0;
//    private int currentQuestionIndex = -1;

//    void Start()
//    {
//        UpdateScore();
//        PlayBackgroundAndStartTimer();
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Answer"))
//        {
//            if (IsCorrectAnswer(other.gameObject))
//            {
//                if (other.gameObject.layer == LayerMask.NameToLayer("Player1")) // مثلاً: فلنفترض أن اللاعب 1 في الطبقة "Player1"
//                {
//                    player1Score++;
//                }
//                else if (other.gameObject.layer == LayerMask.NameToLayer("Player2"))
//                {
//                    player2Score++;
//                }

//                UpdateScore();
//                other.gameObject.SetActive(false);
//                PlayCorrectAnswerSound();
//            }
//        }
//    }

//    bool IsCorrectAnswer(GameObject answerObject)
//    {
//        return currentQuestionIndex < questions.Length && answerObject.name == questions[currentQuestionIndex].correctAnswerName;
//    }

//    void PlayCorrectAnswerSound()
//    {
//        if (audioSource)
//        {
//            audioSource.Stop();
//            if (correctAnswerClip)
//            {
//                audioSource.clip = correctAnswerClip;
//                audioSource.Play();
//                StartCoroutine(StopSoundAfterSeconds(5f));
//            }
//        }
//    }

//    void UpdateScore()
//    {
//        player1ScoreText.text = "Player 1 Score: " + player1Score;
//        player2ScoreText.text = "Player 2 Score: " + player2Score;
//    }

//    void PlayBackgroundAndStartTimer()
//    {
//        audioSource.Stop();
//        if (backgroundClip && currentQuestionIndex + 1 < questions.Length)
//        {
//            audioSource.clip = backgroundClip;
//            audioSource.Play();
//            StartCoroutine(StartCountdown());
//        }
//        else
//        {
//            SetNextQuestion();
//        }
//    }

//    void SetNextQuestion()
//    {
//        currentQuestionIndex++;

//        if (currentQuestionIndex < questions.Length)
//        {
//            if (audioSource && questions[currentQuestionIndex].questionAudioClip)
//            {
//                audioSource.clip = questions[currentQuestionIndex].questionAudioClip;
//                audioSource.Play();
//            }
//        }
//        else
//        {
//            audioSource.Stop();
//            timerText.text = ""; // إخفاء النص التايمر
//        }
//    }

//    IEnumerator StopSoundAfterSeconds(float seconds)
//    {
//        yield return new WaitForSeconds(seconds);
//        audioSource.Stop();
//        PlayBackgroundAndStartTimer();
//    }

//    IEnumerator StartCountdown()
//    {
//        for (int i = 5; i > 0; i--)
//        {
//            timerText.text = i.ToString();
//            yield return new WaitForSeconds(1f);
//        }
//        timerText.text = "";
//        SetNextQuestion();
//    }
//}
