using UnityEngine;

public class GameSetting {
    public static readonly int FrameRate = 60;
    // 调试模式，控制的对象有：
    // 1：Log输出
    // 2：控制台
    // 3：单元测试
    // 4：
    public static readonly bool isDebugMode = true;

    public static void OnInit () {
        // Env(Engine System)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = FrameRate;
    }
}