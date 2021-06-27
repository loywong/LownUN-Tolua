----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\gamecontroller.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/08/24
-- Description	: Lua层⾯的全局管理类,有点杂!!!
                -- 1，场 景
                -- 2，相 机 控 制
                -- 3，资源
                -- 4，C#调⽤Lua层API
-- Version		: 1.0
-- Maintain		: //[2021/05] == Old /Core/GameManager.lua
----------------------------------------------------------------


gamecontroller = {}
local this = gamecontroller

--私有 属性 ----------------------------------------- begin
---------------------------------------------------- end

-- 公有 属性 --------------------------------------- begin
-- 是否处于场景切换过程中(调⽤xxxCtrl.OnEnd⽅法.清理gameobject.清理旧lua脚本.预加载新场景资源.调⽤新xxxCtrl.OnStart)
this.isSceneSwitching = false
-- 上⼀场景
this.prestateid = 0 -- ELevelID.NONE
-- this.prestatetype = 0
-- 当前场景
this.curstateid = 1
-- this.curstatetype = 1
-- 记录切换场景的开始时间
this.stateStartTime = 0
--------------------------------------------------- end

-- -- 当前在登录场景
-- function this.IsLogin()
--     return curstateid==Enum_GameState.LOGIN
-- end

-- 当前在⼤厅场景
function this.IsLobby()
    return this.curstateid == ELevelID.LOBBY
end

--当前在玩法场景
function this.IsBattle()
    return this.curstateid == ELevelID.BATTLE
end

function this.IsIngame()
    return this.IsLobby() or this.IsBattle()
end