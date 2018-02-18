using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acchub_object_controller : MonoBehaviour {
    public GameObject pref_client_;
    public acchub_http_get_all acchub_http_get_all_;

    public float min_x_spawn_ = -3f;
    public float max_x_spawn_ = 3f;
    public float min_y_spawn_ = 0f;
    public float max_y_spawn_ = 1f;
    public float min_z_spawn_ = -3f;
    public float max_z_spawn_ = 3f;

    // Use this for initialization
    void Start () {
        acchub_http_get_all_.d_create_object_ += create_object;
        acchub_http_get_all_.d_delete_object_ += delete_object;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    acchub_client_data create_object(int id)
    {
        GameObject go = Instantiate(pref_client_);
        //random position
        go.transform.position = new Vector3(Random.Range(min_x_spawn_, max_x_spawn_), Random.Range(min_y_spawn_, max_y_spawn_), Random.Range(min_z_spawn_, max_z_spawn_));
        //random rotation
        go.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        return go.GetComponent<acchub_client_data>();
    }

    void delete_object(int id, acchub_client_data c)
    {
        Destroy(c.gameObject, 0.1f);
    }
}
