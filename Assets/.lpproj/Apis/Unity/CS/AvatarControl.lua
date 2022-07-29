---@class CS.AvatarControl : CS.UnityEngine.MonoBehaviour
CS.AvatarControl = {}

---@field public CS.AvatarControl.avatarModel : CS.AvatarModel
CS.AvatarControl.avatarModel = nil

---@field public CS.AvatarControl.roleUIViev : CS.RoleUIViev
CS.AvatarControl.roleUIViev = nil

---@field public CS.AvatarControl._instance : CS.AvatarControl
CS.AvatarControl._instance = nil

---@field public CS.AvatarControl.ab : CS.UnityEngine.AssetBundle
CS.AvatarControl.ab = nil

---@field public CS.AvatarControl.skinnedSourceDict : CS.System.Collections.Generic.Dictionary
CS.AvatarControl.skinnedSourceDict = nil

---@field public CS.AvatarControl.onRoleChange : CS.System.Action
CS.AvatarControl.onRoleChange = nil

---@field public CS.AvatarControl.onInitNewRole : CS.System.Action
CS.AvatarControl.onInitNewRole = nil

---@field public CS.AvatarControl.onAddNewPart : CS.System.Action
CS.AvatarControl.onAddNewPart = nil

---@field public CS.AvatarControl.luaScript : CS.UnityEngine.TextAsset
CS.AvatarControl.luaScript = nil

---@return CS.AvatarControl
function CS.AvatarControl()
end

function CS.AvatarControl:Awake()
end

---@param prefabName : CS.System.String
function CS.AvatarControl:save(prefabName)
end

function CS.AvatarControl:initCharacter()
end

---@param part : CS.System.String
---@param num : CS.System.String
function CS.AvatarControl:tryToChangePeople(part, num)
end

---@param part : CS.System.String
function CS.AvatarControl:OnAddNewPart(part)
end

---@param part : CS.System.String
---@param num : CS.System.String
function CS.AvatarControl:OnChangePeople(part, num)
end

---@param go : CS.UnityEngine.GameObject
function CS.AvatarControl:removeMesh(go)
end

---@param part : CS.System.String
---@param num : CS.System.String
---@return CS.UnityEngine.SkinnedMeshRenderer
function CS.AvatarControl:getSkinnedMeshByPartAndNum(part, num)
end

---@param sourceName : CS.System.String
---@return CS.UnityEngine.Sprite
function CS.AvatarControl:loadSpriteFromAssetBundle(sourceName)
end

---@param sourceName : CS.System.String
---@return CS.System.Object
function CS.AvatarControl:loadSourcesFromAssetBundle(sourceName)
end

---@param AssetBundleName : CS.System.String
---@return CS.UnityEngine.SkinnedMeshRenderer[]
function CS.AvatarControl:loadAllSourcesFromAssetBundle(AssetBundleName)
end

---@param bundleName : CS.System.String
---@return CS.UnityEngine.AssetBundle
function CS.AvatarControl:LoadAssetBundle(bundleName)
end

---@param go : CS.UnityEngine.Object
---@return CS.UnityEngine.GameObject
function CS.AvatarControl:TypeChange(go)
end

---@param partName : CS.System.String
---@return CS.System.Collections.Generic.List
function CS.AvatarControl:getAllMeshNameOfPart(partName)
end