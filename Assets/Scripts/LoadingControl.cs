using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingControl : MonoBehaviour
{
    AsyncOperation async;

    public GameObject progressImg;
    public Text text;

    List<GameObject> gameObjects;

    //读取场景的进度，它的取值范围在0 - 1 之间。
    public float progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update()
    {
        progress = async.progress;
        text.text = "正在加载游戏" + (int)(progress * 100) + "%";
        progressImg.transform.localScale = new Vector3(progress, 1, 1);
    }

    IEnumerator loadScene() {

        //for (int i = 0; i < 30; i++) {
        //    GameObject Barrier1 = ObjectPool.getInstance().GetObj("Barrier1");
        //    GameObject Barrier2 = ObjectPool.getInstance().GetObj("Barrier2");
        //    GameObject Barrier3 = ObjectPool.getInstance().GetObj("Barrier3");
        //    GameObject Monster1 = ObjectPool.getInstance().GetObj("Monster1");
        //    GameObject Monster2 = ObjectPool.getInstance().GetObj("Monster2");
        //    gameObjects.Add(Barrier1);
        //    gameObjects.Add(Barrier2);
        //    gameObjects.Add(Barrier3);
        //    gameObjects.Add(Monster1);
        //    gameObjects.Add(Monster2);
        //}

        //for (int j = 0; j < gameObjects.Count; j++) {
        //    ObjectPool.getInstance().RecycleObj(gameObjects[j]);
        //}


        //yield return new WaitForSeconds(3);

        // 异步读取场景。
		//Globe.loadName 就是A场景中需要读取的C场景名称。
		async = Application.LoadLevelAsync("GameScene");

        yield return async;
    }
}
