/****************************************************************
 * File			: Assets\CORE\LogSetting.cs
 * Author		: www.loywong.com
 * COPYRIGHT	: (C)
 * Date			: 2019/07/29
 * Description	: 自定义log输出功能的全局设置
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

public class LogSetting {
    public static void OnInit () {
        Log.SetOpen(true/*读取本地配置*/);

        // Core
        // Log.OpenTag ("editor");
        Log.OpenTag ("asset");
        Log.OpenTag ("ui");
        // Log.OpenTag ("sound");
        Log.OpenTag ("scene");
        // Log.OpenTag ("lg");
        Log.OpenTag ("workflow");
        Log.OpenTag ("lua");
        Log.OpenTag ("test");

        // Biz
        // Log.OpenTag ("skill");
        Log.OpenTag ("data");
    }
}