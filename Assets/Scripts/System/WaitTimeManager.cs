using System.Collections.Generic;
using UnityEngine;

public static class WaitTimeManager
{
    private static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    private static Dictionary<float, WaitForSeconds> _waitTimeDic = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitTime(float time)
    {
        if (!_waitTimeDic.ContainsKey(time))
        {
            _waitTimeDic[time] = new WaitForSeconds(time);
        }

        return _waitTimeDic[time];
    }

    public static WaitForFixedUpdate GetFixedUpdate()
    {
        return _fixedUpdate;
    }
}
