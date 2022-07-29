---@class CS.ScrollRectUIView : CS.UnityEngine.MonoBehaviour
CS.ScrollRectUIView = {}

---@field public CS.ScrollRectUIView.SRC : CS.ScrollRectControl
CS.ScrollRectUIView.SRC = nil

---@return CS.ScrollRectUIView
function CS.ScrollRectUIView()
end

---@param index : CS.System.Int32
---@param go : CS.UnityEngine.GameObject
---@param SRM : CS.ScrollRectModel
function CS.ScrollRectUIView:setRecordItem(index, go, SRM)
end

---@param width : CS.System.Int32
---@param height : CS.System.Int32
function CS.ScrollRectUIView:setContent(width, height)
end

---@param go : CS.UnityEngine.GameObject
function CS.ScrollRectUIView:disableItem(go)
end