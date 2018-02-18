using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acchub_client_data : MonoBehaviour
{
    [System.NonSerialized]
    public float hz_;
    [System.NonSerialized]
    public float power_;
    [System.NonSerialized]
    public float normalized_value_;

    private float t_;
    private float time_last_get_;
    private float hz_current_;
    [System.NonSerialized]
    public float power_current_;
    private float time_cycle_start_;

    public delegate void on_shake();
    public on_shake d_on_shake_;

    private void Start()
    {
    }
    private void Update()
    {
        t_ += Time.deltaTime;
        normalized_value_ = make_normalized_value();
    }

    public void update_acchub_data(float hz, float power)
    {
        hz_ = hz;
        power_ = power;
    }

    private float make_normalized_value()
    {
        if (hz_current_ == 0.0f)
        {
            hz_current_ = hz_;
            power_current_ = power_;
            time_cycle_start_ = t_;
            return 0.0f;
        }
        float cycle = 1.0f / hz_current_;
        float t = t_ - time_cycle_start_;
        if (t >= cycle)
        {
            while (t >= cycle)
            {
                t -= cycle;
            }
            hz_current_ = hz_;
            power_current_ = power_;
            time_cycle_start_ = t_ - t;
            d_on_shake_?.Invoke();
        }

        return Mathf.Sin(Mathf.PI * 2 * t / cycle);
    }
}