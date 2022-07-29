---@class CS.AvatarButton : CS.UnityEngine.MonoBehaviour
CS.AvatarButton = {}

---@field public CS.AvatarButton.leftClick : CS.UnityEngine.Events.UnityEvent
CS.AvatarButton.leftClick = nil

---@field public CS.AvatarButton.rightClick : CS.UnityEngine.Events.UnityEvent
CS.AvatarButton.rightClick = nil

---@field public CS.AvatarButton.onButtonLeftClick : CS.System.Action
CS.AvatarButton.onButtonLeftClick = nil

---@field public CS.AvatarButton.onButtonRightClick : CS.System.Action
CS.AvatarButton.onButtonRightClick = nil

---@return CS.AvatarButton
function CS.AvatarButton()
end

---@param eventData : CS.UnityEngine.EventSystems.PointerEventData
function CS.AvatarButton:OnPointerClick(eventData)
end