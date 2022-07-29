---@class CS.ScrollRectControl : CS.UnityEngine.MonoBehaviour
CS.ScrollRectControl = {}

---@field public CS.ScrollRectControl.scrollModel : CS.ScrollRectModel
CS.ScrollRectControl.scrollModel = nil

---@field public CS.ScrollRectControl.col : CS.System.Int32
CS.ScrollRectControl.col = nil

---@field public CS.ScrollRectControl.row : CS.System.Int32
CS.ScrollRectControl.row = nil

---@field public CS.ScrollRectControl.cell : CS.System.Int32
CS.ScrollRectControl.cell = nil

---@field public CS.ScrollRectControl.chink : CS.System.Int32
CS.ScrollRectControl.chink = nil

---@field public CS.ScrollRectControl.isDragIng : CS.System.Boolean
CS.ScrollRectControl.isDragIng = nil

---@field public CS.ScrollRectControl.isLoadingRecord : CS.System.Boolean
CS.ScrollRectControl.isLoadingRecord = nil

---@field public CS.ScrollRectControl.scriptEnv : CS.XLua.LuaTable
CS.ScrollRectControl.scriptEnv = nil

---@field public CS.ScrollRectControl.luaScript : CS.UnityEngine.TextAsset
CS.ScrollRectControl.luaScript = nil

---@field public CS.ScrollRectControl.partName : CS.System.String
CS.ScrollRectControl.partName = nil

---@field public CS.ScrollRectControl.setNewItemInView : CS.System.Action
CS.ScrollRectControl.setNewItemInView = nil

---@field public CS.ScrollRectControl.setContent : CS.System.Action
CS.ScrollRectControl.setContent = nil

---@field public CS.ScrollRectControl.listContent : CS.UnityEngine.GameObject
CS.ScrollRectControl.listContent = nil

---@field public CS.ScrollRectControl.scrollRect : CS.UnityEngine.GameObject
CS.ScrollRectControl.scrollRect = nil

---@field public CS.ScrollRectControl.deletaItemInView : CS.System.Action
CS.ScrollRectControl.deletaItemInView = nil

---@return CS.ScrollRectControl
function CS.ScrollRectControl()
end

---@param y : CS.System.Single
function CS.ScrollRectControl:onRecordDrag(y)
end

---@param y : CS.System.Single
---@param cell : CS.System.Int32
---@return CS.System.Int32
function CS.ScrollRectControl:getIndex(y, cell)
end

---@param index : CS.System.Int32
---@param col : CS.System.Int32
---@param cell : CS.System.Int32
---@return CS.System.Int32
function CS.ScrollRectControl:getPos_Y(index, col, cell)
end

---@param index : CS.System.Int32
---@return CS.UnityEngine.Vector3
function CS.ScrollRectControl:getItemPosition(index)
end

---@param t : CS.UnityEngine.Vector2
function CS.ScrollRectControl:onchan(t)
end