----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\Core\audiomanager.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2019/12/26
-- Description	: ⾳效播放模块(Lua) 主要为了处理分不同bundle来打包 ⼤厅和不同玩法的音效 优化热更包体大小
                -- !!!所有关卡通⽤的⾳效也属于Asset_0的⾳效
                ----------------------------------------
                -- 需要确定bundle⽂件的接⼝
                -- AudioPoolManager.Instance:Play2D
                -- AudioPoolManager.Instance:Play2DLoop
                -- AudioPoolManager.Instance:PlayMusic
                -- AudioPoolManager.Instance:PlayBGAudio
                -- AudioPoolManager.Instance:Play3D(暂时不⽤)
                -- !!!要考虑C#层的调⽤,特别是AudioInAnim组件的应⽤需要指定参数:isOnlyInLevel
                ----------------------------------------
-- Version		: 1.0
-- Maintain		: //[date] desc
----------------------------------------------------------------

audiomanager = {}
local this = audiomanager

--2D⾳效默认⾳量
this.volumn2DDefault = 1

--玩法专属于每⼀ 个关卡内部的⾳效(不包括所有关卡通⽤的)
function this.Play2D_Battle(soundtype)
    if not gamecontroller.IsBattle() then
        logError('不在玩法场景,不能调⽤此接⼝ audiomanager.Play2D_Battle()')
        return
    end

    AudioPoolManager.Instance:Play2D(soundtype,this.volumn2DDefault,fn_map.curLevel.assetType,true)
end


function this.Play2DLoop_Battle(soundtype)
    AudioPoolManager.Instance:Play2DLoop(soundtype,this.volumn2DDefault,fn_map.curLevel.assetType,true)
end

function this.PlayBGAudio_Battle(bgm)
    AudioPoolManager.Instance:PlayBGAudio(bgm,fn_map.curLevel.assetType,true)
end


-- 通⽤⾳效
function this.Play2D(soundtype)
    AudioPoolManager.Instance:Play2D(soundtype,this.volumn2DDefault,0)
end

function this.Play2DLoop(soundtype)
    AudioPoolManager.Instance:Play2DLoop(soundtype,this.volumn2DDefault,0)
end
function this.End2D(sname)
    AudioPoolManager.Instance:End2D(sname)
end

function this.SetMusicVolume(v)
    AudioPoolManager.Instance:SetMusicVolume(v)
end
function this.PlayMusic(mname)
    AudioPoolManager.Instance:PlayMusic(mname)
end
function this.PauseMusic()
    AudioPoolManager.Instance:PauseMusic()
end
function this.UnPauseMusic()
    AudioPoolManager.Instance:UnPauseMusic()
end
function this.PlayBGAudio(bgm)
    AudioPoolManager.Instance:PlayBGAudio(bgm)
end
function this.StopMusic()
    AudioPoolManager.Instance:StopMusic()
end

-- //通常切换场景的时候调⽤
function this.ClearMusic()
    AudioPoolManager.Instance:ClearMusic()
    AudioPoolManager.Instance:Clear2DSpec()
end

-- 切后台 ------------------------------------------------- begin
function this.OnGameShow()
    AudioPoolManager.Instance:OnGameShow()
    --⾳乐
    --this.UnPauseMusic()
    --⾳效⽆需要恢复
end

function this.OnGameHide()
    AudioPoolManager.Instance:OnGameHide()
    -- --⾳乐
    -- this.PauseMusic()
    -- -- ⾳效
    -- AudioPoolManager.Instance:Clear Al12D()
end
-- 切后台 ------------------------------------------------- end




-- 设置特殊的播放模式 mode模式	volumerate⾳量系数	---------------------------------- begin
-- 1,//弹出ui界⾯的模式
function this.Set2DSpec_On(volumeRate)
    AudioPoolManager.Instance:Play2DSpec_On(volumeRate)
end
--关闭特殊的播放模式
function this.Set2DSpec_Off()
    AudioPoolManager.Instance:Play2DSpec_Off()
end

function this.Play2D_Spec(soundtype)
    AudioPoolManager.Instance:Play2D_Spec(soundtype,this.volumn2DDefault,0)
end

function this.Play2DLoop_Spec(soundtype)
    AudioPoolManager.Instance:Play2DLoop_Spec(soundtype,this.volumn2DDefault,0)
end
------------------------------------------------------------------------------------ end

--播放通⽤的⽆效⾳效
-- function this.PlayAudio_Invalid()
--     log_Orange("audio","PlayAudio_Invalid() 暂时不⽤")
-- do return end
--     this.Play2D("page_audio_sfx_board_fail")
-- end

--播放通⽤的成功⾳效
-- function this.PlayAudio_Success()
--     log_Orange("audio","PlayAudio_Success() 暂时不⽤")
-- do return end
--     this.Play2D("page_audio_sfx_board_show")
--     --this.Play2D("page_audio_sfx_btn_click")
-- end

-- 请等待 需要⼀点激情
function this.PlayAudio_ComingSoon()
    this.Play2D("page_audio_sfx_btn_positive")
end
-- 已完成事件
function this.PlayAudio_HasComplete()
    this.Play2D("page_audio_sfx_btn_negative")
end
--条件不满⾜
function this.PlayAudio_Invalid()
    this.Play2D("page_audio_sfx_btn_negative")
end

-- 2020/06/08wangliangIMP ------------------------------- begin
-- 被中断或者提前结束
local hasOver = false
--是否播放过⾳频
local hasPlayedNumberAudio = false
--TO DO存储事件类型对应的Tween ID--mT we enID暂时使⽤hasOver来控制,只⽀持同时播放⼀个该种类型的⾳效
--播放数字跳动得⾳效传⼊播放得时间
function this.PlayNumberAnimAudio(times)
    --logError("TEST!!!flow audiomanager.PlayNumberAnim Audio() times:"..tostring(times) ..'/CurTimeSpan:'..tostring(timehelper.CurTimeSpan) )
    --log_Stack()
    
    hasOver=false
    this.Play2DLoop("numberanim_loop")
    hasPlayedNumberAudio=true
    --0.08是结束⾳效得⻓度
    DOVirtual.DelayedCall(
        times - 0.08,
        function()
            if not hasOver then
                this.EndNumberAnimAudio()
            end
        end
    )
end

function this.EndNumberAnimAudio()
    -- 2020/07/08 wangliang TST -------------------------------- begin
    -- logError('audiomanager.EndNumberAnimAudio()')
    -- log_Stack()
    -- 新机制制作的机台这⾥有明显的延时!!!
    log_Orange("flow","audiomanager.EndNumberAnimAudio()")
    --------------------------------------------------------- end
    if hasPlayedNumberAudio then
        hasOver=true
        this.End2D("numberanim_loop")
        this.Play2D("numberanim_end")
        -- logError('TEST!!!flow audiomanager.EndNumberAnimAudio() CurTimeSpan:'..tostring(timehelper.CurTimeSpan) )
        hasPlayedNumberAudio=false
    end
end
-- 2020/06/08wangliangIMP ------------------------------- end