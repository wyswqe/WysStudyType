using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Tools;

public class Player : MonoBehaviour
{
    public bool isLocalPlayer = true;
    public string username;

    public GameObject playerPrefab;

    public GameObject localplayer;

    private SyncPositionRequest syncPosRequest;
    private SyncPlayerRequest syncPlayerRequest;

    private Vector3 lastPosition = Vector3.zero;
    private float moveOffset = 0.1f;

    private Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        //if (isLocalPlayer)
        //{
        localplayer.GetComponent<Renderer>().material.color = Color.red;
        syncPosRequest = GetComponent<SyncPositionRequest>();
        syncPlayerRequest = GetComponent<SyncPlayerRequest>();
        syncPlayerRequest.DefaultRequest();
        InvokeRepeating("SyncPosition", 3, 0.1f);
        //}
    }

    void SyncPosition()
    {
        if (Vector3.Distance(localplayer.transform.position, lastPosition) > 0.1f)
        {
            lastPosition = localplayer.transform.position;
            syncPosRequest.pos = localplayer.transform.position;
            syncPosRequest.DefaultRequest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (isLocalPlayer)
        //{
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        localplayer.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
        //}
    }

    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        //创建其他客户端的Player角色
        foreach (string username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    public void OnNewPlayerEvent(string username)
    {
        GameObject go = GameObject.Instantiate(playerPrefab);
        playerDict.Add(username, go);
    }

    public void OnSyncPositionEvent(List<PlayerData> playerDataList)
    {
        foreach (PlayerData pd in playerDataList)
        {
            GameObject go = DictTool.GetValue<string, GameObject>(playerDict, pd.Username);
            if (go != null)
            {
                go.transform.position = new Vector3() { x = pd.pos.x, y = pd.pos.y, z = pd.pos.z };
            }
        }
    }
}
