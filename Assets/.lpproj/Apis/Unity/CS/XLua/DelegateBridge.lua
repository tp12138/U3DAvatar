---@module CS.XLua
CS.XLua = {}

---@class CS.XLua.DelegateBridge : CS.XLua.DelegateBridgeBase
CS.XLua.DelegateBridge = {}

---@field public CS.XLua.DelegateBridge.Gen_Flag : CS.System.Boolean
CS.XLua.DelegateBridge.Gen_Flag = nil

---@param reference : CS.System.Int32
---@param luaenv : CS.XLua.LuaEnv
---@return CS.XLua.DelegateBridge
function CS.XLua.DelegateBridge(reference, luaenv)
end

---@param p0 : CS.System.Int32
---@param p1 : CS.System.String
---@param p2 : CS.Tutorial.DClass
---@return CS.System.Int32
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp0(p0, p1, p2)
end

---@return CS.System.Action
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp1()
end

---@param p0 : CS.System.Single
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp2(p0)
end

---@param p0 : CS.UnityEngine.Vector2
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp3(p0)
end

---@param p0 : CS.System.Int32
---@param p1 : CS.UnityEngine.GameObject
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp4(p0, p1)
end

---@param p0 : CS.System.String
---@param p1 : CS.System.String
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp5(p0, p1)
end

---@param p0 : CS.UnityEngine.GameObject
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp6(p0)
end

---@param p0 : CS.System.Object
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp7(p0)
end

---@param p0 : CS.System.Object
---@param p1 : CS.System.String
---@return CS.System.Byte[]
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp8(p0, p1)
end

---@param p0 : CS.System.Object
---@param p1 : CS.System.Single
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp9(p0, p1)
end

---@param p0 : CS.System.Object
---@param p1 : CS.System.Int32
---@param p2 : CS.System.Int32
---@param p3 : CS.System.Int32
---@return CS.System.Int32
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp10(p0, p1, p2, p3)
end

---@param p0 : CS.System.Object
---@param p1 : CS.System.Single
---@param p2 : CS.System.Int32
---@return CS.System.Int32
function CS.XLua.DelegateBridge:__Gen_Delegate_Imp11(p0, p1, p2)
end

---@param type : CS.System.Type
---@return CS.System.Delegate
function CS.XLua.DelegateBridge:GetDelegateByType(type)
end

---@param L : CS.System.IntPtr
---@param nArgs : CS.System.Int32
---@param nResults : CS.System.Int32
---@param errFunc : CS.System.Int32
function CS.XLua.DelegateBridge:PCall(L, nArgs, nResults, errFunc)
end

function CS.XLua.DelegateBridge:InvokeSessionStart()
end

---@param nRet : CS.System.Int32
function CS.XLua.DelegateBridge:Invoke(nRet)
end

function CS.XLua.DelegateBridge:InvokeSessionEnd()
end

function CS.XLua.DelegateBridge:Action()
end