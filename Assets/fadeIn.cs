using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class fadeIn : MonoBehaviour {
    private Texture2D txt2d;
    public int frames = 111;
    float alpha=1;
    void Start()
    {
        txt2d = new Texture2D(Screen.width, Screen.height);
        for (int y = 0; y < Screen.height; y++) for (int x = 0; x < Screen.width; x++) txt2d.SetPixel(x, y, Color.black);
        txt2d.Apply();
    }
    void OnGUI()
    {
            Debug.Log("?");
            Color c = GUI.color;
            c.a = alpha = Mathf.Clamp01(alpha); if (alpha >0 ){ alpha -= 1.0f/frames; }
            GUI.color = c;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), txt2d);
    }
}
