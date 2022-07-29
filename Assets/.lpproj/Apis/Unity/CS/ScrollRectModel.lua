---@class CS.ScrollRectModel : CS.UnityEngine.MonoBehaviour
CS.ScrollRectModel = {}

---@field public CS.ScrollRectModel.datas : CS.System.Collections.Generic.List
CS.ScrollRectModel.datas = nil

---@field public CS.ScrollRectModel.datasAndIndex : CS.System.Collections.Generic.Dictionary
CS.ScrollRectModel.datasAndIndex = nil

---@field public CS.ScrollRectModel.needDispose : CS.System.Collections.Generic.List
CS.ScrollRectModel.needDispose = nil

---@field public CS.ScrollRectModel.item : CS.UnityEngine.GameObject
CS.ScrollRectModel.item = nil

---@field public CS.ScrollRectModel.itemWidth : CS.System.Int32
CS.ScrollRectModel.itemWidth = nil

---@field public CS.ScrollRectModel.setRecordItem : CS.System.Action
CS.ScrollRectModel.setRecordItem = nil

---@field public CS.ScrollRectModel.removeItem : CS.System.Action
CS.ScrollRectModel.removeItem = nil

---@field public CS.ScrollRectModel.scriptEnv : CS.XLua.LuaTable
CS.ScrollRectModel.scriptEnv = nil

---@field public CS.ScrollRectModel.luaScript : CS.UnityEngine.TextAsset
CS.ScrollRectModel.luaScript = nil

---@return CS.ScrollRectModel
function CS.ScrollRectModel()
end

---@param startNum : CS.System.Int32
---@param endNum : CS.System.Int32
function CS.ScrollRectModel:removeUnUseItem(startNum, endNum)
end

---@param startNum : CS.System.Int32
---@param endNum : CS.System.Int32
function CS.ScrollRectModel:generaNewItem(startNum, endNum)
end

---@param index : CS.System.Int32
function CS.ScrollRectModel:addNewItem(index)
end

---@param go : CS.UnityEngine.GameObject
function CS.ScrollRectModel:deleteItem(go)
end

function CS.ScrollRectModel:clearRecord()
end