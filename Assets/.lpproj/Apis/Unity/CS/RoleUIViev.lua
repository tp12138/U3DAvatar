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

---@field public CS.RoleUIViev.luaScript : CS.UnityEngine.TextAsset
CS.RoleUIViev.luaScript = nil

---@return CS.RoleUIViev
function CS.RoleUIViev()
end

---@param go : CS.UnityEngine.GameObject
function CS.RoleUIViev:removeMesh(go)
end

---@param filename : CS.System.String
---@return CS.System.Byte[]
function CS.RoleUIViev:LoadLuaScript(filename)
end

---@param target : CS.UnityEngine.GameObject
function CS.RoleUIViev:onInitNewRole(target)
end

---@param state : CS.System.String
---@param part : CS.System.String
---@param num : CS.System.String
function CS.RoleUIViev:changeMesh(state, part, num)
end

---@param partName : CS.System.String
function CS.RoleUIViev:setNewPart(partName)
end

---@param smr : CS.UnityEngine.SkinnedMeshRenderer
---@return CS.System.Collections.Generic.List
function CS.RoleUIViev:getBonesBySmr(smr)
end