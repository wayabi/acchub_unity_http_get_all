using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acchub_character : MonoBehaviour
{
    acchub_client_data acchub_client_data_;
    bool flag_shake_ = false;
    Rigidbody rigid_body_;
    public float amplitude_force_ = 10.0f;

	private void Start () {
        acchub_client_data_ = gameObject.GetComponent<acchub_client_data>();
        acchub_client_data_.d_on_shake_ += on_shake;
        rigid_body_ = gameObject.GetComponent<Rigidbody>();

        gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(Random.Range(0.2f, 1.0f), Random.Range(0.2f, 1.0f), Random.Range(0.2f, 1.0f));
	}
	
    private void Update()
    {
        float amplitude_size = 0.1f;
        float size = 1.0f + amplitude_size * acchub_client_data_.power_ * acchub_client_data_.normalized_value_;
        float min_size = 0.5f;
        float max_size = 1.5f;
        if (size < min_size) size = min_size;
        if (size > max_size) size = max_size;
        transform.localScale = new Vector3(1.0f, size, 1.0f);
    }

    void on_shake()
    {
        Vector3 force = amplitude_force_ * acchub_client_data_.power_ * transform.up;
        rigid_body_.AddForce(force);
    }
}
