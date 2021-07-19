----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\logsetting.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/08/20
-- Description	:
                -- 控制log的开启美闭， 以及定制标签
                -- 标签分为3类
                -- 1. 框架层CORE
                --      1-1功能
                --      1-2模块
                --      1-3流程
                -- 2. 业务层BIZ(所有C#都要翻译成Lua)
                -- 3. 个⼈⾃由定制层PERS(personal)

                -- 颜⾊设置规范!!!
                -- Blue	        Model	    蓝⾊(数据)
                -- Gray	        View	    灰⾊(极图)
                -- Green        Controller  绿⾊(流程)

                -- orange	相当于  Debug.warn
                -- Red      相当于  Debug.Error
                -- Tracfe	相当于  Debug.Log 其他⼀般log
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

-- 控制lua层的日志是否携带堆栈信息
if not IsEditor() then
    log.isStackOpen = false
end

-- 1 CORE -----------------------------------------
-- 1-1 功能
-- Log.OpenTag("util") ;                    --Trace⽩⾊

-- 1-2模块
Log.OpenTag("net")
Log.OpenTag("socket")
if IsEditor() then
    Log.OpenTag("tick")
end

Log.OpenTag("audio")
Log.OpenTag("asset")
Log.OpenTag("ui")

-- 1-3 流程
Log.OpenTag("scene")                        -- Green绿⾊
Log.OpenTag("workflow")                         -- Green绿⾊
Log.OpenTag("login")                        -- Green绿⾊
Log.OpenTag("sdk")
Log.OpenTag("test")

-- 3 PERS--------------------------------------
Log.OpenTag("loywong")