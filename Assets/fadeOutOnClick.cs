using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class fadeOutOnClick : MonoBehaviour {
    public Texture2D txt2d=null;
	public string toScene="";
    public int frames = 111;
    bool click = false;
    bool change = false;
    float alpha=0;
    void Start()
    {
		if(txt2d==null) return;
        txt2d = new Texture2D(Screen.width, Screen.height);
        for (int y = 0; y < Screen.height; y++) for (int x = 0; x < Screen.width; x++) txt2d.SetPixel(x, y, Color.black);
        txt2d.Apply();
		//txt2d.transform.parent=GameObject.Find("screen").transform;
    }
    // Use this for initialization
    public void Click()
    {
		//SteamVR_Fade.View(Color.white,0.0f);
		Color t=Color.black;
		t.a=1f;
        SteamVR_Fade.View(t,5.0f);
        //click = true;
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
			//GUI.depth=-1000;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), txt2d);
            if (alpha >= 1) change = true;
            else alpha += 1.0f/frames;
        }
    }
}
