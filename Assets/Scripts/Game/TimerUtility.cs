using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//returns deltatime * 60 for ez conversions
public class TimerUtility {
    public static float DeltaTimer()
    {
        return Time.deltaTime * 60;
    }
}
