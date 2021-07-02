using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    VideoPlayer video;
    public Image gameOver, homeButton;
    public Text homeText;
    bool flag;

    void Start()
    {
        video = this.GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (!video.isPlaying && !flag)
        {
            float fadeTime = 0.3f;
            flag = true;
            StartCoroutine(FadeOutVideo(video, fadeTime));
            Invoke("VideoInactive", fadeTime);
        }
    }

    void VideoInactive()
    {
        float fadeTime = 3.0f;

        StartCoroutine(FadeInImage(gameOver, 0.5f, fadeTime));
        StartCoroutine(FadeInImage(homeButton, 0.2f, fadeTime));
        StartCoroutine(FadeInText(homeText, 1f, fadeTime));
    }

    IEnumerator FadeInImage(Image image, float alpah, float time)
    {
        Color color = image.color;
        while (color.a < alpah)
        {
            color.a += Time.deltaTime / time * alpah;
            image.color = color;

            yield return null;
        }
    }
    IEnumerator FadeInText(Text text, float alpah, float time)
    {
        Color color = text.color;
        while (color.a < alpah)
        {
            color.a += Time.deltaTime / time * alpah;
            text.color = color;

            yield return null;
        }
    }
    IEnumerator FadeOutVideo(VideoPlayer video, float time)
    {
        while (video.targetCameraAlpha > 0)
        {
            video.targetCameraAlpha  -= Time.deltaTime / time;
            yield return null;
        }
    }
}
