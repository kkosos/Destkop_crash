using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class fadeOutOnClick : MonoBehaviour {
    private Texture2D txt2d;
	public string toScene="";
    public int frames = 111;
    bool click = false;
    bool change = false;
    float alpha=0;
    void Start()
    {
        txt2d = new Texture2D(Screen.width, Screen.height);
        for (int y = 0; y < Screen.height; y++) for (int x = 0; x < Screen.width; x++) txt2d.SetPixel(x, y, Color.black);
        txt2d.Apply();
    }
    // Use this for initialization
    public void Click()
    {
        click = true;
        Debug.Log("AAA");
        //StartCoroutine(changeScene());
    }
    void Update()
    {
        if(change) SceneManager.LoadScene(toScene);
    }
    void OnGUI()
    {
        if(click)
        {
            Debug.Log("?");
            Color c = GUI.color;
            c.a = alpha = Mathf.Clamp01(alpha);
            GUI.color = c;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), txt2d);
            if (alpha >= 1) change = true;
            else alpha += 1.0f/frames;
        }
    }
}
