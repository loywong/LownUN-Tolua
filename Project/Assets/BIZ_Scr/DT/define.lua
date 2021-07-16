----------------------------------------------------------------
-- File			: Assets\BIZ_Scr\DT\define.lua
-- Author		: www.loywong.com
-- COPYRIGHT	: (C)
-- Date			: 2020/05/07
-- Description	: 定义全局对象
-- Version		: 1.0
-- Maintain		: [date] desc
----------------------------------------------------------------

-- == Enum_Scene in C#
enum_scene = {
    None = 0,
    Splash = 1, --//自定义的片头展示
    NewPlayer = 2, --//新手引导专有展示场景（只触发一次（但必须完整的通过，否则会再次触发））
    Login = 3,
    Lobby = 4,
    Battle = 5,
}