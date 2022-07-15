---@class CS.ScrollRectDemo : CS.UnityEngine.MonoBehaviour
CS.ScrollRectDemo = {}

---@field public CS.ScrollRectDemo.RankGrid : CS.UnityEngine.Transform
CS.ScrollRectDemo.RankGrid = nil

---@field public CS.ScrollRectDemo.scorllRect : CS.UnityEngine.UI.ScrollRect
CS.ScrollRectDemo.scorllRect = nil

---@field public CS.ScrollRectDemo.item : CS.UnityEngine.GameObject
CS.ScrollRectDemo.item = nil

---@field public CS.ScrollRectDemo.row : CS.System.Int32
CS.ScrollRectDemo.row = nil

---@field public CS.ScrollRectDemo.col : CS.System.Int32
CS.ScrollRectDemo.col = nil

---@field public CS.ScrollRectDemo.itemWidth : CS.System.Int32
CS.ScrollRectDemo.itemWidth = nil

---@field public CS.ScrollRectDemo.chink : CS.System.Int32
CS.ScrollRectDemo.chink = nil

---@field public CS.ScrollRectDemo.cell : CS.System.Int32
CS.ScrollRectDemo.cell = nil

---@field public CS.ScrollRectDemo.partName : CS.System.String
CS.ScrollRectDemo.partName = nil

---@field public CS.ScrollRectDemo.datas : CS.System.Collections.Generic.List
CS.ScrollRectDemo.datas = nil

---@field public CS.ScrollRectDemo.isDragIng : CS.System.Boolean
CS.ScrollRectDemo.isDragIng = nil

---@field public CS.ScrollRectDemo.isLoadingRecord : CS.System.Boolean
CS.ScrollRectDemo.isLoadingRecord = nil

---@return CS.ScrollRectDemo
function CS.ScrollRectDemo()
end

---@param filename : CS.System.String
---@return CS.System.Byte[]
function CS.ScrollRectDemo:LoadLuaScript(filename)
end

---@param row : CS.System.Int32
---@param col : CS.System.Int32
---@param itemWidth1 : CS.System.Int32
---@param chink : CS.System.Int32
---@param prefabName : CS.System.String
function CS.ScrollRectDemo:loadAssets(row, col, itemWidth1, chink, prefabName)
end

function CS.ScrollRectDemo:SetRecords()
end

---@return CS.System.Collections.IEnumerator
function CS.ScrollRectDemo:SetRecord()
end

function CS.ScrollRectDemo:ClearRecord()
end