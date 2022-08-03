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
		--print(AvatarModel.part[i])
		AvatarModel:UpdatePart(AvatarModel.part[i],AvatarModel.num[i])
	end
	--print(#AvatarModel.part)
end

--config Observer
function AvatarModel:AddObserver(observer)
	if self.observer==nil then
		self.observers={}
	end
	table.insert(self.observers,observer)
end

function AvatarModel:RemoveObserver(observer)
	for k, v in pairs(self.observers) do
		if observer==v then
			table.remove(self.observers,k)
			break
		end
	end
end


--notify observer
function AvatarModel:AddNewPart(part)
	for k, v in pairs(self.observers) do
		v.AddNewPart(part)
	end
end
function AvatarModel:UpdatePart(part,num)
	for k, v in pairs(self.observers) do
		v.onUpdatePart(self.state,part,num)
	end
end

return  AvatarModel
