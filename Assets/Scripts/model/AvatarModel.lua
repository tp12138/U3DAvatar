---------------------------------------------------------------------
-- U3DAvatar (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2022-07-28 10:24:04
---------------------------------------------------------------------

-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File

---@class AvatarModel


AvatarModel = {}
AvatarModel.part={}
AvatarModel.num={}
AvatarModel.state=""

--上层调用
function AvatarModel.updateData(part,num)
	local len=#AvatarModel.part
	for i = 1, len do
		if AvatarModel.part[i]==part then
			AvatarModel.num[i]=num
			AvatarModel:UpdatePart(part,num)
			return
		end
	end
end

function  AvatarModel.setState(state)
	AvatarModel.state=state	
end

function  AvatarModel.initCharacter(dataPart,dataNum)
	for k, v in pairs(dataPart) do
		table.insert(AvatarModel.part,v)
		AvatarModel:AddNewPart(v)
	end
	for k, v in pairs(dataNum) do
		table.insert(AvatarModel.num,v)
	end
	for i = 1, #AvatarModel.part do
		AvatarModel:UpdatePart(AvatarModel.part[i],AvatarModel.num[i])
	end
	
end



--config Observer

function creat()
	local signalToMethod={}
	
	local weak={_mode="null"}
	
	--add observer
	local register=function(signal,observer,method)
		local t=signalToMethod[signal] or {}
		local ob=observer or weak
		local meth={method,ob}
		setmetatable(meth,weak)
		table.insert(t,meth)
		signalToMethod[signal]=t
	end
	
	--removeObserver
	local cancel=function(signal,observer,method)
		local t=signalToMethod[signal]
		if not t then
			return
		end
		
		if not method and not observer then
			signalToMethod[signal]=nil
			return
		end
		local i,v
		i=#t
		while i>0 do
			v=t[i] or {}
			if ~method or v[1]==method then
				if not observer or v[2]==observer then
					table.remove(t,i)
				end
			end
			i=i-1
		end
	end
	
	--notify
	local notify=function(signal,...)
		local t=signalToMethod[signal]
		if not t then
			return
		end
		for k, v in pairs(t) do
			if v[2]==weak then
				v[1](...)
			else
				
				v[1](...)
			end
		end
	end
	
	return register,cancel,notify
end

function signal(notify,name)
	return function (...)
		return notify(name,...)
	end
end


AvatarModel.reg,AvatarModel.cancel,AvatarModel.notify=creat()
function AvatarModel:AddObserver(signal,observer,method)
	if method~=nil then
		AvatarModel.reg(signal,observer,method)
	end
	
	
end

function AvatarModel:RemoveObserver(signal,observer,method)
	if method~=nil then
		AvatarModel.cancel(signal,observer,method)
	end
	
end

--notify observer
function AvatarModel:AddNewPart(part)
	AvatarModel.notify("addNewPart",part)

end
function AvatarModel:UpdatePart(part,num)
	local partNum=part.."-"..num
	AvatarModel.notify("updatePart",partNum)
end
return  AvatarModel
