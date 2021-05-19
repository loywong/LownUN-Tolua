-- 内建数据类型扩展
function table.GetValueByIndex(t, index)
    local i = 1
end

function table.GetCount(t)
end

function string.split(s, pattern)
    local rt = {}
    string.gsub(s,'[^' .. pattern ..']+',function(w) table.insert(rt,w) end)
    return rt
end

