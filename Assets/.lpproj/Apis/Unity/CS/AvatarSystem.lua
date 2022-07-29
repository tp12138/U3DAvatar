---@class CS.AvatarSystem : CS.UnityEngine.MonoBehaviour
CS.AvatarSystem = {}

---@field public CS.AvatarSystem._instance : CS.AvatarSystem
CS.AvatarSystem._instance = nil

---@field public CS.AvatarSystem.skinnedSources : CS.System.Collections.Generic.Dictionary
CS.AvatarSystem.skinnedSources = nil

---@field public CS.AvatarSystem.target : CS.UnityEngine.GameObject
CS.AvatarSystem.target = nil

---@field public CS.AvatarSystem.targetHips : CS.UnityEngine.Transform[]
CS.AvatarSystem.targetHips = nil

---@field public CS.AvatarSystem.targetDatas : CS.System.String[]
CS.AvatarSystem.targetDatas = nil

---@field public CS.AvatarSystem.targetMesh : CS.System.Collections.Generic.Dictionary
CS.AvatarSystem.targetMesh = nil

---@field public CS.AvatarSystem.ab : CS.UnityEngine.AssetBundle
CS.AvatarSystem.ab = nil

---@field public CS.AvatarSystem.prefabsAb : CS.UnityEngine.AssetBundle
CS.AvatarSystem.prefabsAb = nil

---@field public CS.AvatarSystem.sourceSKinnedMesh : CS.System.Collections.Generic.List
CS.AvatarSystem.sourceSKinnedMesh = nil

---@return CS.AvatarSystem
function CS.AvatarSystem()
end

---@param filename : CS.System.String
---@return CS.System.Byte[]
function CS.AvatarSystem:LoadLuaScript(filename)
end

function CS.AvatarSystem:initCharacter()
end

---@param prefabName : CS.System.String
function CS.AvatarSystem:save(prefabName)
end

---@param part : CS.System.String[]
---@param num : CS.System.String[]
function CS.AvatarSystem:configCharater(part, num)
end

---@param part : CS.System.String
---@param num : CS.System.String
function CS.AvatarSystem:OnChangePeople(part, num)
end

---@param partName : CS.System.String
---@return CS.System.Collections.Generic.List
function CS.AvatarSystem:getAllMeshNameOfPart(partName)
end

---@param bundleName : CS.System.String
---@return CS.UnityEngine.AssetBundle
function CS.AvatarSystem:LoadAssetBundle(bundleName)
end

---@param sourceName : CS.System.String
---@return CS.UnityEngine.Sprite
function CS.AvatarSystem:loadSpriteFromAssetBundle(sourceName)
end

---@param sourceName : CS.System.String
---@return CS.System.Object
function CS.AvatarSystem:loadSourcesFromAssetBundle(sourceName)
end

---@param AssetBundleName : CS.System.String
---@return CS.UnityEngine.SkinnedMeshRenderer[]
function CS.AvatarSystem:loadAllSourcesFromAssetBundle(AssetBundleName)
end

---@param _object : CS.System.Object
---@return CS.UnityEngine.GameObject
function CS.AvatarSystem:TypeChange(_object)
end

---@return CS.System.Collections.Generic.Dictionary
function CS.AvatarSystem:getDict()
end

---@param tar : CS.UnityEngine.GameObject
---@return CS.UnityEngine.Transform[]
function CS.AvatarSystem:getBonesFromObj(tar)
end

---@param part : CS.System.String
---@param num : CS.System.String
---@return CS.System.Boolean
function CS.AvatarSystem:removeMesh(part, num)
end