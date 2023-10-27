using System.Collections;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [Tooltip("Delay before Splash Screen starts formatted in Seconds.")]
    public int startupDelay;

    [Tooltip("The Background of the Splash Logo, used as Transition")]
    public Image background;

    [Tooltip("The Logo used within the Splash Screen.")]
    public Image logo;

    private async void Start()
    {
        if (startupDelay < 0)
            startupDelay = 0;

        if (logo.color.a > 0)
        {
            Color newColor = new()
            {
                a = 0
            };  

            logo.color = newColor;
        }

        await Task.Delay(startupDelay * 1000);

        Task task = FadeImage(true);
        await task;

        await Task.Delay(1000);

        // load assets here!

        await Task.Delay(1000);

        task = SizeImageY(background.rectTransform.sizeDelta.x, Screen.currentResolution.height);
        await task;
    }

    private async Task FadeImage(bool fadeIn)
    {
        Color UpdateAlpha(float i)
        {
            return new Color(1, 1, 1, i);
        }

        if (!fadeIn)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                logo.color = UpdateAlpha(i);
                await Task.Yield();
            }
        }

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            logo.color = UpdateAlpha(i);
            await Task.Yield();
        }
    }

    private async Task SizeImageY(float xSize, float yRes)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            background.rectTransform.sizeDelta = new Vector2(xSize, -Mathf.Lerp(0, yRes, i));
            await Task.Yield();
        }
    }
}
