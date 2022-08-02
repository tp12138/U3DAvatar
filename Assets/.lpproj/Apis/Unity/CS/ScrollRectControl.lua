---@class CS.ScrollRectControl : CS.UnityEngine.MonoBehaviour
CS.ScrollRectControl = {}

---@field public CS.ScrollRectControl.col : CS.System.Int32
CS.ScrollRectControl.col = nil

---@field public CS.ScrollRectControl.row : CS.System.Int32
CS.ScrollRectControl.row = nil

---@field public CS.ScrollRectControl.cell : CS.System.Int32
CS.ScrollRectControl.cell = nil

---@field public CS.ScrollRectControl.isDragIng : CS.System.Boolean
CS.ScrollRectControl.isDragIng = nil

---@field public CS.ScrollRectControl.dataCount : CS.System.Int32
CS.ScrollRectControl.dataCount = nil

---@field public CS.ScrollRectControl.itemSize : CS.UnityEngine.Vector2
CS.ScrollRectControl.itemSize = nil

---@field public CS.ScrollRectControl.scriptEnv : CS.XLua.LuaTable
CS.ScrollRectControl.scriptEnv = nil

---@field public CS.ScrollRectControl.luaScript : CS.UnityEngine.TextAsset
CS.ScrollRectControl.luaScript = nil

---@return CS.ScrollRectControl
function CS.ScrollRectControl()
end

---@param posY : CS.System.Single
function CS.ScrollRectControl:onRecordDrag(posY)
end