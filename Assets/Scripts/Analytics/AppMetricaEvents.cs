using System.Collections.Generic;
using UnityEngine;

public class AppMetricaEvents : MonoBehaviour
{
    [SerializeField] private Data _data;

    public void OnGameFirstInicialize()
    {

    }

    public void OnGameInitialize()
    {
        AppMetrica.Instance.ReportEvent("game_start", new Dictionary<string, object>()
        {
            {"count",_data.GetSessionCount() }
        });
    }


}
