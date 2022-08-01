---@class CS.ScrollRectUIView : CS.UnityEngine.MonoBehaviour
CS.ScrollRectUIView = {}

---@field public CS.ScrollRectUIView.SRC : CS.ScrollRectControl
CS.ScrollRectUIView.SRC = nil

---@field public CS.ScrollRectUIView.avatarModel : CS.AvatarModel
CS.ScrollRectUIView.avatarModel = nil

---@field public CS.ScrollRectUIView.roleUIViev : CS.RoleUIViev
CS.ScrollRectUIView.roleUIViev = nil

---@field public CS.ScrollRectUIView._instance : CS.AvatarControl
CS.ScrollRectUIView._instance = nil

---@field public CS.ScrollRectUIView.ab : CS.UnityEngine.AssetBundle
CS.ScrollRectUIView.ab = nil

---@field public CS.ScrollRectUIView.skinnedSourceDict : CS.System.Collections.Generic.Dictionary
CS.ScrollRectUIView.skinnedSourceDict = nil

---@field public CS.ScrollRectUIView.onRoleChange : CS.System.Action
CS.ScrollRectUIView.onRoleChange = nil

---@field public CS.ScrollRectUIView.onInitNewRole : CS.System.Action
CS.ScrollRectUIView.onInitNewRole = nil

---@field public CS.ScrollRectUIView.onAddNewPart : CS.System.Action
CS.ScrollRectUIView.onAddNewPart = nil

---@field public CS.ScrollRectUIView.luaScript : CS.UnityEngine.TextAsset
CS.ScrollRectUIView.luaScript = nil

---@return CS.ScrollRectUIView
function CS.ScrollRectUIView()
end

---@param index : CS.System.Int32
---@param go : CS.UnityEngine.GameObject
---@param SRM : CS.ScrollRectModel
function CS.ScrollRectUIView:setRecordItem(index, go, SRM)
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