using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class acchub_http_get_all : MonoBehaviour
{
    [System.Serializable]
    public class client_data
    {
        [SerializeField]
        public int id;
        [SerializeField]
        public float hz;
        [SerializeField]
        public float power;
        [SerializeField]
        public int shake;
    }

    [System.Serializable]
    public class all_data
    {
        [SerializeField]
        public List<client_data> data;
    }

    public string topic_ = "debug/random";
    public float interval_http_get_ = 1.0f;
    private float time_last_request_;
    private float t_;

    private Dictionary<int, acchub_client_data> dict_;
    private SortedSet<int> set_check_delete_;

    public delegate acchub_client_data create_object(int id);
    public create_object d_create_object_;
    public delegate void delete_object(int id, acchub_client_data go);
    public delete_object d_delete_object_;

    // Use this for initialization
    void Start()
    {
        dict_ = new Dictionary<int, acchub_client_data>();
        set_check_delete_ = new SortedSet<int>();
    }

    // Update is called once per frame
    void Update()
    {
        t_ += Time.deltaTime;
        if (t_ - time_last_request_ > interval_http_get_)
        {
            time_last_request_ = t_;
            StartCoroutine(get_all_data("http://acchub.net/all/" + topic_));
        }
    }

    IEnumerator get_all_data(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        // リクエスト送信
        yield return request.SendWebRequest();

        // 通信エラーチェック
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                // UTF8文字列として取得する
                string text = request.downloadHandler.text;
                Debug.Log(text);
                all_data d = JsonUtility.FromJson<all_data>(text);
                Debug.Log("num_client:"+d.data.Count);
                update_gameobject(d);
            }
        }
    }

    private void update_gameobject(all_data data)
    {
        set_check_delete_.Clear();
        foreach(client_data d in data.data)
        {
            if (!dict_.ContainsKey(d.id))
            {
                acchub_client_data o = d_create_object_?.Invoke(d.id);
                dict_[d.id] = o;
            }
            set_check_delete_.Add(d.id);
            dict_[d.id].update_acchub_data(d.hz, d.power);

        }
        List<int> list_delete = new List<int>();
        foreach(KeyValuePair<int, acchub_client_data>kv in dict_)
        {
            if (!set_check_delete_.Contains(kv.Key))
            {
                d_delete_object_?.Invoke(kv.Key, kv.Value);
                list_delete.Add(kv.Key);
            }
        }
        foreach(int id in list_delete)
        {
            dict_.Remove(id);
        }
    }

}
