---@class CS.AvatarModel : CS.UnityEngine.MonoBehaviour
CS.AvatarModel = {}

---@field public CS.AvatarModel.targetDatas : CS.System.String[]
CS.AvatarModel.targetDatas = nil

---@field public CS.AvatarModel.onUpdatePart : CS.System.Action
CS.AvatarModel.onUpdatePart = nil

---@field public CS.AvatarModel.onAddNewPart : CS.System.Action
CS.AvatarModel.onAddNewPart = nil

---@field public CS.AvatarModel.scriptEnv : CS.XLua.LuaTable
CS.AvatarModel.scriptEnv = nil

---@field public CS.AvatarModel.luaScript : CS.UnityEngine.TextAsset
CS.AvatarModel.luaScript = nil

---@field public CS.AvatarModel.nameToFun : CS.System.Collections.Generic.Dictionary
CS.AvatarModel.nameToFun = nil

---@return CS.AvatarModel
function CS.AvatarModel()
end

function CS.AvatarModel:Awake()
end

---@param part : CS.System.String
---@param num : CS.System.String
function CS.AvatarModel:updateData(part, num)
end

---@param part : CS.System.String[]
---@param num : CS.System.String[]
function CS.AvatarModel:configCharater(part, num)
end

---@param index : CS.System.Int32
---@param part : CS.System.String
---@param num : CS.System.String
function CS.AvatarModel:configRoleDate(index, part, num)
end