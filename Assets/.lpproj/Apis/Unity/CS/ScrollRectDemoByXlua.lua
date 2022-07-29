---@class CS.ScrollRectDemoByXlua : CS.UnityEngine.MonoBehaviour
CS.ScrollRectDemoByXlua = {}

---@field public CS.ScrollRectDemoByXlua.listContent : CS.UnityEngine.GameObject
CS.ScrollRectDemoByXlua.listContent = nil

---@field public CS.ScrollRectDemoByXlua.scrollRect : CS.UnityEngine.GameObject
CS.ScrollRectDemoByXlua.scrollRect = nil

---@field public CS.ScrollRectDemoByXlua.datas : CS.System.Collections.Generic.List
CS.ScrollRectDemoByXlua.datas = nil

---@field public CS.ScrollRectDemoByXlua.col : CS.System.Int32
CS.ScrollRectDemoByXlua.col = nil

---@field public CS.ScrollRectDemoByXlua.row : CS.System.Int32
CS.ScrollRectDemoByXlua.row = nil

---@field public CS.ScrollRectDemoByXlua.cell : CS.System.Int32
CS.ScrollRectDemoByXlua.cell = nil

---@field public CS.ScrollRectDemoByXlua.chink : CS.System.Int32
CS.ScrollRectDemoByXlua.chink = nil

---@field public CS.ScrollRectDemoByXlua.isDragIng : CS.System.Boolean
CS.ScrollRectDemoByXlua.isDragIng = nil

---@field public CS.ScrollRectDemoByXlua.isLoadingRecord : CS.System.Boolean
CS.ScrollRectDemoByXlua.isLoadingRecord = nil

---@field public CS.ScrollRectDemoByXlua.datasAndIndex : CS.System.Collections.Generic.Dictionary
CS.ScrollRectDemoByXlua.datasAndIndex = nil

---@field public CS.ScrollRectDemoByXlua.needDispose : CS.System.Collections.Generic.List
CS.ScrollRectDemoByXlua.needDispose = nil

---@field public CS.ScrollRectDemoByXlua.partName : CS.System.String
CS.ScrollRectDemoByXlua.partName = nil

---@field public CS.ScrollRectDemoByXlua.scriptEnv : CS.XLua.LuaTable
CS.ScrollRectDemoByXlua.scriptEnv = nil

---@field public CS.ScrollRectDemoByXlua.luaScript : CS.UnityEngine.TextAsset
CS.ScrollRectDemoByXlua.luaScript = nil

---@return CS.ScrollRectDemoByXlua
function CS.ScrollRectDemoByXlua()
end

---@param filename : CS.System.String
---@return CS.System.Byte[]
function CS.ScrollRectDemoByXlua:LoadLuaScript(filename)
end

---@param y : CS.System.Single
function CS.ScrollRectDemoByXlua:onRecordDrag(y)
end

---@param index : CS.System.Int32
---@param col : CS.System.Int32
---@param cell : CS.System.Int32
---@return CS.System.Int32
function CS.ScrollRectDemoByXlua:getPos_Y(index, col, cell)
end

---@param y : CS.System.Single
---@param cell : CS.System.Int32
---@return CS.System.Int32
function CS.ScrollRectDemoByXlua:getIndex(y, cell)
end