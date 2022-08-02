---@class CS.ScrollRectUIView : CS.UnityEngine.MonoBehaviour
CS.ScrollRectUIView = {}

---@field public CS.ScrollRectUIView.listContent : CS.UnityEngine.GameObject
CS.ScrollRectUIView.listContent = nil

---@field public CS.ScrollRectUIView.scrollRect : CS.UnityEngine.GameObject
CS.ScrollRectUIView.scrollRect = nil

---@field public CS.ScrollRectUIView.partName : CS.System.String
CS.ScrollRectUIView.partName = nil

---@field public CS.ScrollRectUIView.SRC : CS.ScrollRectControl
CS.ScrollRectUIView.SRC = nil

---@return CS.ScrollRectUIView
function CS.ScrollRectUIView()
end

---@param y : CS.System.Single
function CS.ScrollRectUIView:onDrag(y)
end

---@param index : CS.System.Int32
---@param go : CS.UnityEngine.GameObject
---@param itemName : CS.System.String
---@param itemSize : CS.UnityEngine.Vector2
---@param itemPos : CS.UnityEngine.Vector3
function CS.ScrollRectUIView:setRecordItem(index, go, itemName, itemSize, itemPos)
end

---@param state : CS.System.String
---@param a : CS.System.String
---@param b : CS.System.String
function CS.ScrollRectUIView:chengeMesh(state, a, b)
end

---@param width : CS.System.Int32
---@param height : CS.System.Int32
function CS.ScrollRectUIView:setContent(width, height)
end

---@param go : CS.UnityEngine.GameObject
function CS.ScrollRectUIView:disableItem(go)
end