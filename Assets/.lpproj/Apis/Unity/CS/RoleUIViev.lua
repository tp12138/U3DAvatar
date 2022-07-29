---@class CS.RoleUIViev : CS.UnityEngine.MonoBehaviour
CS.RoleUIViev = {}

---@field public CS.RoleUIViev.avatarControl : CS.AvatarControl
CS.RoleUIViev.avatarControl = nil

---@field public CS.RoleUIViev.target : CS.UnityEngine.GameObject
CS.RoleUIViev.target = nil

---@field public CS.RoleUIViev.targetHips : CS.UnityEngine.Transform[]
CS.RoleUIViev.targetHips = nil

---@field public CS.RoleUIViev.targetSkinned : CS.System.Collections.Generic.Dictionary
CS.RoleUIViev.targetSkinned = nil

---@field public CS.RoleUIViev.targetHipsDict : CS.System.Collections.Generic.Dictionary
CS.RoleUIViev.targetHipsDict = nil

---@return CS.RoleUIViev
function CS.RoleUIViev()
end

---@param smr : CS.UnityEngine.SkinnedMeshRenderer
---@return CS.System.Collections.Generic.List
function CS.RoleUIViev:getBonesBySmr(smr)
end