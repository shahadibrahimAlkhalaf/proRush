using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string questionText;
    public string correctAnswerName;
}


public class PlayerQuiz : MonoBehaviour
{
    private float audioTimer = 0.0f;
    private bool isPlayingCorrectAnswerSound = false;
    public ArabicFixer arabicFixer;
    public Text questionText;
    public Text scoreText;
    public Question[] questions;  // قائمة الأسئلة

    private int score = 0;
    private int currentQuestionIndex = -1;  // يشير إلى السؤال الحالي

    // 1. إضافة متغيرات الصوت هنا
    public AudioSource audioSource; // مكون المصدر الصوتي
    public AudioClip correctAnswerClip; // مقطع الصوت للإجابة الصحيحة

    void Start()
    {
        UpdateScore();
        SetNextQuestion();
    }
    void Update()
    {
        if (isPlayingCorrectAnswerSound)
        {
            audioTimer -= Time.deltaTime;

            if (audioTimer <= 0)
            {
                audioSource.Stop();
                isPlayingCorrectAnswerSound = false;
            }
        }
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

                SetNextQuestion();

                PlayCorrectAnswerSound();
            }
        }
    }

    bool IsCorrectAnswer(GameObject answerObject)
    {
        if (currentQuestionIndex < questions.Length)
            return answerObject.name == questions[currentQuestionIndex].correctAnswerName;
        return false;
    }

    void PlayCorrectAnswerSound()
    {
        // تشغيل مقطع الصوت للإجابة الصحيحة
        if (audioSource && correctAnswerClip)
        {
            audioSource.PlayOneShot(correctAnswerClip);
            audioTimer = 5.0f; // ضبط المؤقت على 5 ثوان
            isPlayingCorrectAnswerSound = true;
        }
    }

    void UpdateScore()
    {
        // إعداد النص بشكل عادي
        string combinedText = "نقاط: " + score;

        // استخدام الدالة لتحويل النص إلى العربي بشكل صحيح
        string arabicText = arabicFixer.FixTextForUI(combinedText);

        // تعيين النص النهائي لعنصر الواجهة
        scoreText.text = arabicText;
    }


    void SetNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            string arabicText = arabicFixer.FixTextForUI(questions[currentQuestionIndex].questionText);
            questionText.text = arabicText;
        }
        else
        {
            string arabicText = arabicFixer.FixTextForUI("تهانينا! تم الإجابة على جميع الأسئلة!");
            questionText.text = arabicText;
        }
    }


}