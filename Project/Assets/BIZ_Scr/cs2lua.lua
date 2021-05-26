----------------------------------------------------------------
-- File	        ：Assets\BIZ_Scr\cs2lua.lua
-- Author	    ：www.loywong.com
-- COPYRIGHT    ：(C)
-- Date	        ：2019/12/27
-- Description  ：C#层调⽤的Lua层 胶合脚本
-- Version      ：1.0
-- Maintain	    ：//[date] desc
----------------------------------------------------------------

cs2lua = {}
local this=	cs2lua

-- loading ui ////////////////////////////////////////////////////////////////// begin
local hasplayedaudio2battle = false
function  this.Loading_SetProgress(value)
    if hasplayedaudio2battle == false then
        if value > 0.36 then
            hasplayedaudio2battle = true
            audiomanager.Play2D("loading2battle")
        end
    end
end
function this.Loading_OnShow()
    UILoading2ctrl.OnShow()
end
function this.Loading_OnHide()
    UILoading2ctrl.OnHide()
end
-- loading ui ////////////////////////////////////////////////////////////////// end